﻿using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IoTProtect.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            CheckShouldLoginOrSignup();
        }

        private void CheckShouldLoginOrSignup()
        {
            //var nav = new NavigationPage(new DevicesPage());
            //Navigation.PushModalAsync(nav);
            //nav.BarTextColor = Color.Red;
        }
    }
}