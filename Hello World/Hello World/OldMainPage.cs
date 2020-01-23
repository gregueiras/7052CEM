using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Essentials;

namespace Hello_World
{
    public partial class OldMainPage : ContentPage
    {
        Entry phoneNumberText;
        Button translateButton;
        Button callButton;
        string translatedNumber;

        public OldMainPage()
        {
            this.Padding = new Thickness(20, 20, 20, 20);

            StackLayout panel = new StackLayout
            {
                Spacing = 15
            };

            panel.Children.Add(new Label
            {
                Text = "Enter a Phoneword:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            panel.Children.Add(phoneNumberText = new Entry
            {
                Text = "1-855-XAMARIN",
            });

            panel.Children.Add(translateButton = new Button
            {
                Text = "Translate"
            });

            panel.Children.Add(callButton = new Button
            {
                Text = "Call",
                IsEnabled = false,
            });

            translateButton.Clicked += OnTranslate;
            callButton.Clicked += OnCall;

            this.Content = panel;
        }

        async public void OnTranslate(object obj, EventArgs eventArgs)
        {
            var phoneText = this.phoneNumberText.Text;

            translatedNumber = PhonewordTranslator.ToNumber(phoneText);

            if (!string.IsNullOrEmpty(translatedNumber))
            {
                callButton.IsEnabled = true;
                callButton.Text = $"Call {translatedNumber}";
            }
            else
            {
                await DisplayAlert("Invalid", "Invalid number", "OK");
                callButton.IsEnabled = false;
                callButton.Text = $"Call";
            }
        }

        async public void OnCall(object obj, EventArgs eventArgs)
        {
            if (
                await DisplayAlert("Dial a number", $"Do you want to call {translatedNumber}?", "Yes", "No")
                )
            {
                try
                {
                    PhoneDialer.Open(translatedNumber);
                    System.Diagnostics.Debug.WriteLine("DIALING");
                }
                catch (ArgumentNullException anEx)
                {
                    // Number was null or white space
                    await DisplayAlert("Exception", $"Exception, NULL: {anEx.ToString()}", "OK");
                }
                catch (FeatureNotSupportedException ex)
                {
                    // Phone Dialer is not supported on this device.
                    await DisplayAlert("Exception", $"Exception, NO FEATURE: {ex.ToString()}", "OK");
                }
                catch (Exception ex)
                {
                    // Other error has occurred.
                    await DisplayAlert("Exception", $"Exception: {ex.ToString()}", "OK");
                }
            }
            else
            {
                await DisplayAlert("NO", "NO", "OK");
            }
        }
    }
}