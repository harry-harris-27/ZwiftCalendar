using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Zwift.Calendar.API.Models.Calendar;

namespace Zwift.Calendar.API.Services.Calendar
{
    public class CalendarService : ICalendarService
    {

        private const string URL_BASE = "https://zwiftinsider.com/schedule/";


        private readonly ILogger<CalendarService> logger;


        public CalendarService(ILogger<CalendarService> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<CalendarMonth> GetCalendarAsync(int year, int month)
        {
            // Validate input...

            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;

                var requestedMonth = new DateTime(year, month, 1);
                string url = URL_BASE + string.Format("?month={0:MMM}&yr={0:yyyy}", requestedMonth).ToLowerInvariant();
                string content = await client.GetStringAsync(url);

                var days = ExtractCalendarDays(content);
                return new CalendarMonth(year, month, days);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retreive calendar page content");
            }

            return null;
        }


        private IEnumerable<CalendarDay> ExtractCalendarDays(string html)
        {
            // Create the HTML document and load in the read page HTML
            var document = new HtmlDocument();
            document.LoadHtml(html);

            // Try find the calendar table in the document
            var calendarTableNode = document.DocumentNode.Descendants("table")
                .Where(x => x.HasClass("calendar-table"))
                .FirstOrDefault();

            if (calendarTableNode != null)
            {                
                // Get the non-header rows from the table's body
                var dayNodes = calendarTableNode.Descendants("tr")
                    .Where(x => !x.HasClass("calendar-heading") && !x.HasClass("weekday-title"))
                    .SelectMany(x => x.Descendants("td"))
                    .Where(x => x.HasClass("day-with-date"));

                // Parse each day node, returns each successful result
                foreach (var dayNode in dayNodes)
                {
                    var day = ParseDay(dayNode);
                    if (day != null)
                    {
                        yield return day;
                    }
                }
            }
        }


        private CalendarDay ParseDay(HtmlNode dayNode)
        {
            var dayNumberNode = dayNode.Descendants("span")
                .Where(x => x.HasClass("day-number"))
                .FirstOrDefault();

            if (dayNumberNode != null)
            {
                if (byte.TryParse(dayNumberNode.InnerHtml, out byte day))
                {
                    var worldLinkNodes = dayNode.Descendants("span")
                        .Where(x => x.HasClass("calnk-box"))
                        .Select(x => x.Descendants("a").FirstOrDefault());

                    var worlds = new List<World>();
                    foreach (var worldLinkNode in worldLinkNodes)
                    {
                        try
                        {
                            string worldLink = worldLinkNode.GetAttributeValue("href", null);
                            string worldName = worldLinkNode.Descendants("span")
                                .Where(x => x.HasClass("spiffy-title"))
                                .Select(x => x.InnerHtml)
                                .FirstOrDefault();

                            if (!string.IsNullOrEmpty(worldLink) && !string.IsNullOrEmpty(worldName))
                            {
                                worlds.Add(new World(worldName, worldLink));
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "Failed to parse event node");
                        }
                    }

                    return new CalendarDay(day, worlds);
                }
            }

            return null;
        }

    }
}
