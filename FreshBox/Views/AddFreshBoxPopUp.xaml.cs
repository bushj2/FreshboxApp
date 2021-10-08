using System;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms.Xaml;

namespace FreshBox.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFreshBoxPopUp : Popup
    {
        public AddFreshBoxPopUp()
        {
            InitializeComponent();
        }

        private async void SelectFridgeButtonClicked(object sender, EventArgs e)
        {
            var result = await Navigation.ShowPopupAsync(new SelectFridgePopUp());
            Dismiss(result);
        }
    }
}