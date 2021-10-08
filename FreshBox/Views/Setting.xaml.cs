using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FreshBox.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Setting : ContentPage
    {
        public Setting()
        {
            InitializeComponent();
        }
        async void AccountInfoNav(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AccountInfo());
        }

        async void PreferencesNav(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Preferences());
        }

        async void AppDetailsNav(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AppDetails());
        }
    }
}