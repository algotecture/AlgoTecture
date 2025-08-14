namespace AlgoTecture.Libraries.Reservations.Models;

public class UpdateReservationStatusModel
{
    public long ReservationId { get; set; }

    public string ReservationStatus { get; set; } = null!;
}