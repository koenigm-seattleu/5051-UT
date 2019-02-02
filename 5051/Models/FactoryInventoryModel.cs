using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using _5051.Backend;

namespace _5051.Models
{
    /// <summary>
    /// FactoryInventorys for the system
    /// </summary>
    public class FactoryInventoryModel
    {
        // When the record was created, used for Table Storage
        public DateTime TimeStamp { get; set; }

        [Display(Name = "Id", Description = "FactoryInventory Id")]
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }

        [Display(Name = "Uri", Description = "Picture to Show")]
        [Required(ErrorMessage = "Picture is required")]
        public string Uri { get; set; }

        [Display(Name = "Name", Description = "FactoryInventory Name")]
        [Required(ErrorMessage = "FactoryInventory Name is required")]
        public string Name { get; set; }

        [Display(Name = "Description", Description = "FactoryInventory Description")]
        [Required(ErrorMessage = "FactoryInventory Description is required")]
        public string Description { get; set; }

        [Display(Name = "Category", Description = "FactoryInventory Category")]
        [Required(ErrorMessage = "Category is required")]
        public FactoryInventoryCategoryEnum Category { get; set; }

        [Display(Name = "Tokens", Description = "Cost in Tokens")]
        [Required(ErrorMessage = "Tokens is required")]
        public int Tokens { get; set; }

        [Display(Name = "Quantities", Description = "FactoryInventory Quantities")]
        [Required(ErrorMessage = "Quantities is required")]
        public int Quantities { get; set; }
        
        [Display(Name = "Limited Supply", Description ="FactoryInventory IsLimitSupply")]

        public bool IsLimitSupply { get; set; }

        // Track if the item is a default item
        public bool IsDefault { get; set; }

        /// <summary>
        /// Create the default values
        /// </summary>
        public void Initialize()
        {
            Id = Guid.NewGuid().ToString();
            Tokens = 1;
            Quantities = 10;
            IsLimitSupply = true;
            TimeStamp = DateTimeHelper.Instance.GetDateTimeNowUTC();
        }

        /// <summary>
        /// New FactoryInventory
        /// </summary>
        public FactoryInventoryModel()
        {
            Initialize();
        }

        /// <summary>
        /// Make a New Model, and Update it with data
        /// </summary>
        /// <param name="data"></param>
        public FactoryInventoryModel(FactoryInventoryModel data)
        {
            Initialize();
            Update(data);
        }

        /// <summary>
        /// Make an FactoryInventory from values passed in
        /// </summary>
        /// <param name="uri">The Picture path</param>
        /// <param name="name">FactoryInventory Name</param>
        /// <param name="description">FactoryInventory Description</param>
        public FactoryInventoryModel(string uri, string name, string description, FactoryInventoryCategoryEnum category, int tokens, int quantities, bool isLimitSupply, bool isDefault = false)
        {
            Initialize();
         
            Uri = uri;
            Name = name;
            Description = description;
            Category = category;
            Tokens = tokens;
            Quantities = quantities;
            IsLimitSupply = isLimitSupply;
            IsDefault = isDefault;
        }

        /// <summary>
        /// Used to Update FactoryInventory Before doing a data save
        /// Updates everything except for the ID
        /// </summary>
        /// <param name="data">Data to update</param>
        public void Update(FactoryInventoryModel data)
        {
            if (data == null)
            {
                return;
            }

            Uri = data.Uri;
            Name = data.Name;
            Description = data.Description;
            Tokens = data.Tokens;
            Quantities = data.Quantities;
            Category = data.Category;
            IsLimitSupply = data.IsLimitSupply;
            IsDefault = data.IsDefault;
        }
    }
}