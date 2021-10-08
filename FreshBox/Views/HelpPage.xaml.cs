using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;
using FreshBox.Services;
using FreshBox.Models;

namespace FreshBox.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelpPage : ContentPage
    {

        public HelpPage()
        {
            InitializeComponent();
        }
        void OnTextChanged(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            searchResults.ItemsSource = HelpDataStore.GetSearchResults(searchBar.Text);
        }

        private void SearchResults_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var txt = (HelpItem)e.SelectedItem;
            DisplayAlert("Item Selected", txt.Description, "ok");
        }

    }

}