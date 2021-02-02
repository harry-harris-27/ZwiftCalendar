using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Zwift.Calendar.API.Services.Calendar;

namespace Zwift.Calendar.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CalendarController : ControllerBase
    {

        private readonly ICalendarService calendarService;
        private readonly ILogger<CalendarController> logger;


        public CalendarController(ICalendarService calendarService, ILogger<CalendarController> logger)
        {
            this.calendarService = calendarService ?? throw new ArgumentNullException(nameof(calendarService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int year = 0, [FromQuery] int month = 0)
        {
            // Set year/month to now if not already specified
            if (year == 0) year = DateTime.Now.Year;
            if (month == 0) month = DateTime.Now.Month;

            // Validate the month value
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month));
            }

            try
            {
                var calendar = await calendarService.GetCalendarAsync(year, month);
                return Ok(calendar);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retreive calendar for current month");
                throw ex;
            }
        }
    }
}
