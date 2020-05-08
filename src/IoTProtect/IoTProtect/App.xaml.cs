using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IoTProtect.Services;
using IoTProtect.Views;

namespace IoTProtect
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

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
