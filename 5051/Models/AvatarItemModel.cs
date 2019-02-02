using _5051.Backend;
using System;
using System.ComponentModel.DataAnnotations;

namespace _5051.Models
{
    /// <summary>
    /// AvatarItems for the system
    /// </summary>
    public class AvatarItemModel
    {
        // When the record was created, used for Table Storage
        public DateTime TimeStamp { get; set; }

        [Display(Name = "Id", Description = "AvatarItem Id")]
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }

        [Display(Name = "Uri", Description = "Picture to Show")]
        public string Uri { get; set; }

        [Display(Name = "Name", Description = "AvatarItem Name")]
        public string Name { get; set; }

        [Display(Name = "Description", Description = "AvatarItem Description")]
        public string Description { get; set; }

        [Display(Name = "Category", Description = "AvatarItem Category")]
        public AvatarItemCategoryEnum Category { get; set; }

        [Display(Name = "Tokens", Description = "Cost in Tokens")]
        public int Tokens { get; set; }

        [Display(Name = "Quantities", Description = "AvatarItem Quantities")]
        public int Quantities { get; set; }

        public bool IsLimitSupply { get; set; }

        // Is the item a default selection item
        public bool IsDefault;

        /// <summary>
        /// Create the default values
        /// </summary>
        public void Initialize()
        {
            Id = Guid.NewGuid().ToString();
            Tokens = 1;
            Quantities = 10;
            IsLimitSupply = false;
            TimeStamp = DateTimeHelper.Instance.GetDateTimeNowUTC();
        }

        /// <summary>
        /// New AvatarItem
        /// </summary>
        public AvatarItemModel()
        {
            Initialize();
        }

        /// <summary>
        /// Make a New Model, and Update it with data
        /// </summary>
        /// <param name="data"></param>
        public AvatarItemModel(AvatarItemModel data)
        {
            Initialize();
            Update(data);
        }

        /// <summary>
        /// Make an AvatarItem from values passed in
        /// </summary>
        /// <param name="uri">The Picture path</param>
        /// <param name="name">AvatarItem Name</param>
        /// <param name="description">AvatarItem Description</param>
        public AvatarItemModel(string uri, string name, string description, AvatarItemCategoryEnum category, int tokens, int quantities, bool isLimitSupply, bool isDefault=false)
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
        /// Used to Update AvatarItem Before doing a data save
        /// Updates everything except for the ID
        /// </summary>
        /// <param name="data">Data to update</param>
        public void Update(AvatarItemModel data)
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