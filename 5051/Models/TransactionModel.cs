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
    public class TransactionModel
    {
        [Display(Name = "Id", Description = "Id")]
        public string Id { get; set; }

        [Display(Name = "Name", Description = "Name")]
        public string Name { get; set; }

        [Display(Name = "Uri", Description = "Image")]
        public string Uri { get; set; }



        /// <summary>
        /// Create the default values
        /// </summary>
        public void Initialize()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// New Game
        /// </summary>
        public TransactionModel()
        {
            Initialize();
        }

        /// <summary>
        /// New Game, with data passed in
        /// </summary>
        public TransactionModel(TransactionModel data)
        {
            Initialize();
            Update(data);
        }

        /// <summary>
        /// Used to Update Game Before doing a data save
        /// Updates everything except for the ID
        /// </summary>
        /// <param name="data">Data to update</param>
        public void Update(TransactionModel data)
        {
            if (data == null)
            {
                return;
            }

            Name = data.Name;
            Uri = data.Uri;
        }
    }
}