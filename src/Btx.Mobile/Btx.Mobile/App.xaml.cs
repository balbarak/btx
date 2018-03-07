using System;

using Btx.Mobile.Views;
using Xamarin.Forms;

namespace Btx.Mobile
{
	public partial class App : Application
	{

        public static ChatListPage ChatListPage { get; set; }

		public App ()
		{
			InitializeComponent();


            //MainPage = GetNavigationPage(new ChatBoxPage());
            MainPage = GetNavigationPage(new ChatListPage());
            //MainPage = new MainPage();
            
            //MainPage = new AttachmentPage("/storage/emulated/0/Android/data/com.companyname.Btx.Mobile/files/Pictures/temp/IMG_20180304_024511_8.jpg");

        }

        public NavigationPage GetNavigationPage(Page page)
        {
            
            return new NavigationPage(page)
            {
                
            };
        }


        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
