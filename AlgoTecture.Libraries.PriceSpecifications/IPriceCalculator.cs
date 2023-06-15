using AlgoTecture.Domain.Enum;

namespace AlgoTecture.Libraries.PriceSpecifications;

public interface IPriceCalculator
{
    string CalculateTotalPriceToReservation(DateTime from, DateTime to, UnitOfDateTime unitOfDateTime, string price);
}