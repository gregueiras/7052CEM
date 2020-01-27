using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MockBlanck
{

    public class Entry
    {
        public double price { get; set; }
        public double miles { get; set; }
    }

    public class Vehicle
    {
        public string name { get; set; }
        public ObservableCollection<Entry> fuelExpenses { get; set; }
        public ObservableCollection<Entry> serviceExpenses { get; set; }
        public double averageFuelMile { get; set; }

        public Vehicle()
        {
            averageFuelMile = 4 / 2.0 + 4 / 3.0;

            fuelExpenses = new ObservableCollection<Entry> { 
                new Entry() { price = 4, miles = 2},
                new Entry() { price = 4, miles = 2},
                new Entry() { price = 4, miles = 2},
                new Entry() { price = 4, miles = 2},
                new Entry() { price = 4, miles = 2},
                new Entry() { price = 4, miles = 2},
                new Entry() { price = 4, miles = 2},
                new Entry() { price = 4, miles = 2},
                new Entry() { price = 4, miles = 2},
                new Entry() { price = 4, miles = 2},
                new Entry() { price = 4, miles = 2},
                new Entry() { price = 4, miles = 3}
            };
            serviceExpenses= new ObservableCollection<Entry> {
                new Entry() { price = 10},
                new Entry() { price = 12}
            };
        }

        public override string ToString()
        {
            return name;
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehiclesPage : ContentPage
    {
        public ObservableCollection<Vehicle> vehicles;
        public Vehicle vehicle;


        public VehiclesPage()
        {
            InitializeComponent();

            vehicles = new ObservableCollection<Vehicle>
            {
                new Vehicle() { name = "Volvo V60" },
                new Vehicle() { name = "Volvo V70" }
            };

            selectVehicles.ItemsSource = vehicles;

        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                vehicle = vehicles[selectedIndex];
            }

            fuelExpensesList.ItemsSource = vehicle.fuelExpenses;
            servicesExpensesList.ItemsSource = vehicle.serviceExpenses;
            averageFM.Text = vehicle.averageFuelMile.ToString();
        }

        private void AddFuel(object sender, EventArgs e)
        {
            double fuel = Convert.ToDouble(fuelEntry.Text);
            double miles= Convert.ToDouble(milesEntry.Text);

            vehicle?.fuelExpenses.Add(new Entry
            {
                miles = miles,
                price = fuel
            }) ;

            vehicle.averageFuelMile += fuel / miles;
            averageFM.Text =  vehicle.averageFuelMile.ToString();
            fuelEntry.Text = null;
            milesEntry.Text = null;
        }

        private void AddService(object sender, EventArgs e)
        {
            double service = Convert.ToDouble(serviceEntry.Text);
            vehicle?.serviceExpenses.Add(new Entry
            {
                price = service
            }) ;

            serviceEntry.Text = null;

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (entry.Text == null)
            {
                throw new Exception();
            }

            vehicles.Add(new Vehicle() { name=entry.Text });
            entry.Text = "";

        }
    }
}