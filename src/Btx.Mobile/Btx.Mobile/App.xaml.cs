using System;
using Btx.Mobile.Services;
using Btx.Mobile.Views;
using Xamarin.Forms;

namespace Btx.Mobile
{
	public partial class App : Application
	{
        public static MasterDetailPage MasterPage
        {
            get
            {
                return App.Current.MainPage as MasterDetailPage;
            }
        }
        public static ChatListPage ChatListPage { get; set; }
        
        public static BtxChatManager ChatManager { get; set; } = new BtxChatManager();

		public App ()
		{
			InitializeComponent();


            //MainPage = GetNavigationPage(new ChatBoxPage());

            MainPage = new MasterDetailPage()
            {
                Master = new MenuPage(),
                Detail = GetNavigationPage(new ChatListPage()),
            };

            if (Device.RuntimePlatform == Device.iOS)
            {
                MainPage.Padding = new Thickness(0, 40, 0, 0);
                
            }


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
