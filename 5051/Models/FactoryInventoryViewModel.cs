using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Models
{
    /// <summary>
    /// View Model for the FactoryInventory Views to have the list of FactoryInventorys
    /// </summary>
    public class FactoryInventoryViewModel
    {
        /// <summary>
        /// The List of FactoryInventorys
        /// </summary>
        public List<FactoryInventoryModel> FactoryInventoryList = new List<FactoryInventoryModel>();
        public FactoryInventoryCategoryEnum Category = FactoryInventoryCategoryEnum.Unknown;
    }

    /// <summary>
    /// Adds a list of FactoryInventory Lists per Category, making it easier to select
    /// </summary>
    public class FactoryInventoryListViewModel
    {
        public List<FactoryInventoryViewModel> FactoryInventoryCategoryList = new List<FactoryInventoryViewModel>();
    }

    /// <summary>
    /// Adds the Student Information to the View Model for the FactoryInventorys availble for the student to select
    /// </summary>
    public class SelectedFactoryInventoryForStudentViewModel : FactoryInventoryListViewModel
    {
        public StudentModel Student = new StudentModel();
    }
}