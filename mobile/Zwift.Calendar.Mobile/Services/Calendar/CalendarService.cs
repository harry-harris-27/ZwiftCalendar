using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zwift.Calendar.Mobile.Models.Calendar;
using Zwift.Calendar.Mobile.Services.RequestProvider;

namespace Zwift.Calendar.Mobile.Services.Calendar
{
    [Export(typeof(ICalendarService))]
    public class CalendarService : ICalendarService
    {

        private const string URL_BASE = "https://us-central1-zwiftcalendar-9e89c.cloudfunctions.net/calendar";

        private static readonly string[] months = { "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december" };
        private static readonly string[] abbreviatedMonths = { "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec" };

        // Dictionary to cache the results of a calendar requests in memory
        private readonly IDictionary<int, CalendarMonth> calendarCache = new Dictionary<int, CalendarMonth>();

        private readonly IRequestProvider requestProvider;
        private readonly ILogger<CalendarService> logger;


        [ImportingConstructor]
        public CalendarService(IRequestProvider requestProvider, ILogger<CalendarService> logger)
        {
            this.requestProvider = requestProvider ?? throw new ArgumentNullException(nameof(requestProvider));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        /// <inheritdoc/>
        public DateTime MinimumDate { get; } = new DateTime(2019, 11, 1);


        /// <inheritdoc/>
        public async Task<CalendarMonth> GetCalendarAsync(int year, int month, CancellationToken cancellationToken = default)
        {
            int calendarId = (year * 100) + month;
            if (!calendarCache.TryGetValue(calendarId, out CalendarMonth calendar))
            {
                string url = BuildCalendarUrl(year, month);

                calendar = await requestProvider.GetAsync<CalendarMonth>(url, cancellationToken);
                calendarCache.Add(calendarId, calendar);
            }
            
            return calendar;
        }


        /// <summary>
        /// Builds the calendar URL.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <returns></returns>
        private static string BuildCalendarUrl(int year, int month)
        {
            return URL_BASE + $"?year={year}&month={month}";
        }

        /// <summary>
        /// Gets the abbreviated month name of the month with the specified index.
        /// </summary>
        /// <param name="monthIdx">Index of the month (1-indexed).</param>
        /// <returns>The abbreviated name of the month with the specified index.</returns>
        private static string GetAbbreviatedMonth(int monthIdx)
        {
            return abbreviatedMonths[monthIdx - 1];
        }

        /// <summary>
        /// Gets the index of the specifed month (1-based).
        /// </summary>
        /// <param name="month">The full name of the month.</param>
        /// <returns>The 1-based index of the specified month.</returns>
        private static int GetMonthIndex(string month)
        {
            return Array.IndexOf(months, month.ToLowerInvariant()) + 1;
        }

    }
}
