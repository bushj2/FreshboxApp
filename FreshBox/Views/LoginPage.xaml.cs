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
using Plugin.Fingerprint;
using Xamarin.Essentials;
using Newtonsoft.Json;
using Amazon.Extensions.CognitoAuthentication;
using Plugin.Fingerprint.Abstractions;

namespace FreshBox.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            //BindingContext = new LoginViewModel();
            //var handler = new JwtSecurityTokenHandler();
            //var tokenS = handler.ReadToken(App.user.IdToken) as JwtSecurityToken;
            //lstToken.ItemsSource = tokenS.Claims.ToList();
            //string userSub = tokenS.Claims.First(claim => claim.Type == "sub").Value;

            //lblTitle.Text = "WELCOME " + tokenS.Claims.First(claim => claim.Type == "cognito:username").Value.ToUpper();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (App.user != null)
            {
                AuthenticationRequestConfiguration biometricConfig = new AuthenticationRequestConfiguration("Use your fingerprint to log in", "");

                var resp = await CrossFingerprint.Current.AuthenticateAsync(biometricConfig);

                if (resp.Authenticated)
                {
                    string r = await RefreshToken(App.user.IdToken, App.user.AccessToken, App.user.RefreshToken, App.user.TokenIssued, App.user.Expires);
                    if (r == "Refreshed")
                    {
                        await SecureStorage.SetAsync("User", JsonConvert.SerializeObject(App.user));
                        await Navigation.PushAsync(new LoginPage());
                        Navigation.RemovePage(this);
                    }
                }
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string v = await SignIn(UserNameTextBox.Text, PasswordTextBox.Text);
            if (!v.StartsWith("Error"))
            {
                await SecureStorage.SetAsync("User", JsonConvert.SerializeObject(App.user));
                await Navigation.PushAsync(new Logged());
                Navigation.RemovePage(this);
            }
            else if (v == "pass-change-required")
                await DisplayAlert("Password Change Required", "Check your email.", "Ok");
            else if (v.StartsWith("Error"))
                await DisplayAlert("Error", "Login Error. " + v, "Ok");
        }

        public async Task<string> SignIn(string userName, string password)
        {
            try
            {
                CognitoUserPool userPool = new CognitoUserPool(AWS.UserpoolID, AWS.ClientID, App.provider);
                CognitoUser user = new CognitoUser(userName, AWS.ClientID, userPool, App.provider);

                AuthFlowResponse context = await user.StartWithSrpAuthAsync(new InitiateSrpAuthRequest()
                {
                    Password = password
                }).ConfigureAwait(false);

                if (context.ChallengeName == ChallengeNameType.NEW_PASSWORD_REQUIRED)
                    return "pass-change-required";
                else
                {
                    App.user = new AWSUser
                    {
                        IdToken = context.AuthenticationResult?.IdToken,
                        RefreshToken = context.AuthenticationResult?.RefreshToken,
                        AccessToken = context.AuthenticationResult?.AccessToken,
                        TokenIssued = user.SessionTokens.IssuedTime,
                        Expires = user.SessionTokens.ExpirationTime
                    };
                    return "Logged in";
                }
            }
            catch (Exception e)
            {
                return "Error " + e.Message;
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Logged());
        }

        public async Task<string> RefreshToken(string idToken, string accessToken, string refreshToken, DateTime issued, DateTime expires)
        {
            try
            {
                CognitoUserPool userPool = new CognitoUserPool(AWS.UserpoolID, AWS.ClientID, App.provider);
                CognitoUser user = new CognitoUser("", AWS.ClientID, userPool, App.provider)
                {
                    // We have to use expire time of REFRESH TOKEN not ACCESS TOKEN, now forcing refresh adding 1h as we don't have REFRESH TOKEN expire date
                    SessionTokens = new CognitoUserSession(idToken, accessToken, refreshToken, issued, DateTime.Now.AddHours(1))
                };

                AuthFlowResponse context = await user.StartWithRefreshTokenAuthAsync(new InitiateRefreshTokenAuthRequest
                {
                    AuthFlowType = AuthFlowType.REFRESH_TOKEN_AUTH
                })
                .ConfigureAwait(false);

                App.user = new AWSUser
                {
                    IdToken = context.AuthenticationResult?.IdToken,
                    RefreshToken = App.user.RefreshToken,
                    AccessToken = context.AuthenticationResult?.AccessToken,
                    TokenIssued = user.SessionTokens.IssuedTime,
                    Expires = user.SessionTokens.ExpirationTime
                };
                return "Refreshed";
            }
            catch (Exception e)
            {
                return "Error " + e.Message;
            }
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            if (App.user != null)
            {
                AuthenticationRequestConfiguration biometricConfig = new AuthenticationRequestConfiguration("Use your fingerprint to log in", "");

                var resp = await CrossFingerprint.Current.AuthenticateAsync(biometricConfig);

                if (resp.Authenticated)
                {
                    string r = await RefreshToken(App.user.IdToken, App.user.AccessToken, App.user.RefreshToken, App.user.TokenIssued, App.user.Expires);
                    if (r == "Refreshed")
                    {
                        await SecureStorage.SetAsync("User", JsonConvert.SerializeObject(App.user));
                        await Navigation.PushAsync(new Logged());
                        Navigation.RemovePage(this);
                    }
                }
            }
        }
    }
}