using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zwift.Calendar.API.Models.Calendar;

namespace Zwift.Calendar.API.Services.Calendar
{
    public static class ICalendarServiceExtensions
    {
        public static Task<CalendarMonth> GetCalendarAsync(this ICalendarService calendarService)
        {
            var current = DateTime.Now;
            return calendarService.GetCalendarAsync(current.Year, current.Month);
        }
    }
}
