using Algotecture.Domain.Enum;

namespace Algotecture.Libraries.PriceSpecifications;

public interface IPriceCalculator
{
    string CalculateTotalPriceToReservation(DateTime from, DateTime to, UnitOfDateTime unitOfDateTime, string price);
}