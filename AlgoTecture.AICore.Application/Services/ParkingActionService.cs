using AlgoTecture.AICoreService.Application.Models;

public class ParkingActionService
{
    public async Task<ParkingReservationModel> ExecuteActionAsync(RecognizedIntent intent)
    {
        if (!intent.IsValid)
        {
            //_logger.LogError(ex, "\"I didn't quite understand your request. Please specify: parking address, dates, and your vehicle license plate.\"", intent.Action);
        }

        try
        {
            //_logger.LogInformation("Executing parking action: {Action}", intent.Action);

            return intent.Action switch
            {
                "create_reservation" => await CreateParkingReservationModel(intent),
                _ => throw new NotImplementedException()
            };
        }
        catch (Exception ex)
        {
            //     _logger.LogError(ex, "Error executing parking action: {Action}", intent.Action);
        }

        return null!;
    }

    private async Task<ParkingReservationModel> CreateParkingReservationModel(RecognizedIntent intent)
    {
        var address = intent.GetParameter("address");
        var dateFrom = intent.GetDateParameter("start_datetime");
        var dateTo = intent.GetDateParameter("end_datetime");
        var carNumber = intent.GetParameter("car_number");
        var parkingType = intent.GetParameter("parking_type", "street");

        return new ParkingReservationModel
        {
            Action = intent.Action,
            Address = address,
            DateTimeFrom = dateFrom,
            DateTimeTo = dateTo,
            CarNumber = carNumber,
            ParkingType = parkingType,
        };
    }
}