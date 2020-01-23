using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace SmartHome
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            List<SmartDevice> list = new List<SmartDevice>(DeviceManager.Instance.Value.Devices);
            list.Sort((device1, device2) => device1.Name.CompareTo(device2.Name));
            var ctx = list.ToLookup((device) => device.Name[0].ToString());

            BindingContext = ctx;
        }
    }
}
