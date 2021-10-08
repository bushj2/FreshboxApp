using FreshBox.Models;
using System;
using System.IO;
using Xamarin.Forms;

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

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
