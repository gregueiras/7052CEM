﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hello_World
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new OldMainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
