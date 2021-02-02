using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zwift.Calendar.Mobile.Views.Shell
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShellView : Xamarin.Forms.Shell
    {
        public ShellView()
        {
            InitializeComponent();
        }
    }
}