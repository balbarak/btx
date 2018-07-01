using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using FFImageLoading.Forms.Droid;
using System.Threading.Tasks;

namespace Btx.Mobile.Droid
{
    [Activity(Label = "Btx.Mobile", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            //Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;


            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Rg.Plugins.Popup.Popup.Init(this,bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);

            LoadApplication(new App());

            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException; ;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException; ;
            AndroidEnvironment.UnhandledExceptionRaiser += OnUnhandledExceptionRaiser;
        }

        private void OnUnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            //throw e.Exception;
        }

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            //throw e.Exception;
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            //PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            //base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

