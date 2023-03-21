using System.Globalization;

namespace AlgoTecture.TelegramBot.Implementations;

public static class DateTimeParser
{
    public static DateTime? GetDateTime(DateTime? date, string time)
    {
        if (!date.HasValue) return null;
        if (string.IsNullOrEmpty(time)) return null;

        var isValidTime = DateTime.TryParse(time, CultureInfo.InvariantCulture, DateTimeStyles.None, out var targetTime);

        if (!isValidTime) return null;

        var targetDateTime = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, targetTime.Hour, targetTime.Minute, targetTime.Second);

        return targetDateTime;
    }
}