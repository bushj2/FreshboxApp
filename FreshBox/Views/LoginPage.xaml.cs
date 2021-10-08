using FreshBox.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FreshBox.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}