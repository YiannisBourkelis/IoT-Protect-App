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

            var page = new DeviceMeasurementsPage();
            var viewmodel = page.BindingContext as DeviceMeasurementsViewModel;
            viewmodel.Device = DevicesListCollectionView.SelectedItem as IoTProtect.Models.Device;

            //((DeviceMeasurementsViewModel)page.BindingContext).Item = FastDeepCloner.DeepCloner.Clone((DocumentSeries)ItemsCollectionView.SelectedItem); ;
            //((DeviceMeasurementsViewModel)page.BindingContext).OriginalItem = (DocumentSeries)ItemsCollectionView.SelectedItem;
            await Navigation.PushAsync(page, true);
            await viewmodel.LoadItemsList();
            DevicesListCollectionView.SelectedItem = null;
        }
    }
}
