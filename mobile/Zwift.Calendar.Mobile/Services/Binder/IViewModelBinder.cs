using System;
using Xamarin.Forms;

namespace Zwift.Calendar.Mobile.Services.Binder
{
    public interface IViewModelBinder
    {

        void Bind(BindableObject view, object viewModel = null);

        void Bind<TViewModel>(BindableObject view);
        void Bind<TViewModel>(BindableObject view, TViewModel viewModel);
    }
}
