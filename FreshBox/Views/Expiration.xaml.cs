using Xamarin.Forms;

namespace FreshBox.Views
{
    public partial class Expiration : ContentPage
    {
        public Expiration()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.ItemDatabase.GetItemsSortedByDate();
        }
    }
}