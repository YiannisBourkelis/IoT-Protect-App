using System;
using System.Collections.Generic;
using IoTProtect.Models;
using IoTProtect.ViewModels;

using Xamarin.Forms;

namespace IoTProtect.Views
{
    public partial class DeviceDetailPage : ContentPage
    {
        DeviceDetailViewModel deviceDetailViewModel;

        public DeviceDetailPage(DeviceDetailViewModel deviceDetail_ViewModel)
        {
            InitializeComponent();
            this.BindingContext = deviceDetailViewModel = deviceDetail_ViewModel;
        }

        void Cancel_ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }
    }
}
