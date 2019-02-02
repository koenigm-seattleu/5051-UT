using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Backend;
using _5051.Models;

namespace _5051.Maintain
{

    public class AvatarItemMaintenance
    {
        /// <summary>
        /// Refersh Avatars, walks the Avatar Data in the DB, with the Avatar Defaults from the Helper, and restores defaults that are not there. 
        /// Call this function after uploading new avatars etc
        /// Or changing an avatar settings
        /// </summary>
        public bool RefreshAvatars()
        {
            var DefaultDataSet = AvatarItemDataSourceHelper.Instance.GetDefaultDataSet();
            var CurrentDataSet = DataSourceBackend.Instance.AvatarItemBackend.Index();

            /* Walk each item in the Default Data Set and see if it is in the exiting DB
             * Note the IDs will be different so use the URI to match.
             * 
             * If not, then Add it 
             * If it is there, check the values
             * If the values are different Update them to match.
            */

            foreach (var item in DefaultDataSet)
            {
                var FoundData = CurrentDataSet.Find(n => n.Uri == item.Uri);
                if (FoundData == null)
                {
                    // Not found, so add it
                    DataSourceBackend.Instance.AvatarItemBackend.Create(item);
                }
                else
                {
                    // Update it, Overrite FoundData fields with Item, the save it
                    FoundData.Update(item);
                    DataSourceBackend.Instance.AvatarItemBackend.Update(item);
                }
            }

            return true;
        }
    }
}