using System;
using System.Collections.Generic;

namespace Zwift.Calendar.API.Models.Calendar
{
    public class CalendarMonth
    {
        public CalendarMonth(int year, int month, IEnumerable<CalendarDay> days)
        {
            // Add validations...

            this.Year = year;
            this.Month = month;
            this.Days = days;
        }

        public int Year { get; }
        public int Month { get; }
        public IEnumerable<CalendarDay> Days { get; }
    }
}
