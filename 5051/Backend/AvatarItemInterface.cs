using System.Collections.Generic;
using _5051.Models;

namespace _5051.Backend
{

    /// <summary>
    /// Datasource Interface for AvatarItems
    /// </summary>
    public interface IAvatarItemInterface
    {
        AvatarItemModel Create(AvatarItemModel data, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown);
        AvatarItemModel Read(string id);
        AvatarItemModel Update(AvatarItemModel data);
        bool Delete(string id, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown);
        List<AvatarItemModel> Index();
        void Reset();
        void LoadDataSet(DataSourceDataSetEnum setEnum);
    }
}