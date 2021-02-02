using Caliburn.Micro;
using Microsoft.Extensions.Logging;
using System;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Plugin.Calendar.Models;
using Zwift.Calendar.Mobile.Models.Calendar;
using Zwift.Calendar.Mobile.Services.Calendar;

namespace Zwift.Calendar.Mobile.ViewModels.Calendar
{
    [Export]
    public class CalendarViewModel : Screen
    {

        private readonly ICalendarService calendarService;
        private readonly ILogger<CalendarViewModel> logger;

        private CancellationTokenSource refreshCancellationTokenSource = null;

        // Bindable property backing stores


        [ImportingConstructor]
        public CalendarViewModel(ICalendarService calendarService, ILogger<CalendarViewModel> logger)
        {
            this.calendarService = calendarService ?? throw new ArgumentNullException(nameof(calendarService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            TodayCommand = new RelayCommand(SelectToday);
            RefreshCommand = new RelayCommand(RefreshCalendar);

            DisplayName = "Calendar";
        }


        public bool IsRefreshing => refreshCancellationTokenSource != null;

        public DateTime MinimumDate => calendarService.MinimumDate;

        public EventCollection Events { get; } = new EventCollection();

        private DateTime _monthYear = DateTime.Today;
        public DateTime MonthYear
        {
            get => _monthYear;
            set
            {
                Set(ref _monthYear, value);
                RefreshCalendar();
            }
        }

        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate { get => _selectedDate; set => Set(ref _selectedDate, value); }

        public ICommand TodayCommand { get; }
        public ICommand RefreshCommand { get; }


        protected override void OnActivate()
        {
            RefreshCalendar();
        }


        private void SelectToday()
        {
            SelectedDate = DateTime.Today;
            MonthYear = SelectedDate;
        }

        private void RefreshCalendar()
        {
            // If there is current a refresh task in progress, let it continue
            if (refreshCancellationTokenSource != null) return;

            // Flag as now refreshing
            refreshCancellationTokenSource = new CancellationTokenSource();
            NotifyOfPropertyChange(() => IsRefreshing);

            calendarService.GetCalendarAsync(MonthYear.Year, MonthYear.Month, refreshCancellationTokenSource.Token).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    logger.LogError(task.Exception, "Exception throw while fetching calendar");
                }
                else if (!task.IsCanceled)
                {
                    var calendar = task.Result;
                    DateTime calendarBaseDate = new DateTime(calendar.Year, calendar.Month, 1);

                    foreach (var calendarDay in calendar.Days)
                    {
                        if (calendarDay.Worlds.Any())
                        {
                            DateTime calendarDayDate = calendarBaseDate.AddDays(calendarDay.Day - 1);
                            Events[calendarDayDate] = calendarDay.Worlds;
                        }
                    }
                }

                refreshCancellationTokenSource.Dispose();
                refreshCancellationTokenSource = null;

                NotifyOfPropertyChange(() => IsRefreshing);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
