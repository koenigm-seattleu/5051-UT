using System.Collections.Generic;

namespace _5051.Models
{
    /// <summary>
    /// View Model for the AvatarItem Views to have the list of AvatarItems
    /// </summary>
    public class AvatarItemViewModel
    {
        /// <summary>
        /// The List of AvatarItems
        /// </summary>
        public List<AvatarItemModel> AvatarItemList = new List<AvatarItemModel>();
        public AvatarItemCategoryEnum Category = AvatarItemCategoryEnum.Unknown;
    }

    /// <summary>
    /// Adds a list of AvatarItem Lists per Category, making it easier to select
    /// </summary>
    public class AvatarItemListViewModel
    {
        public List<AvatarItemViewModel> AvatarItemCategoryList = new List<AvatarItemViewModel>();
    }

    /// <summary>
    /// Adds the Student Information to the View Model for the AvatarItemLists availble for the student to select
    /// </summary>
    public class SelectedAvatarItemListForStudentViewModel : AvatarItemListViewModel
    {
        public StudentModel Student = new StudentModel();
    }
}