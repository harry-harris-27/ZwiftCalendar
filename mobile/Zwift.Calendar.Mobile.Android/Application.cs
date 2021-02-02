using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Caliburn.Micro;
using static Android.App.Application;

namespace Zwift.Calendar.Mobile.Droid
{
    [Application]
    public class Application : CaliburnApplication, IActivityLifecycleCallbacks
    {

        private const string AppCenterSecret = "d12eef83-54c6-402d-bba2-7d085fc51fea";

        private readonly AppBootstrapper bootstrapper = new AppBootstrapper(AppCenterSecret);


        public Application(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {

        }


        internal static Context CurrentContext { get; private set; }


        public override void OnCreate()
        {
            Initialize();

            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        protected override void BuildUp(object instance)
        {
            bootstrapper.BuildUp(instance);
        }

        protected override void Configure()
        {
            bootstrapper.Configure();
        }

        protected override object GetInstance(Type service, string key)
        {
            return bootstrapper.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return bootstrapper.GetAllInstances(service);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return bootstrapper.SelectAssemblies().Concat(new Assembly[]
            {

            });
        }

        #region IActivityLifecycleCallbacks

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CurrentContext = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {

        }

        public void OnActivityPaused(Activity activity)
        {

        }

        public void OnActivityResumed(Activity activity)
        {
            CurrentContext = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {

        }

        public void OnActivityStarted(Activity activity)
        {
            CurrentContext = activity;
        }

        public void OnActivityStopped(Activity activity)
        {

        }

        #endregion
    }
}