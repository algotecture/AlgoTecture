using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Domain.Models;
using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Libraries.GeoAdminSearch;
using AlgoTecture.Libraries.Spaces.Interfaces;
using AlgoTecture.Libraries.UtilizationTypes;
using AlgoTecture.TelegramBot.Controllers.Interfaces;
using AlgoTecture.TelegramBot.Interfaces;
using AlgoTecture.TelegramBot.Models;
using Deployf.Botf;
using Newtonsoft.Json;

namespace AlgoTecture.TelegramBot.Controllers;

public class MainController : BotController, IMainController
{
    private readonly ITelegramUserInfoService _telegramUserInfoService;
    private readonly IUtilizationTypeGetter _utilizationTypeGetter;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MainController> _logger;
    private readonly IGeoAdminSearcher _geoAdminSearcher;
    private readonly ITelegramToAddressResolver _telegramToAddressResolver;
    private readonly ISpaceGetter _spaceGetter;
    private readonly IServiceProvider _serviceProvider;
    private readonly IBoatController _boatController;

    private Dictionary<string, string> utilizationTypeToSmile = new()
    {
        { "Residential", "🏠" }, { "Parking", "🚙" }, { "Boat", "🚤" }, { "Coworking", "🏢" }
    };

    public MainController(ITelegramUserInfoService telegramUserInfoService, IBoatController boatController, IUtilizationTypeGetter utilizationTypeGetter, 
        IUnitOfWork unitOfWork, ILogger<MainController> logger, IGeoAdminSearcher geoAdminSearcher, ITelegramToAddressResolver telegramToAddressResolver, 
        ISpaceGetter spaceGetter, IServiceProvider serviceProvider)
    {
        _telegramUserInfoService = telegramUserInfoService;
        _boatController = boatController;
        _utilizationTypeGetter = utilizationTypeGetter;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _geoAdminSearcher = geoAdminSearcher;
        _telegramToAddressResolver = telegramToAddressResolver;
        _spaceGetter = spaceGetter;
        _serviceProvider = serviceProvider;
    }

    [Action("/start", "start the bot")]
    public async Task Start()
    {
        var chatId = Context.GetSafeChatId();
        var userId = Context.GetSafeUserId();
        var userName = Context.GetUsername();
        var fullUserName = Context.GetUserFullName();
        
        var addTelegramUserInfoModel = new AddOrUpdateTelegramUserInfoModel
        {
            TelegramUserId = userId,
            TelegramChatId = chatId,
            TelegramUserName = userName,    
            TelegramUserFullName = fullUserName
        };

        var user = await _telegramUserInfoService.AddOrUpdate(addTelegramUserInfoModel);
        
        _logger.LogInformation($"User {user.TelegramUserFullName} logged in by telegram bot");
        
        PushL("I am your assistant 💁‍♀️ in searching and renting sustainable spaces around the globe 🌍 (test mode)");

        RowButton("🔍 Explore & 📌 Reserve Spaces", Q(PressToRentButton));
        RowButton("📅 Control & 📝 Manage Reservations", Q(PressToFindReservationsButton));
    }
    
    [Action]
    public async Task PressToRentButton()
    {
        //only for demo
        var utilizationTypes = (await _utilizationTypeGetter.GetAll()).Where(x=> (new List<string>(){"Residential", "Parking", "Boat", "Coworking"})
            .Contains(x.Name)).ToList();
        
        foreach (var utilizationType in utilizationTypes)
        {
            var utilizationTypeOut = new UtilizationTypeToTelegramOut
            {
                Name = utilizationTypeToSmile.TryGetValue(utilizationType.Name, out var smile) ? utilizationType.Name + " " + smile : utilizationType.Name,
                Id = utilizationType.Id
            };

            var botState = new BotState { UtilizationTypeId = utilizationType.Id, UtilizationName = utilizationType.Name, MessageId = default };

            //it is not yet known what to do with the rest of the types only!
            RowButton(utilizationTypeOut.Name,
                utilizationType.Name == "Boat" ? Q(_boatController.PressToMainBookingPage, botState) : Q(PressToCommonButtonToAnotherUtilizationTypes, botState));
        }
        RowButton("Go Back", Q(Start));

        PushL("Choose your type");
        await SendOrUpdate();
    }

    [Action]
    public async Task PressToCommonButtonToAnotherUtilizationTypes(BotState botState)
    {
        RowButton("Enter address", Q(EnterAddress));
        RowButton("Go Back", Q(PressToRentButton));

        if (botState.UtilizationName != null) 
            PushL(botState.UtilizationName);
        await SendOrUpdate();
    }
    

    [Action]
    public async Task PressToFindReservationsButton()
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var user = await _unitOfWork.Users.GetByTelegramChatId(chatId.Value);
        var reservations = await _unitOfWork.Reservations.GetReservationsByUserId(user.Id);

        var reservationList = new List<ReservationToTelegramOut>();

