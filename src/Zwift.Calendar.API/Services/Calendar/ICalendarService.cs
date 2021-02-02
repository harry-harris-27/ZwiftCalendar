using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zwift.Calendar.API.Models.Calendar;

namespace Zwift.Calendar.API.Services.Calendar
{
    public interface ICalendarService
    {

        Task<CalendarMonth> GetCalendarAsync(int year, int month);

    }
}
