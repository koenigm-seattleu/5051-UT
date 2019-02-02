using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Models;

namespace _5051.Backend
{

    /// <summary>
    /// Datasource Interface for FactoryInventorys
    /// </summary>
    public interface IFactoryInventoryInterface
    {
        FactoryInventoryModel Create(FactoryInventoryModel data, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown);
        FactoryInventoryModel Read(string id);
        FactoryInventoryModel Update(FactoryInventoryModel data);
        bool Delete(string id, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown);
        List<FactoryInventoryModel> Index();
        void Reset();
        void LoadDataSet(DataSourceDataSetEnum setEnum);

        bool BackupData(DataSourceEnum dataSourceSource, DataSourceEnum dataSourceDestination);
    }
}