using System.Globalization;
using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Libraries.GeoAdminSearch;
using AlgoTecture.Libraries.PriceSpecifications;
using AlgoTecture.Libraries.Reservations;
using AlgoTecture.Libraries.Spaces.Interfaces;
using AlgoTecture.TelegramBot.Controllers;
using AlgoTecture.TelegramBot.Interfaces;
using AlgoTecture.TelegramBot.Models;

public class BookingActionService
{
    private readonly ILogger<BookingActionService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPriceCalculator _priceCalculator;
    private readonly IGeoAdminSearcher _geoAdminSearcher;
    private readonly ITelegramToAddressResolver _telegramToAddressResolver;
    private readonly ISpaceGetter _spaceGetter;
    private readonly ISpaceService _spaceService;
    private readonly IReservationService _reservationService;
    private readonly TimeZoneInfo _zurichTz = TimeZoneInfo.FindSystemTimeZoneById("Europe/Zurich");

    public BookingActionService(ILogger<BookingActionService> logger,
        IServiceProvider serviceProvider, IUnitOfWork unitOfWork, IPriceCalculator priceCalculator,
        IGeoAdminSearcher geoAdminSearcher, ITelegramToAddressResolver telegramToAddressResolver,
        ISpaceGetter spaceGetter, ISpaceService spaceService, IReservationService reservationService)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _unitOfWork = unitOfWork;
        _priceCalculator = priceCalculator;
        _geoAdminSearcher = geoAdminSearcher;
        _telegramToAddressResolver = telegramToAddressResolver;
        _spaceGetter = spaceGetter;
        _spaceService = spaceService;
        _reservationService = reservationService;
    }

    public async Task<(string, BotState, TelegramToAddressModel)> ExecuteActionAsync(BookingIntent intent)
    {
        if (!intent.IsValid)
            return
                ("I didn't quite understand your request. Please specify: parking address, dates, and your vehicle license plate.", null, null);

        try
        {
            _logger.LogInformation("Executing parking action: {Action}", intent.Action);

            return intent.Action switch
            {
                "create_booking" => await CreateParkingBookingAsync(intent),
                //   "cancel_booking" => await CancelParkingBookingAsync(intent),
                //    "check_availability" => await CheckParkingAvailabilityAsync(intent),
                //    "extend_booking" => await ExtendParkingBookingAsync(intent),
                //    "get_price" => await GetParkingPriceAsync(intent),
                _ => ("This action is not currently supported in the parking system", null, null)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing parking action: {Action}", intent.Action);
            return ("An error occurred while processing your request. Please try again.", null, null);
        }
    }

    private async Task<(string, BotState, TelegramToAddressModel)> CreateParkingBookingAsync(BookingIntent intent)
    {
        var address = intent.GetParameter("address");
        var dateFrom = intent.GetDateParameter("start_datetime");
        var dateTo = intent.GetDateParameter("end_datetime");
        var carNumber = intent.GetParameter("car_number");
        var parkingType = intent.GetParameter("parking_type", "street");

        if (dateFrom == null || dateTo == null)
            return ("Please specify the start and end dates for parking space rental.", null, null);

        var booking = new ParkingBooking
        {
            Address = address,
            DateFrom = dateFrom.Value,
            DateTo = dateTo.Value,
            CarNumber = carNumber,
            ParkingType = parkingType,
        };

        var labels = (await _geoAdminSearcher.GetAddress(address)).ToList();

        if (labels.Count == 0)
            return ("Please specify the address", null, null);

        var targetAddress = labels.First();

        var telegramToAddressModel = new TelegramToAddressModel
        {
            FeatureId = targetAddress.featureId,
            OriginalAddressLatitude = targetAddress.lat.ToString(CultureInfo.InvariantCulture),
            OriginalAddressLongitude = targetAddress.lon.ToString(CultureInfo.InvariantCulture),
            Address = targetAddress.label
        };
        var targetSpacesInside = await _spaceGetter.GetByType(15);
        var targetSpacesOutside = await _spaceGetter.GetByType(16);
        var targetSpaces = targetSpacesInside.Take(1).ToList();
        targetSpaces.AddRange(targetSpacesOutside);

        var nearestParkingSpaces = await _spaceService.GetNearestSpaces(targetSpaces,
            Convert.ToDouble(telegramToAddressModel.OriginalAddressLatitude, CultureInfo.InvariantCulture),
            Convert.ToDouble(telegramToAddressModel.OriginalAddressLongitude, CultureInfo.InvariantCulture), 7);

        if (nearestParkingSpaces.Any())
        {
            var counter = 1;
            var tamModel = new TelegramToAddressModel
            {
                latitude = nearestParkingSpaces.First().Value.Latitude.ToString(CultureInfo.InvariantCulture),
                longitude = nearestParkingSpaces.First().Value.Longitude.ToString(CultureInfo.InvariantCulture),
                OriginalAddressLatitude = telegramToAddressModel.OriginalAddressLatitude,
                OriginalAddressLongitude = telegramToAddressModel.OriginalAddressLongitude,
            };

            return
                ($"✅ Parking space ready to book!\n📍 Address: {address}\n🚗 Vehicle: {carNumber}\n📅 From {dateFrom.Value:MM/dd/yyyy} to {dateTo.Value:MM/dd/yyyy}\n🏷 Type: {parkingType}", new BotState()
                {
                    CarNumber = carNumber,
                    EndRent = DateTime.SpecifyKind(dateTo.Value.ToUniversalTime(), DateTimeKind.Utc) ,
                    StartRent = DateTime.SpecifyKind(dateFrom.Value.ToUniversalTime(), DateTimeKind.Utc) ,
                    SpaceAddress = telegramToAddressModel.Address,
                    SpaceId = nearestParkingSpaces.First().Value.Id,
                    UtilizationTypeId = nearestParkingSpaces.First().Value.UtilizationTypeId,
                }, tamModel);
        }

        return  ("❌ Failed to book the space. There may be no available spots for the specified dates.", null, null);
    }

    // private async Task<string> CancelParkingBookingAsync(BookingIntent intent)
    // {
    //     if (intent.HasParameter("booking_id"))
    //     {
    //         var result = await _bookingRepository.CancelAsync(intent.GetParameter("booking_id"));
    //         return result ? "✅ Parking reservation cancelled" : "❌ Could not find reservation to cancel";
    //     }
    //     else
    //     {
    //         var result = await _bookingRepository.CancelByParametersAsync(
    //             intent.GetParameter("address"),
    //             intent.GetDateParameter("date_from"),
    //             intent.GetParameter("car_number"));
    //         
    //         return result ? "✅ Parking reservation cancelled" : "❌ Could not find reservation to cancel";
    //     }
    // }

    // private async Task<string> CheckParkingAvailabilityAsync(BookingIntent intent)
    // {
    //     var address = intent.GetParameter("address");
    //     var dateFrom = intent.GetDateParameter("date_from");
    //     var dateTo = intent.GetDateParameter("date_to");
    //
    //     if (dateFrom == null || dateTo == null)
    //         return "Please specify dates to check availability.";
    //
    //     var availableSpots = await _bookingRepository.CheckReservation(address, dateFrom.Value, dateTo.Value);
    //     
    //     return availableSpots > 0 ? 
    //         $"✅ Parking '{address}' has {availableSpots} available spots for the specified dates" :
    //         $"❌ Parking '{address}' has no available spots for the specified dates";
    // }

    // private async Task<string> ExtendParkingBookingAsync(BookingIntent intent)
    // {
    //     var bookingId = intent.GetParameter("booking_id");
    //     var newDateTo = intent.GetDateParameter("date_to");
    //
    //     if (newDateTo == null)
    //         return "Please specify the new end date for the rental.";
    //
    //     var result = await _bookingRepository.ExtendAsync(bookingId, newDateTo.Value);
    //     
    //     return result ? 
    //         $"✅ Parking rental extended until {newDateTo.Value:MM/dd/yyyy}" :
    //         "❌ Failed to extend the rental";
    // }
    //
    // private async Task<string> GetParkingPriceAsync(BookingIntent intent)
    // {
    //     var address = intent.GetParameter("address");
    //     var dateFrom = intent.GetDateParameter("date_from");
    //     var dateTo = intent.GetDateParameter("date_to");
    //     var parkingType = intent.GetParameter("parking_type", "standard");
    //
    //     if (dateFrom == null || dateTo == null)
    //         return "Please specify dates to calculate the price.";
    //
    //     var days = (dateTo.Value - dateFrom.Value).Days;
    //     var pricePerDay = await _bookingRepository.GetPriceAsync(address, parkingType);
    //     var totalPrice = days * pricePerDay;
    //
    //     return $"💰 Parking rental cost:\n📍 {address}\n🏷 {parkingType}\n📅 {days} days\n💵 ${totalPrice} (${pricePerDay}/day)";
    // }
}