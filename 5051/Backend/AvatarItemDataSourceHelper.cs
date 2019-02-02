using System;
using System.Collections.Generic;

using _5051.Models;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace _5051.Backend
{
    /// <summary>
    /// Backend Table DataSource for AvatarItems, to manage them
    /// </summary>
    public class AvatarItemDataSourceHelper
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile AvatarItemDataSourceHelper instance;
        private static readonly object syncRoot = new Object();

        private AvatarItemDataSourceHelper() { }

        public static AvatarItemDataSourceHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new AvatarItemDataSourceHelper();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The AvatarItem List
        /// </summary>
        private List<AvatarItemModel> DataList = new List<AvatarItemModel>();

        /// <summary>
        /// Clear the Data List, and build up a new one
        /// </summary>
        /// <returns></returns>
        public List<AvatarItemModel> GetDefaultDataSet()
        {
            DataList.Clear();

            DataList.Add(new AvatarItemModel("Accessory0.png", "None", "None", AvatarItemCategoryEnum.Accessory, 1, 10, false, true));  // Default
            DataList.Add(new AvatarItemModel("Accessory1.png", "Glasses", "Horn-Rimmed", AvatarItemCategoryEnum.Accessory, 10, 10, false));
            DataList.Add(new AvatarItemModel("Accessory2.png", "Glasses", "Round", AvatarItemCategoryEnum.Accessory, 10, 10, false));
            DataList.Add(new AvatarItemModel("Accessory3.png", "Glasses", "Rectangle", AvatarItemCategoryEnum.Accessory, 10, 10, false));
            DataList.Add(new AvatarItemModel("Accessory4.png", "Earrings", "Studs", AvatarItemCategoryEnum.Accessory, 15, 10, false));
            DataList.Add(new AvatarItemModel("Accessory5.png", "Earrings", "Hoops", AvatarItemCategoryEnum.Accessory, 15, 10, false));

            DataList.Add(new AvatarItemModel("Cheeks0.png", "None", "None", AvatarItemCategoryEnum.Cheeks, 1, 10, false,true)); // Default
            DataList.Add(new AvatarItemModel("Cheeks1.png", "Blush", "Light", AvatarItemCategoryEnum.Cheeks, 10, 10, false));
            DataList.Add(new AvatarItemModel("Cheeks2.png", "Blush", "Dark", AvatarItemCategoryEnum.Cheeks, 10, 10, false));
            DataList.Add(new AvatarItemModel("Cheeks3.png", "Blush", "Darker", AvatarItemCategoryEnum.Cheeks, 10, 10, false));
            DataList.Add(new AvatarItemModel("Cheeks4.png", "Freckles", "Dark", AvatarItemCategoryEnum.Cheeks, 15, 10, false));
            DataList.Add(new AvatarItemModel("Cheeks5.png", "Freckles", "Light", AvatarItemCategoryEnum.Cheeks, 15, 10, false));

            // DataList.Add(new AvatarItemModel("Expression0.png", "None", "None", AvatarItemCategoryEnum.Expression, 1, 10, false));
            DataList.Add(new AvatarItemModel("Expression1.png", "Smile", "Happy", AvatarItemCategoryEnum.Expression, 1, 10, false, true)); // Default
            DataList.Add(new AvatarItemModel("Expression2.png", "Smile", "Lashy", AvatarItemCategoryEnum.Expression, 10, 10, false));
            DataList.Add(new AvatarItemModel("Expression3.png", "Eyes", "Femme Smile", AvatarItemCategoryEnum.Expression, 10, 10, false));
            DataList.Add(new AvatarItemModel("Expression4.png", "Eyes", "Smiley", AvatarItemCategoryEnum.Expression, 10, 10, false));
            DataList.Add(new AvatarItemModel("Expression5.png", "Meh", "Moody", AvatarItemCategoryEnum.Expression, 15, 10, false));
            DataList.Add(new AvatarItemModel("Expression6.png", "Meh", "Feeme Moody", AvatarItemCategoryEnum.Expression, 15, 10, false));

            DataList.Add(new AvatarItemModel("Head0.png", "Head", "None", AvatarItemCategoryEnum.Head, 1, 10, false, true)); // Default
            DataList.Add(new AvatarItemModel("Head1.png", "Head", "Light", AvatarItemCategoryEnum.Head, 1, 10, false));
            DataList.Add(new AvatarItemModel("Head2.png", "Head", "Olive", AvatarItemCategoryEnum.Head, 1, 10, false));
            DataList.Add(new AvatarItemModel("Head3.png", "Head", "Netural", AvatarItemCategoryEnum.Head, 1, 10, false));
            DataList.Add(new AvatarItemModel("Head4.png", "Head", "Dark", AvatarItemCategoryEnum.Head, 1, 10, false));
            DataList.Add(new AvatarItemModel("Head5.png", "Head", "Darker", AvatarItemCategoryEnum.Head, 1, 10, false));

            DataList.Add(new AvatarItemModel("Shirt_white.png", "Shirt", "White", AvatarItemCategoryEnum.ShirtFull, 1, 10, false, true)); // Default
            DataList.Add(new AvatarItemModel("Shirt_black.png", "Shirt", "Black", AvatarItemCategoryEnum.ShirtFull, 20, 10, false));
            DataList.Add(new AvatarItemModel("Shirt_blue.png", "Shirt", "Blue", AvatarItemCategoryEnum.ShirtFull, 20, 10, false));
            DataList.Add(new AvatarItemModel("Shirt_green.png", "Shirt", "Green", AvatarItemCategoryEnum.ShirtFull, 20, 10, false));
            DataList.Add(new AvatarItemModel("Shirt_orange.png", "Shirt", "Orange", AvatarItemCategoryEnum.ShirtFull, 20, 10, false));
            DataList.Add(new AvatarItemModel("Shirt_red.png", "Shirt", "Red", AvatarItemCategoryEnum.ShirtFull, 20, 10, false));

            DataList.Add(new AvatarItemModel("Pants.png", "Pants", "Blue", AvatarItemCategoryEnum.Pants, 1, 10, false, true)); // Default

            #region FrontHair

            DataList.Add(new AvatarItemModel("placeholder.png", "Shaved", "Bald", AvatarItemCategoryEnum.HairFront, 1, 30, false, true)); // Default

            DataList.Add(new AvatarItemModel("Hair1_shortfringe_grey.png", "Grey", "Short", AvatarItemCategoryEnum.HairFront, 10, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_shortfringe_chestnut.png", "Chestnut", "Short", AvatarItemCategoryEnum.HairFront, 10, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_shortfringe_blonde.png", "Blonde", "Short", AvatarItemCategoryEnum.HairFront, 10, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_shortfringe_black.png", "Black", "Short", AvatarItemCategoryEnum.HairFront, 10, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_shortfringe_brown.png", "Brown", "Short", AvatarItemCategoryEnum.HairFront, 10, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_shortfringe_red.png", "Red", "Short", AvatarItemCategoryEnum.HairFront, 10, 30, false));


            DataList.Add(new AvatarItemModel("Hair1_straight_white.png", "White", "Straight", AvatarItemCategoryEnum.HairFront, 10, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_straight_black.png", "Black", "Straight", AvatarItemCategoryEnum.HairFront, 10, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_straight_Blonde.png", "Blonde", "Straight", AvatarItemCategoryEnum.HairFront, 10, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_straight_brown.png", "Brown", "Straight", AvatarItemCategoryEnum.HairFront, 10, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_straight_chestnut.png", "Chestnut", "Straight", AvatarItemCategoryEnum.HairFront, 10, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_straight_red.png", "Red", "Straight", AvatarItemCategoryEnum.HairFront, 10, 30, false));

            DataList.Add(new AvatarItemModel("Hair1_short_white.png", "White", "Crew Cut", AvatarItemCategoryEnum.HairFront, 10, 20, false));
            DataList.Add(new AvatarItemModel("Hair1_short_black.png", "Black", "Crew Cut", AvatarItemCategoryEnum.HairFront, 10, 20, false));
            DataList.Add(new AvatarItemModel("Hair1_short_Blonde.png", "Blonde", "Crew Cut", AvatarItemCategoryEnum.HairFront, 10, 20, false));
            DataList.Add(new AvatarItemModel("Hair1_short_brown.png", "Brown", "Crew Cut", AvatarItemCategoryEnum.HairFront, 10, 20, false));
            DataList.Add(new AvatarItemModel("Hair1_short_chestnut.png", "Chestnut", "Crew Cut", AvatarItemCategoryEnum.HairFront, 10, 20, false));
            DataList.Add(new AvatarItemModel("Hair1_short_red.png", "Red", "Crew Cut", AvatarItemCategoryEnum.HairFront, 10, 20, false));

            DataList.Add(new AvatarItemModel("Hair1_hairline_black.png", "Black", "Shaved", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_hairline_Blonde.png", "Blonde", "Shaved", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_hairline_brown.png", "Brown", "Shaved", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_hairline_chestnut.png", "Chestnut", "Shaved", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_hairline_red.png", "Red", "Shaved", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_hairline_white.png", "White", "Shaved", AvatarItemCategoryEnum.HairFront, 10, 10, false));

            DataList.Add(new AvatarItemModel("Hair1_loose_black.png", "Black", "Loose", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_loose_Blonde.png", "Blonde", "Loose", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_loose_brown.png", "Brown", "Loose", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_loose_chestnut.png", "Chestnut", "Loose", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_loose_red.png", "Red", "Loose", AvatarItemCategoryEnum.HairFront, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair1_loose_white.png", "White", "Loose", AvatarItemCategoryEnum.HairFront, 10, 10, false));

            DataList.Add(new AvatarItemModel("Hair1_swept_black.png", "Black", "Swept", AvatarItemCategoryEnum.HairFront, 15, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_swept_Blonde.png", "Blonde", "Swept", AvatarItemCategoryEnum.HairFront, 15, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_swept_brown.png", "Brown", "Swept", AvatarItemCategoryEnum.HairFront, 15, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_swept_chestnut.png", "Chestnut", "Swept", AvatarItemCategoryEnum.HairFront, 15, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_swept_red.png", "Red", "Swept", AvatarItemCategoryEnum.HairFront, 15, 30, false));
            DataList.Add(new AvatarItemModel("Hair1_swept_white.png", "White", "Swept", AvatarItemCategoryEnum.HairFront, 15, 30, false));

            #endregion FrontHair

            #region BackHair
            DataList.Add(new AvatarItemModel("Hair0.png", "Shaved", "Very Short", AvatarItemCategoryEnum.HairBack, 1, 10, false, true)); // Default

            DataList.Add(new AvatarItemModel("Hair2_short_white.png", "White", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_short_black.png", "Black", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_short_Blonde.png", "Blonde", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_short_brown.png", "Brown", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_short_chestnut.png", "Chestnut", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_short_red.png", "Red", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_short_blackblue.png", "Black Blue", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_short_blackgreen.png", "Black Green", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_short_blackviolet.png", "Black violet", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_short_Blondegreen.png", "Blonde Green", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_short_Blondeombre.png", "Blonde Ombre", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_short_Blondepink.png", "Blonde Pink", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_short_BlondeViolet.png", "Blonde Violet", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_short_brownombre.png", "Brown Ombre", "Short", AvatarItemCategoryEnum.HairBack, 10, 10, false));

            DataList.Add(new AvatarItemModel("Hair2_kinky_black.png", "Black", "Fluffy", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_kinky_Blonde.png", "Blonde", "Fluffy", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_kinky_brown.png", "Brown", "Fluffy", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_kinky_chestnut.png", "Chestnut", "Fluffy", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_kinky_red.png", "Red", "Fluffy", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_kinky_white.png", "White", "Fluffy", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_kinky_BlackBlue.png", "Black Blue", "Fluffy", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_kinky_BlackGreen.png", "Black Green", "Fluffy", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_kinky_BlackViolet.png", "Black Violet", "Fluffy", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_kinky_BlondeOmbre.png", "Blonde Ombre", "Fluffy", AvatarItemCategoryEnum.HairBack, 10, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_kinky_BrownOmbre.png", "Brown Ombre", "Fluffy", AvatarItemCategoryEnum.HairBack, 10, 10, false));

            DataList.Add(new AvatarItemModel("Hair2_long_black.png", "Black", "Long", AvatarItemCategoryEnum.HairBack, 15, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_long_Blonde.png", "Blonde", "Long", AvatarItemCategoryEnum.HairBack, 15, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_long_brown.png", "Brown", "Long", AvatarItemCategoryEnum.HairBack, 15, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_long_chestnut.png", "Chestnut", "Long", AvatarItemCategoryEnum.HairBack, 15, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_long_red.png", "Red", "Long", AvatarItemCategoryEnum.HairBack, 15, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_long_white.png", "White", "Long", AvatarItemCategoryEnum.HairBack, 15, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_long_blackblue.png", "Black Blue", "Long", AvatarItemCategoryEnum.HairBack, 15, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_long_blackgreen.png", "Black Green", "Long", AvatarItemCategoryEnum.HairBack, 15, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_long_blackviolet.png", "Black Violet", "Long", AvatarItemCategoryEnum.HairBack, 15, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_long_Blondeombre.png", "Blonde Ombre", "Long", AvatarItemCategoryEnum.HairBack, 15, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_long_Blondepink.png", "Blonde Pink", "Long", AvatarItemCategoryEnum.HairBack, 15, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_long_Blondeviolet.png", "Blonde Violet", "Long", AvatarItemCategoryEnum.HairBack, 15, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_long_Blondegreen.png", "Blonde Green", "Long", AvatarItemCategoryEnum.HairBack, 15, 10, false));
            DataList.Add(new AvatarItemModel("Hair2_long_brownombre.png", "Brown Ombre", "Long", AvatarItemCategoryEnum.HairBack, 15, 10, false));
            #endregion BackHair

            return DataList;
        }

    }
}