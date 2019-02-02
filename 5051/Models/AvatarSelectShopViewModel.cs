using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using _5051.Backend;

namespace _5051.Models
{
    // The URI for the Images for the Truck
    public class AvatarSelectShopViewModel
    {
        // Positions, with current item.

        public AvatarItemShopViewModel data;

        //public AvatarItemShopViewModel HairBack;
        //public AvatarItemShopViewModel Head;
        //public AvatarItemShopViewModel ShirtShort;
        //public AvatarItemShopViewModel Expression;
        //public AvatarItemShopViewModel Cheeks;
        //public AvatarItemShopViewModel HairFront;
        //public AvatarItemShopViewModel Accessory;
        //public AvatarItemShopViewModel ShirtFull;
        //public AvatarItemShopViewModel Pants;

        // The StudentID for this truck, used to simplify the models
        public StudentModel Student;
        
    }
}