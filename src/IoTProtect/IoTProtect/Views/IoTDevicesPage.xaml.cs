using System;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using IoTProtect.Models;

using Xamarin.Forms;

namespace IoTProtect.Views
{
    public partial class IoTDevicesPage : ContentPage
    {
        public IoTDevicesPage()
        {
            InitializeComponent();
        }

        void TextCell_Tapped(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        static bool firstAppear = false;
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!firstAppear)
            {
                firstAppear = true;
                iotDevicesViewModel.LoadDevicesCommand.Execute(null);
            }
        }

        void EditDescription_Clicked(System.Object sender, System.EventArgs e)
        {
            foreach (var d in iotDevicesViewModel.DevicesListContainer)
            {
                for (int i = 0; i < d.Count; i++)
                {
                    if (d[i].ID == 120)
                    {
                        //devicesListView.BeginRefresh();
                        //DeviceInfo n = new DeviceInfo() { ID = 120, Description = "hh gg ff ss" };
                        //d[i] = n;
                        d[i].Description = "fff fff fff";
                        //devicesListView.EndRefresh();
                    }
                }
            }
            //iotDevicesViewModel.DevicesDataStore.UpdateItemAsync(new DeviceInfo() { ID = 120 });
        }

        void EditLocation_Clicked(System.Object sender, System.EventArgs e)
        {

            //iotDevicesViewModel.DevicesListContainer[0].Add(new DeviceInfo() { Description = "insert existing location" });

            //DeviceInfoList d2 = iotDevicesViewModel.DevicesListContainer[0];
            DeviceInfoList d2 = new DeviceInfoList();


            d2.Add(new DeviceInfo() { Description = "aaaaa 2" });
            d2.Add(new DeviceInfo() { Description = "aaaaa 2" });

            d2.Heading = "neoooo" + "1";
            iotDevicesViewModel.DevicesListContainer.Add(d2);
            
        }

        void devicesListView_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            //dimiourgw to viewmodel gia to device detail page
            //kai thetw referense tis listas me ola ta device wste
            //na mporei na kanei update/delete sto epilegmeno device 
            var viewModel = new ViewModels.DeviceDetailViewModel(e.Item as DeviceInfo);
            viewModel.DeviceGroupList = iotDevicesViewModel.DevicesListContainer;
            Navigation.PushModalAsync(new Xamarin.Forms.NavigationPage(new DeviceDetailPage(viewModel)));
        }
        /*
        //add new location
        void EditLocation_Clicked(System.Object sender, System.EventArgs e)
        {

            iotDevicesViewModel.DevicesListContainer[0].Add(new DeviceInfo() { Description = "insert existing location" });

            DeviceInfoList d2 = new DeviceInfoList();

            d2.Add(new DeviceInfo() { Description = "aaaaa 2" });
            d2.Add(new DeviceInfo() { Description = "aaaaa 2" });

            d2.Heading = "Office";
            iotDevicesViewModel.DevicesListContainer.Add(d2);
        }
        */
    }
}
