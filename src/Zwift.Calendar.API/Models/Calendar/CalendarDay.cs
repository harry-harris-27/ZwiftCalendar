using System;
using System.Collections.Generic;
using System.Linq;

namespace Zwift.Calendar.API.Models.Calendar
{
    public class CalendarDay
    {
        public CalendarDay(byte day, IEnumerable<World> worlds)
        {
            // Add validation...

            this.Day = day;
            this.Worlds = worlds?.ToList() ?? new List<World>();
        }


        public byte Day { get; set; } = 1;

        public IEnumerable<World> Worlds { get; }
    }
}
