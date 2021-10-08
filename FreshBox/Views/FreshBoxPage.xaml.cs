using System;
using Xamarin.Forms;
using FreshBox.Models;
using Xamarin.CommunityToolkit.Extensions;

namespace FreshBox.Views
{
    public partial class FreshBoxPage : ContentPage
    {
        public FreshBoxPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.FreshBoxDatabase.GetFreshBoxesAsync();
        }

        private async void AddFreshBoxButtonClicked(object sender, EventArgs e)
        {
            //returns a task
            _ = await Navigation.ShowPopupAsync(new AddFreshBoxPopUp());
            collectionView.ItemsSource = await App.FreshBoxDatabase.GetFreshBoxesAsync();
        }

        private async void OnCollectionViewSelection(object sender, SelectionChangedEventArgs e)
        {
            //does nothing if current selection is not a FreshBox, otherwise navigates to relevant Items page
            if (!(e.CurrentSelection[0] is FreshBoxes freshBox))
                return;
            await Navigation.PushAsync(new ItemPage(freshBox));
        }

        private async void OnDeleteSwipeItem(object sender, EventArgs e)
        {
            SwipeItem swipeItem = sender as SwipeItem;
            if (!(swipeItem.BindingContext is FreshBoxes freshBox))
                return;
            _ = await App.FreshBoxDatabase.DeleteFreshBoxAsync(freshBox);
            collectionView.ItemsSource = await App.FreshBoxDatabase.GetFreshBoxesAsync();
            return;
        }
    }
}
