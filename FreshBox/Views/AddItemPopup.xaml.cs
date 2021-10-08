using FreshBox.Models;
using System;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms.Xaml;

namespace FreshBox.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemPopup : Popup
    {
        private readonly FreshBoxes freshBox;
        public AddItemPopup(FreshBoxes freshBox)
        {
            InitializeComponent();
            this.freshBox = freshBox;
        }

        public async void AddItemButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                var result = await App.ItemDatabase.SaveItemAsync(new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = nameEntry.Text,
                    ExpiryDate = expiryDatePicker.Date,
                    FridgeId = freshBox.Id
                });

                nameEntry.Text = string.Empty;
                Dismiss(result);
            }
        }
    }
}