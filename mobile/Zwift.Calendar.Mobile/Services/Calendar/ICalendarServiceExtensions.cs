using System;
using System.Threading;
using System.Threading.Tasks;
using Zwift.Calendar.Mobile.Models.Calendar;

namespace Zwift.Calendar.Mobile.Services.Calendar
{
    public static class ICalendarServiceExtensions
    {
        public static Task<CalendarMonth> GetCalendarAsync(this ICalendarService calendarService, CancellationToken cancellationToken = default)
            => calendarService.GetCalendarAsync(DateTime.Now.Month, cancellationToken);

        public static Task<CalendarMonth> GetCalendarAsync(this ICalendarService calendarService, int month, CancellationToken cancellationToken = default)
            => calendarService.GetCalendarAsync(DateTime.Now.Year, month, cancellationToken);
    }
}
