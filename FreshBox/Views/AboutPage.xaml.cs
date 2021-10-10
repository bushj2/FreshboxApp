using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Newtonsoft.Json;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System.Threading.Tasks;
using Xamarin.Essentials;



namespace FreshBox.Views
{
    public partial class AboutPage : ContentPage
    {
        ZXingScannerPage scanPage;
        public AboutPage()
        {
            InitializeComponent();
            ButtonScan.Clicked += ButtonScan_Clicked;
        }

        private async void ButtonScan_Clicked(object sender, EventArgs e)
        {
            scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopModalAsync();
                    await DisplayAlert("Scanned Barcode", result.Text, "OK");
                });
            };

            await Navigation.PushModalAsync(scanPage);
        }

        //send user to FreshBoxes page on click from bottom navbar
        private async void FreshBoxPageNav(object sender, EventArgs e)
        {
             await Navigation.PushModalAsync(new FreshBoxPage());
        }
        //send user to Expiration page on click from bottom navbar
        private async void ExpirationPageNav(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Expiration());
        }

    }
}