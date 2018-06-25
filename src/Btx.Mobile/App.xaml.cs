using System;
using Btx.Client.Application.Persistance;
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
        
        public static BtxChatManager ChatManager { get; set; } = new BtxChatManager();

        public static App Instance { get; private set; }

		public App ()
		{

			InitializeComponent();

            //BtxDbContext.InitDatabase();

            SetLoggedOutPage();

            if (Device.RuntimePlatform == Device.iOS)
            {
                MainPage.Padding = new Thickness(0, 40, 0, 0);
                
            }

            Instance = this;
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

        public void SetLoggedInPage()
        {
            
            this.MainPage = new MasterDetailPage()
            {
                Master = new MenuPage(),
                Detail = GetNavigationPage(new ChatListPage()),
            };
        }

        public void SetLoggedOutPage()
        {
            MainPage = new MasterDetailPage()
            {
                Master = new LogoutMenuPage(),
                Detail = GetNavigationPage(new LogoutPage()),
            };
        }


        public NavigationPage GetNavigationPage(Page page)
        {
            return new NavigationPage(page)
            {

            };
        }

    }
}
