using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Models
{
    // Used for Editing Controller and View
    public class AvatarItemShopViewModel
    {
        // Current Item
        public AvatarItemModel Item = new AvatarItemModel();

        // List of Options to Pick...
        public List<AvatarItemModel> ItemList = new List<AvatarItemModel>();
    }
}