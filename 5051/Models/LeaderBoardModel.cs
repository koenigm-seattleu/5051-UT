using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Globalization;

namespace _5051.Models
{
    /// <summary>
    /// Games for the system
    /// </summary>
    public class LeaderBoardModel
    {
        [Display(Name = "Id", Description = "Id")]
        public string Id { get; set; }

        [Display(Name = "Name", Description = "Name")]
        public string Name { get; set; }

        [Display(Name = "Profit", Description = "Profit")]
        public int Profit { get; set; }



        /// <summary>
        /// Create the default values
        /// </summary>
        public void Initialize()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// New LeadBoard
        /// </summary>
        public LeaderBoardModel()
        {
            Initialize();
        }

        /// <summary>
        /// New LeadBoard, with data passed in
        /// </summary>
        public LeaderBoardModel(LeaderBoardModel data)
        {
            Initialize();
            Update(data);
        }

        /// <summary>
        /// Used to Update LeadBoard Before doing a data save
        /// Updates everything except for the ID
        /// </summary>
        /// <param name="data">Data to update</param>
        public void Update(LeaderBoardModel data)
        {
            if (data == null)
            {
                return;
            }

            Name = data.Name;
            Profit = data.Profit;
        }
    }
}