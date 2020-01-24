using System;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace People
{
    public partial class App : Application
    {
        string dbPath => FileAccessHelper.GetLocalFilePath("people.db3");
        public static PersonRepository PersonRepo;
        public static readonly string PASSWORD_KEY = "crypto_password";

        public App()
        {
            InitializeComponent();
            PersonRepo = new PersonRepository(dbPath);
            MainPage = new People.MainPage();
        }

        private async Task generateSecurePassword()
        {
            string password = await SecureStorage.GetAsync(PASSWORD_KEY);
            if (password is null)
            {
                password = Encoding.ASCII.GetString(CryptoHelper.Generate256BitsOfRandomEntropy());
                await SecureStorage.SetAsync(PASSWORD_KEY, password);
            }
        }

        protected override async void OnStart()
        {
            await generateSecurePassword();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
