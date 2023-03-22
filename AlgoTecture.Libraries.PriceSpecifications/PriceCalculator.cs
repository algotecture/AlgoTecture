using System.Globalization;
using AlgoTecture.Domain.Enum;

namespace AlgoTecture.Libraries.PriceSpecifications;

public static class PriceCalculator
{
    public static string CalculateTotalPriceToReservation(DateTime from, DateTime to, UnitOfDateTime unitOfDateTime, string price)
    {
        var totalPriceStr = string.Empty;
        
        if (unitOfDateTime != UnitOfDateTime.Hour) return totalPriceStr;
        
        var isValidPrice = double.TryParse(price, out var priceValue);
        
        if (!isValidPrice) return totalPriceStr;

        var timeSpan = to - from;
        var totalMinutes = (to - from).TotalMinutes;
        var pricePerMinute = priceValue / 60;
        var totalPrice = totalMinutes * pricePerMinute;
        totalPriceStr = Math.Round(totalPrice, 2).ToString(CultureInfo.InvariantCulture);

        return totalPriceStr;
    }
}