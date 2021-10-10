using FreshBox.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Amazon.CognitoIdentityProvider;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;

namespace FreshBox.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(App.user.IdToken) as JwtSecurityToken;
            lstToken.ItemsSource = tokenS.Claims.ToList();
            string userSub = tokenS.Claims.First(claim => claim.Type == "sub").Value;

            lblTitle.Text = "WELCOME " + tokenS.Claims.First(claim => claim.Type == "cognito:username").Value.ToUpper();
        }
    }
}