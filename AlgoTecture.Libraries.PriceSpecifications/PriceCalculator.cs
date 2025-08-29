using System.Globalization;
using Algotecture.Domain.Enum;

namespace Algotecture.Libraries.PriceSpecifications;

public  class PriceCalculator : IPriceCalculator
{
    public string CalculateTotalPriceToReservation(DateTime from, DateTime to, UnitOfDateTime unitOfDateTime, string price)
    {
        var totalPriceStr = string.Empty;
        //easy way for demo
        if (unitOfDateTime != UnitOfDateTime.Hour) return totalPriceStr;
        
        var isValidPrice = double.TryParse(price, out var priceValue);
        
        if (!isValidPrice) return totalPriceStr;
        
        var totalMinutes = (to - from).TotalMinutes;
        var pricePerMinute = priceValue / 60;
        var totalPrice = totalMinutes * pricePerMinute;
        totalPriceStr = Math.Round(totalPrice, 2).ToString(CultureInfo.InvariantCulture);

        return totalPriceStr;
    }
}