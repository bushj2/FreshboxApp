using FreshBox.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FreshBox.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class NotificationsPage : ContentPage
    {
        INotificationManager notificationManager;
        string notificationString = "pork";

        public NotificationsPage()
        {
            InitializeComponent();

            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationSent += (sender, eventArgs) =>//notification received - want notification sent
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };
        }

        void OnSendClick(object sender, EventArgs e)
        {
            //notificationNumber++;
            string title = $"Your {notificationString} is about to expire!";
            string message = $"Please eat or throw away your {notificationString}.";
            notificationManager.SendNotification(title, message);
        }

        //where to put notifications so that it shows up in page when you click
        void OnScheduleClick(object sender, EventArgs e)
        {
            //notificationNumber++;
            string title = $"Your {notificationString} is about to expire!";
            string message = $"Please eat or throw away your {notificationString}.";
            notificationManager.SendNotification(title, message, DateTime.Now.AddSeconds(10)); //add seconds - how to schedule notifications for certain time?
        }
        
        //shows message - what triggers notification ???
        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Frame defaultFrame = new Frame
                {
                    CornerRadius = 20,
                    HasShadow = true,
                    Content = new Label() {
                        //FontAttributes = FontAttributes.Bold,
                        Text = $"{title}\n{message}",
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
                    }
                };

                //insert list of prev notifications -sort by most to least recent
                stackLayout.Children.Add(defaultFrame);
            });
        }
    }
    public class NotificationEventArgs : EventArgs
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}