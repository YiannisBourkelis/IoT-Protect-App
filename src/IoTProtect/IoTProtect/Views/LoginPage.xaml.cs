using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace IoTProtect.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            loginViewModel.OnLoginCompleted += LoginViewModel_OnLoginCompleted;
        }

        private void LoginViewModel_OnLoginCompleted(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
