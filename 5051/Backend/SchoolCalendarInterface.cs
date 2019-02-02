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
    public interface ISchoolCalendarInterface
    {
        SchoolCalendarModel Create(SchoolCalendarModel data, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown);
        SchoolCalendarModel Read(string id);
        SchoolCalendarModel Update(SchoolCalendarModel data);
        bool Delete(string id, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown);
        List<SchoolCalendarModel> Index();
        void Reset();
        void LoadDataSet(DataSourceDataSetEnum setEnum);

        SchoolCalendarModel ReadDate(DateTime date);

        bool BackupData(DataSourceEnum dataSourceSource, DataSourceEnum dataSourceDestination);

    }
}