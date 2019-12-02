using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XNotes.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new ViewModel.HomePageViewModel(Navigation);

            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#00739a");
            //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddNewPage());
        }
    }
}