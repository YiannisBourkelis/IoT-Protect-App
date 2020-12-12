using System;
using System.Collections.Generic;
using IoTProtect.ViewModels;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace IoTProtect.Views
{
    public partial class DevicesListPage : ContentPage
    {
        public DevicesListPage()
        {
            InitializeComponent();


            Task.Run(async () => await ((DevicesListViewModel)this.BindingContext).LoadItemsList());
        }

        void AddNewDeviceToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        async void DevicesListCollectionView_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            if (DevicesListCollectionView.SelectedItem == null)
                //γίνεται null όταν θέτω ProductsCollectionView.SelectedItem = null;
                return;

            var selectedDevice = DevicesListCollectionView.SelectedItem as IoTProtect.Models.Device;

            switch (selectedDevice.Type)
            {
                case Models.DeviceModelsEnum.SmokePhotoelectricFlameTemp_v1:
                    var page = new DeviceMeasurementsPage();
                    var viewmodel = page.BindingContext as DeviceMeasurementsViewModel;
                    viewmodel.Device = selectedDevice;

                    //((DeviceMeasurementsViewModel)page.BindingContext).Item = FastDeepCloner.DeepCloner.Clone((DocumentSeries)ItemsCollectionView.SelectedItem); ;
                    //((DeviceMeasurementsViewModel)page.BindingContext).OriginalItem = (DocumentSeries)ItemsCollectionView.SelectedItem;
                    await viewmodel.LoadItemsList();
                    await Navigation.PushAsync(page, true);
                    break;


                case Models.DeviceModelsEnum.EnvironmentalMonStation_v1:
                    var env_page = new EnvMonStationDeviceMeasurementsPage();
                    var env_viewmodel = env_page.BindingContext as EnvMonStationMeasurementsViewModel;
                    env_viewmodel.Device = selectedDevice;
                    await env_viewmodel.LoadItemsList();
                    await Navigation.PushAsync(env_page, true);
                    break;

                default:
                    break;
            }

            DevicesListCollectionView.SelectedItem = null;
        }
    }
}
