using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartHome
{
    class DeviceTemplateSelector : DataTemplateSelector
    {
        DataTemplate doorBellTemplate;
        DataTemplate smokeDetectorTemplate;
        DataTemplate thermostatTemplate;

        public DeviceTemplateSelector()
        {
            doorBellTemplate = new DataTemplate(typeof(DoorBellViewCell));
            smokeDetectorTemplate= new DataTemplate(typeof(SmokeDetectorViewCell));
            thermostatTemplate = new DataTemplate(typeof(ThermostatViewCell));

        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch(item) {
                case DoorBell _:
                    return doorBellTemplate;
                case SmokeDetector _:
                    return smokeDetectorTemplate;
                case Thermostat _:
                    return thermostatTemplate;
                default:
                    throw new ArgumentException("Invalid item");
            }
        }
    }
}
