using System.Collections.ObjectModel;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
using FreshBox.Models;

namespace FreshBox.ViewModels
{

    public class AboutViewModel : ContentPage
    {
        readonly IList<DateTime> source;

        //ExpTodayTitle isnt working, idk why, will fix later
        public String ExpTodayTitle { get; private set; }
        public ObservableCollection<DateTime> Dates { get; private set; }
        public ObservableCollection<Item> Items { get; private set; }
        public ObservableCollection<ObservableCollection<Item>> ItemsExpToday { get; private set; }

        public AboutViewModel()
        {
            source = new List<DateTime>();
            source.Add(new DateTime(2021, 09, 21));
            source.Add(new DateTime(2021, 09, 22));

            ExpTodayTitle = "Items Expiring Today";
            Dates = new ObservableCollection<DateTime>(source);
        }

        
    
    }
}
