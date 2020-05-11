using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace IoTProtect.Views
{
    public partial class AddDevicePage : ContentPage
    {
        public AddDevicePage()
        {
            InitializeComponent();
        }

        void Cancel_ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }
    }
}
