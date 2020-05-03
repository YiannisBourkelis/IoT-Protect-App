using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace IoTProtect.Views
{
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        private bool firstAppear = true;
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (firstAppear)
            {
                txtEmail.Focus();
                firstAppear = false;
            }
        }

    }
}