        foreach (var reservation in reservations)
        {
            var reservationToTelegram = new ReservationToTelegramOut
            {
                Id = reservation.Id,
                DateTimeFrom = $"{reservation.ReservationFromUtc!.Value:dd-MM-yyyy HH:mm} utc",
                Description = reservation.Description,
                TotlaPrice = reservation.TotalPrice,
                PriceCurrency = reservation.PriceSpecification?.PriceCurrency,
                Address = reservation.Space?.SpaceAddress
            };
            reservationList.Add(reservationToTelegram);
            var description =
                $"{reservationToTelegram.Address}, \n\r{reservationToTelegram.DateTimeFrom}, {reservationToTelegram.TotlaPrice} \n\r" +
                $"{reservationToTelegram.PriceCurrency?.ToUpper()}";
            RowButton(description, Q(PressToManageContract));
        }
        RowButton("Go Back", Q(Start));
        
        PushL("Reservations");
        await SendOrUpdate();
    }
    
    [Action]
    public void PressToManageContract()
    {
      return;
    }
    
     [Action]
     private async Task PressAddressToRentButton(TelegramToAddressModel telegramToAddressModel)
     {
         var chatId = Context.GetSafeChatId();
         if (!chatId.HasValue) return;
         
         var targetAddress = _telegramToAddressResolver.TryGetAddressListByChatId(chatId.Value)!.FirstOrDefault(x => x.FeatureId == telegramToAddressModel.FeatureId);

         var targetSpace = await _spaceGetter.GetByCoordinates(targetAddress!.latitude, targetAddress.longitude);

         _telegramToAddressResolver.RemoveAddressListByChatId(chatId.Value);
         
         if (targetSpace == null)
         {
             var formattedGeoAdminFeatureId = !string.IsNullOrEmpty(telegramToAddressModel.FeatureId) ? telegramToAddressModel.FeatureId.Split('_')[0] : string.Empty;
             PushL("This space will soon be available for rent. Go to space properties or /start to try again");
             
             var urlToAddressProperties = $"https://algotecture.io/webapi-qrcode/spacePropertyPage?featureId={formattedGeoAdminFeatureId}&label={telegramToAddressModel.Address}";
             RowButton("Go to space properties", urlToAddressProperties);
             await SendOrUpdate();
         }
         else
         {
             var targetSpaceProperty = JsonConvert.DeserializeObject<SpaceProperty>(targetSpace.SpaceProperty);
         
             var boatControllerService = _serviceProvider.GetRequiredService<IBoatController>();
         
             RowButton("Rent!", Q(boatControllerService.PressToEnterTheStartEndTime, new BotState{SpaceId = targetSpace.Id, 
                 SpaceName = targetSpaceProperty!.Name}, RentTimeState.None, null!));
             RowButton("Go to main", Q(Start));
         
             PushL($"Found! {targetSpace.UtilizationType?.Name}: {targetSpaceProperty?.Name}. {targetSpaceProperty?.Description}");
         }
     }
     [Action]
     private async Task EnterAddress()
     {
         var chatId = Context.GetSafeChatId();
         if (!chatId.HasValue) return;
         
         PushL("Enter the address or part of the address");
         await SendOrUpdate();
         
         var address = await AwaitText(() => Send("Text input timeout. Use /start to try again"));

         var user = await _unitOfWork.TelegramUserInfos.GetByTelegramChatId(chatId.Value);
         
         _logger.LogInformation($"User {user?.TelegramUserFullName} entered text {address} to search for an address");
        
         var telegramToAddressList = new List<TelegramToAddressModel>();

         var labels = (await _geoAdminSearcher.GetAddress(address)).ToList();
        
         foreach (var label in labels)
         {
             var telegramToAddressModel = new TelegramToAddressModel
             {
                 FeatureId = label.featureId,
                 latitude = label.lat,
                 longitude = label.lon,
                 Address = label.label
             };
             telegramToAddressList.Add(telegramToAddressModel);
             RowButton(label.label, Q(PressAddressToRentButton, telegramToAddressModel));
         }

         if (!labels.Any())
         {
             RowButton("Try again"!);
             await Send("Nothing found");
         }
         else
         {
             _telegramToAddressResolver.TryAddCurrentAddressList(chatId.Value, telegramToAddressList);

             if (_telegramToAddressResolver.TryGetAddressListByChatId(chatId.Value)!.Count > 1)
             {
                 await Send("Choose the right address");   
             }
             else
             {
                 await Send("Address");   
             }
         }
     }
     
     [Action]
     private async Task RedirectToAddressPropertiesButton(string geoAdminFeatureId)
     {
         var chatId = Context.GetSafeChatId();
         if (!chatId.HasValue) return;

        
     }
     
 
    [On(Handle.Exception)]
    public void Exception(Exception ex)
    {
        _logger.LogError(ex, "Handle.Exception on telegram-bot");
    }

    [On(Handle.Unknown)]
    public async Task Unknown()
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        RowButton("Enter address", Q(EnterAddress));
        RowButton("Go back", Q(Start));
        
        PushL("I'm sorry, but I'm not yet able to understand natural language requests at the moment. Enter an address to search for the space");
        await SendOrUpdate();
    }
    
    [On(Handle.ChainTimeout)]
    void ChainTimeout(Exception ex)
    {
        _logger.LogError(ex, "Handle.Exception on telegram-bot");
    }
}