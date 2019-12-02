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
    public partial class AddNewPage : ContentPage
    {
        public AddNewPage()
        {
            InitializeComponent();
            BindingContext = new ViewModel.AddNewViewModel(Navigation);

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#00739a");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }

        public AddNewPage(Object prarmeters)
        {
            InitializeComponent();
            BindingContext = new ViewModel.AddNewViewModel(Navigation, prarmeters);
            this.Title = "Update note";

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#00739a");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }


    }
}