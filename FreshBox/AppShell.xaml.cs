using System;

namespace FreshBox
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Current.GoToAsync("//LoginPage");
        }
    }
}
