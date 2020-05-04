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

            mysignup.OnSigneupCompleted += Mysignup_OnSigneupCompleted;
        }

        private void Mysignup_OnSigneupCompleted(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
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

        void btnCreateAccount_Clicked(System.Object sender, System.EventArgs e)
        {

            //Navigation.PopModalAsync();
        }
    }
}
