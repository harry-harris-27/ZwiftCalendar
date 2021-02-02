using Caliburn.Micro.Xamarin.Forms;
using System;
using System.Composition;
using Xamarin.Forms;

namespace Zwift.Calendar.Mobile.Services.Binder
{
    [Export(typeof(IViewModelBinder))]
    public class CMViewModelBinder : IViewModelBinder
    {
        public void Bind(BindableObject view, object viewModel = null)
            => BindImpl(view, viewModel);

        public void Bind<TViewModel>(BindableObject view)
            => Bind(view, Caliburn.Micro.IoC.Get<TViewModel>());

        public void Bind<TViewModel>(BindableObject view, TViewModel viewModel)
            => BindImpl(view, viewModel);


        private void BindImpl(BindableObject view, object viewModel)
        {
            if (view != null)
            {
                ViewModelBinder.Bind(viewModel, view, null);
            }
        }
    }
}
