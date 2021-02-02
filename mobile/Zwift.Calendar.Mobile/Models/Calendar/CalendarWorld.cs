using System;
using System.Text.Json.Serialization;

namespace Zwift.Calendar.Mobile.Models.Calendar
{
    /// <summary>
    /// Model representing a specified Zwift guest world.
    /// </summary>
    public class CalendarWorld
    {
        /// <summary>
        /// Gets or sets the name of the zwift guest world.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the link to the Zwift Insider world info page.
        /// </summary>
        [JsonPropertyName("link")]
        public string Link { get; set; }
    }
}
