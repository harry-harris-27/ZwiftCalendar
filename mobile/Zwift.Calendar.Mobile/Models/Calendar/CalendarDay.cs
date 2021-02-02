using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Zwift.Calendar.Mobile.Models.Calendar
{
    /// <summary>
    /// Model representating a single day on the Zwift Guest World Calendar.
    /// </summary>
    public class CalendarDay
    {
        /// <summary>
        /// Gets or sets the day of the month that this model represents.
        /// </summary>
        [JsonPropertyName("day")]
        public int Day { get; set; }

        /// <summary>
        /// Gets or sets the worlds available on this day.
        /// </summary>
        [JsonPropertyName("worlds")]
        public List<CalendarWorld> Worlds { get; set; }
    }
}
