using Realms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XNotes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new View.HomePage());
            NavigationPage page = App.Current.MainPage as NavigationPage;
            page.BarBackgroundColor = Color.FromHex("#00739a");
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
