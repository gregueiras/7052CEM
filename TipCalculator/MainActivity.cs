﻿using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace TipCalculator
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText inputBill;
        Button calcButton;
        TextView outputTip;
        TextView outputTotal;
        private static readonly int TIP_AMOUNT = 20;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            inputBill = FindViewById<EditText>(Resource.Id.inputBill);
            calcButton = FindViewById<Button>(Resource.Id.buttonCalc);
            outputTip = FindViewById<TextView>(Resource.Id.outputTip);
            outputTotal = FindViewById<TextView>(Resource.Id.outputTotal);

            calcButton.Click += OnCalculateClick;
        }

        void OnCalculateClick(Object sender, EventArgs eventArgs)
        {
            try
            {
                string stringBill = inputBill.Text;
                double bill = Convert.ToDouble(stringBill);

                double tip = TIP_AMOUNT * bill / 100;
                double total = bill + tip;

                outputTip.Text = string.Format("{0:0.##}", tip);
                outputTotal.Text = string.Format("{0:0.##}", total);
            }
            catch (Exception e)
            {
                outputTotal.Text = e.ToString();
            }

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }



        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

