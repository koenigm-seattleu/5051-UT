using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Models;

namespace _5051.Backend
{

    /// <summary>
    /// Datasource Interface for Avatars
    /// </summary>
    public interface IKioskSettingsInterface
    {
        KioskSettingsModel Create(KioskSettingsModel data, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown);
        KioskSettingsModel Read(string id);
        KioskSettingsModel Update(KioskSettingsModel data);
        bool Delete(string id, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown);
        List<KioskSettingsModel> Index();
        void Reset();
        void LoadDataSet(DataSourceDataSetEnum setEnum);

        bool BackupData(DataSourceEnum dataSourceSource, DataSourceEnum dataSourceDestination);
    }
}