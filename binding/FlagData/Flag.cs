using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FlagData
{
    /// <summary>
    /// This model object represents a single flag
    /// </summary>
    public class Flag : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DateTime dateAdopted;
        public string country;
        public string imageUrl;

        /// <summary>
        /// Name of the country that this flag belongs to
        /// </summary>
        public string Country { 
            get { return country; } 
            set { 
                if (country != value)
                {
                    country = value;
                    RaisePropertyChanged();
                }
            } }
        /// <summary>
        /// Image URL for the flag (from resources)
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// The date this flag was adopted
        /// </summary>
        public DateTime DateAdopted { get { return dateAdopted; } 
            set {
                if (dateAdopted != value)
                {
                    dateAdopted = value;
                    RaisePropertyChanged();
                }    
            }
             }
        /// <summary>
        /// Whether the flag includes an image/shield as part of the design
        /// </summary>
        public bool IncludesShield { get; set; }
        /// <summary>
        /// Some trivia or commentary about the flag
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// A URL for more information
        /// </summary>
        public Uri MoreInformationUrl { get; set; }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
