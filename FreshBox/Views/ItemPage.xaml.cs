using FreshBox.Models;
using System;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace FreshBox.Views
{
    public partial class ItemPage : ContentPage
    {
        private FreshBoxes FreshBox { get; }
        public ItemPage(FreshBoxes freshBox)
        {
            InitializeComponent();
            FreshBox = freshBox;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.ItemDatabase.GetItemsByBox(FreshBox.Id);
        }

        private async void AddItemButtonClicked(object sender, EventArgs e)
        {
            //returns a task
            _ = await Navigation.ShowPopupAsync(new AddItemPopup(FreshBox));
            collectionView.ItemsSource = await App.ItemDatabase.GetItemsByBox(FreshBox.Id);

        }
        private async void OnDeleteSwipeItem(object sender, EventArgs e)
        {
            SwipeItem swipeItem = sender as SwipeItem;
            if (!(swipeItem.BindingContext is Item item)) { return; }
            
            await App.ItemDatabase.DeleteItemAsync(item);
            collectionView.ItemsSource = await App.ItemDatabase.GetItemsByBox(FreshBox.Id);
            return;
        }
    }
}
