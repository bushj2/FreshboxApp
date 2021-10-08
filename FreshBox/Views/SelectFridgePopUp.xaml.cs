using FreshBox.Models;
using System;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms.Xaml;

namespace FreshBox.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectFridgePopUp : Popup
    {
        public SelectFridgePopUp()
        {
            InitializeComponent();
        }

        public async void AddButtonClicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                _ = await App.FreshBoxDatabase.SaveFreshBoxAsync(new FreshBoxes
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = nameEntry.Text,
                    Image = "Refrigerator.png" //image clicked on, passed in
                });

                nameEntry.Text = string.Empty;

                var result = await App.FreshBoxDatabase.GetFreshBoxesAsync();
                Dismiss(result);
            }
        }
    }
}