using System;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MockBlanck
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        byte[] originalHash = (new SHA256Managed()).ComputeHash(Encoding.UTF8.GetBytes("1234"));

        public LoginPage()
        {
            InitializeComponent();
        }

        private async System.Threading.Tasks.Task DisplayError() => await DisplayAlert("ERROR", "Invalid Password", "OK");

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (passEntry.Text != null)
            {
                var alg = new SHA256Managed();
                byte[] hash = alg.ComputeHash(Encoding.UTF8.GetBytes(passEntry.Text));

                if (Convert.ToString(hash.ToString()) == Convert.ToString(originalHash))
                {
                    var newPage = new NavPage(new MainPage());

                    
                    await Navigation.PushAsync(newPage);
                }
                else
                {
                    await DisplayError();
                }
            } else
            {
                await DisplayError();
            }
            

        }
    }
}