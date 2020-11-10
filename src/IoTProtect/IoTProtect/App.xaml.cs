using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IoTProtect.Services;
using IoTProtect.Views;
using IoTProtect.Models;

namespace IoTProtect
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //Αρχικοποιηση του authenticated http client με το οποιο στέλνονται όλα τα requests
            //στον server. Πρέπει να είναι static και να διατηρείται στη μνύμη αλλιώς καθυστερεί
            //τη λήβη δεδομένων και αφήνει ανοιχτές ports.
            Api.InitAuthHttpClient();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MockDeviceInfoDatastore>();
            MainPage = new MainPage();
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
