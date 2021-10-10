using FreshBox.Models;
using System;
using System.IO;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoSync;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Amazon; 
using Amazon.CognitoSync.Model;

namespace FreshBox
{
    public partial class App : Application
    {
        private static FreshBoxDatabase freshBoxDatabase;

        public static FreshBoxDatabase FreshBoxDatabase
        { 
            get
            {
                if (freshBoxDatabase == null)
                {
                    freshBoxDatabase = new FreshBoxDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FreshBoxes.db"));
                }
                return freshBoxDatabase;
            }
        }

        private static ItemDatabase itemDatabase;

        public static ItemDatabase ItemDatabase
        {
            get
            {
                if(itemDatabase == null)
                {
                    itemDatabase = new ItemDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FreshBoxItems.db"));
                }
                return itemDatabase;
            }
        }
        public static AWSUser user;
        public static AmazonCognitoIdentityProviderClient provider;

        public App()
        {
            InitializeComponent();
            provider = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), AWS.RegionEndpoint);
            MainPage = new AppShell();
        }

        protected async override void OnStart()
        {
            string secureUser = await SecureStorage.GetAsync("User");
            if (secureUser != null)
            {
                user = JsonConvert.DeserializeObject<AWSUser>(secureUser);
            }
            else
            {
                user = null;
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
