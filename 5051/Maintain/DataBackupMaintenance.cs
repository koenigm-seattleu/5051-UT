using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Backend;
using _5051.Models;

namespace _5051.Maintain
{
    public class DataBackupMaintenance
    {

        public bool DataBackup(DataSourceEnum Source, DataSourceEnum Destination)
        {
            if (Source == Destination)
            {
                return false;
            }

            if (Source == DataSourceEnum.Mock)
            {
                return false;
            }


            DataSourceBackend.Instance.AvatarItemBackend.BackupData(Source, Destination);

            DataSourceBackend.Instance.FactoryInventoryBackend.BackupData(Source, Destination);

            DataSourceBackend.Instance.IdentityBackend.BackupData(Source, Destination);

            DataSourceBackend.Instance.SchoolDismissalSettingsBackend.BackupData(Source, Destination);

            DataSourceBackend.Instance.StudentBackend.BackupData(Source, Destination);

            DataSourceBackend.Instance.KioskSettingsBackend.BackupData(Source, Destination);

            return true;
        }
    }
}