using System;
using System.Web;

namespace _5051.Models
{
    /// <summary>
    /// System wide Global variables
    /// </summary>
    public class SystemGlobalsModel
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile SystemGlobalsModel instance;
        private static readonly object syncRoot = new Object();

        private SystemGlobalsModel() { }

        public static SystemGlobalsModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SystemGlobalsModel();
                            Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        // The Enum to use for the current data source
        // Default to Mock
        private static DataSourceEnum _DataSourceValue;

        public DataSourceEnum DataSourceValue => _DataSourceValue;

        public static void Initialize()
        {
            SetDataSourceEnum(DataSourceEnum.Mock);

            return;
        }

        public static DataSourceEnum SelectDataSourceEnum(string choice)
        {
            var myReturn = DataSourceEnum.Mock;

            if (choice == null)
            {
                return myReturn;
            }

            if (choice.Contains("mchs.azurewebsites.net"))
            {
                return DataSourceEnum.ServerLive;
            }

            if (choice.Contains("azurewebsites.net"))
            {
                return DataSourceEnum.ServerTest;
            }

            return myReturn;
        }

        public static void SetDataSourceEnum(DataSourceEnum SetDataSourceValue)
        {
            _DataSourceValue = SetDataSourceValue;
        }
    }
}