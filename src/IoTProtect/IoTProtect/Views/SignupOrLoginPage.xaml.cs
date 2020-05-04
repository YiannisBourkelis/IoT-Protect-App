using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace IoTProtect.Views
{
    public partial class SignupOrLoginPage : ContentPage
    {

        public SignupOrLoginPage()
        {
            InitializeComponent();
        }

        void btnNewAccount_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SignupPage());
        }
    }
}
