using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;
using Zwift.Calendar.Mobile.ViewModels.Calendar;

namespace Zwift.Calendar.Mobile.ViewModels.Shell
{
    [Export]
    public class ShellViewModel : Screen
    {


        [ImportingConstructor]
        public ShellViewModel(CalendarViewModel calendar)
        {
            this.Calendar = calendar ?? throw new ArgumentNullException(nameof(calendar));

            Calendar.ActivateWith(this);
        }


        public CalendarViewModel Calendar { get; }

    }
}
