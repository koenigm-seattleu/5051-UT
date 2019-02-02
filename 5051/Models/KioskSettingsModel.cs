using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace _5051.Models
{
    /// <summary>
    /// Setting model for the kiosk password for login and kiosk time zone
    /// The Amin should be allowed to change these
    /// Save as defaults to the Database
    /// </summary>
    public class KioskSettingsModel
    {
        // Used to access the settings instance, even if there is just one
        public string Id { get; set; }

        // The password for logging in 
        [Display(Name = "Password", Description = "Kiosk Password")]
        public string Password { get; set; }

        // The time zone of kiosk
        [Display(Name = "Kiosk Time Zone", Description = "Kiosk Time Zone")]
        public TimeZoneInfo TimeZone { get; set; }

        /// <summary>
        /// Contains a select list for time zone selection dropdown
        /// </summary>
        public List<SelectListItem> TimeZones { get; set; }

        /// <summary>
        /// The selected time zone id for semester selection dropdown
        /// </summary>
        public int SelectedTimeZoneId { get; set; }

        /// <summary>
        /// The last date the kiosk updated student login
        /// </summary>
        public DateTime LastProcessedDate { get; set; }

        /// <summary>
        /// The collection of system time zones
        /// </summary>
        private ReadOnlyCollection<TimeZoneInfo> TzCollection { get; set; }

        public KioskSettingsModel()
        {
            Initialize();
        }

        /// <summary>
        /// Create a copy of the item, used for updates.
        /// </summary>
        /// <param name="data"></param>
        public KioskSettingsModel(KioskSettingsModel data)
        {
            if (data == null)
            {
                return;
            }

            Password = data.Password;
            SelectedTimeZoneId = data.SelectedTimeZoneId;
            TzCollection = data.TzCollection;
            //reload the time zone using the given id
            TimeZone = TzCollection[SelectedTimeZoneId];
            LastProcessedDate = data.LastProcessedDate;
        }

        /// <summary>
        /// Create the default values
        /// </summary>
        private void Initialize()
        {
            SetDefault();
        }

        /// <summary>
        /// Sets the default values for the Item
        /// Because it is set here, there is no need to set defaults over in the Mock, call this instead
        /// </summary>
        public void SetDefault()
        {
            Id = Guid.NewGuid().ToString();
            Password = "123";

            //set default id to 5, pst time zone
            TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");

            //Initalize the drop down list
            TimeZones = new List<SelectListItem>();

            //Get system time zones
            TzCollection = TimeZoneInfo.GetSystemTimeZones();

            //fill the drop down list with system time zones
            for (int i = 0; i < TzCollection.Count; i++)
            {
                TimeZones.Add(new SelectListItem { Value = "" + i, Text = TzCollection[i].DisplayName });
            }

            LastProcessedDate = DateTime.MinValue;

        }

        /// <summary>
        /// Used to Update Before doing a data save
        /// Updates everything except for the ID
        /// </summary>
        /// <param name="data">Data to update</param>
        public void Update(KioskSettingsModel data)
        {
            if (data == null)
            {
                return;
            }

            Password = data.Password;
            SelectedTimeZoneId = data.SelectedTimeZoneId;
            TzCollection = data.TzCollection;
            //reload the time zone using the given id
            TimeZone = TzCollection[SelectedTimeZoneId];
            LastProcessedDate = data.LastProcessedDate;
        }
    }
}
