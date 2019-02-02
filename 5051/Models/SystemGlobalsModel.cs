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
        //private static HttpContext _HttpContext;
        //private static HttpContext _DefaultHttpContext;

        ////The current date
        //public DateTime CurrentDate = DateTime.MinValue;

        //The Target Site

        public DataSourceEnum DataSourceValue => _DataSourceValue;
        //public HttpContext HttpContext  => _HttpContext;
        //public HttpContext DefaultHttpContext => _DefaultHttpContext;

        //public static void SetDefaultHttpContext(HttpContext context)
        //{
        //    _DefaultHttpContext = context;
        //}

        //public static void SetHttpContext(HttpContext context)
        //{
        //    _HttpContext = context;
        //}

        //public static void RestoreDefaultHttpContext()
        //{
        //    _HttpContext = _DefaultHttpContext;
        //}

        public static void Initialize()
        {
            var myCurrentURL = GetCurrentHostURL(HttpContext.Current);
            var myDataSoruceEnum = SelectDataSourceEnum(myCurrentURL);
            SetDataSourceEnum(myDataSoruceEnum);

            return;
        }

        public static string GetCurrentHostURL(HttpContext context)
        {
            string myReturn = null;

            if (context == null)
            {
                return myReturn;
            }

            if (string.IsNullOrEmpty(context.Request.Url.Host))
            {
                return myReturn;
            }

            myReturn = context.Request.Url.Host;
            return myReturn;
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