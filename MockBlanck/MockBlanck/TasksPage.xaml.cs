using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MockBlanck
{
    public class Task
    {
        public string task { get; set; }

        public Task(string t)
        {
            task = t;
        }
    }

    class vehicleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Vehicle)value).name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksPage : ContentPage
    {
        public ObservableCollection<Task> tasks;

        public TasksPage()
        {
            InitializeComponent();
            tasks = new ObservableCollection<Task>();
            tasks.Add(new Task("TER"));

            list.ItemsSource = tasks;
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (entry.Text == null)
            {
                throw new Exception();
            }

            tasks.Add(new Task(entry.Text));
            entry.Text = "";

        }
    }
}