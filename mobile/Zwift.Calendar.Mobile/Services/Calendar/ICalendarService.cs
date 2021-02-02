using System;
using System.Threading;
using System.Threading.Tasks;
using Zwift.Calendar.Mobile.Models.Calendar;

namespace Zwift.Calendar.Mobile.Services.Calendar
{
    public interface ICalendarService
    {

        DateTime MinimumDate { get; }


        Task<CalendarMonth> GetCalendarAsync(int year, int month, CancellationToken cancellationToken = default);
    }
}
