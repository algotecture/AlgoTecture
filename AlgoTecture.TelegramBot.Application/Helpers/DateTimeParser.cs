using System.Globalization;

namespace AlgoTecture.TelegramBot.Application.Helpers;

public class DateTimeParser
{
    public static DateTime? GetDateTimeUtc(DateTime? date, string time)
    {
        if (!date.HasValue) return null;
        if (string.IsNullOrEmpty(time)) return null;

        string[] formats = {
            "HH:mm", "H:mm",    // 12:00, 9:30
            "HH.mm", "H.mm",    // 12.00, 9.30
            "h:mm tt", "h.mm tt" // 12:00 PM, 12.00 PM
        };

        var isValidTime = DateTime.TryParseExact(time, formats,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var targetTime);
        
        if (!isValidTime) return null;

        var targetDateTime = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, targetTime.Hour, targetTime.Minute, targetTime.Second);

        return targetDateTime.ToUniversalTime();
    }
}
