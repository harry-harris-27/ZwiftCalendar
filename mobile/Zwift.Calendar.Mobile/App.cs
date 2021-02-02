using Caliburn.Micro;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Zwift.Calendar.Mobile.Services.Binder;
using Zwift.Calendar.Mobile.ViewModels.Shell;

namespace Zwift.Calendar.Mobile
{
    [Export]
    public class App : Caliburn.Micro.Xamarin.Forms.FormsApplication
    {

        private readonly IPermissionsService permissionsService;
        private readonly IViewModelBinder viewModelBinder;
        private readonly ILogger<App> logger;

        private readonly ShellViewModel shellViewModel;

        [ImportingConstructor]
        public App(IPermissionsService permissionsService, IViewModelBinder viewModelBinder, ShellViewModel shellViewModel, ILogger<App> logger)
        {
            this.permissionsService = permissionsService ?? throw new ArgumentNullException(nameof(permissionsService));
            this.viewModelBinder = viewModelBinder ?? throw new ArgumentNullException(nameof(viewModelBinder));
            this.shellViewModel = shellViewModel ?? throw new ArgumentNullException(nameof(shellViewModel));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override void OnStart()
        {
            base.OnStart();

            CheckPermissions();

            var shellView = new Views.Shell.ShellView();
            viewModelBinder.Bind(shellView, shellViewModel);

            Application.Current.MainPage = shellView;

            (shellViewModel as IActivate)?.Activate();
        }

        protected override void CleanUp()
        {
            (shellViewModel as IDeactivate)?.Deactivate(true);

            base.CleanUp();
        }


        private void CheckPermissions()
        {
            //permissionsService.RequestAsync<Permissions.Net>
        }
    }
}
