using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Zwift.Calendar.Mobile.Models.Calendar
{
    /// <summary>
    /// Model representing a single month in the Zwift Guest World calendar.
    /// </summary>
    public class CalendarMonth
    {
        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        [JsonPropertyName("year")]
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the month within the year this <see cref="CalendarMonth"/> respresents.
        /// </summary>
        /// <remarks>
        /// One-indexed, i.e. Jan = 1, Feb = 2...
        /// </remarks>
        [JsonPropertyName("month")]
        public int Month { get; set; }

        /// <summary>
        /// Gets or sets the days that make up this month.
        /// </summary>
        [JsonPropertyName("days")]
        public List<CalendarDay> Days { get; set; }
    }
}
