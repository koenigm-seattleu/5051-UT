using _5051.Models;
using System;
using System.Web;

namespace _5051.Backend
{
    /// <summary>
    /// Class that manages the overall data sources
    /// </summary>
    public class DataSourceBackend
    {
        /// <summary>
        /// Hold one of each of the DataSources as an instance to the datasource
        /// </summary>
        public AvatarItemBackend AvatarItemBackend = AvatarItemBackend.Instance;
        public KioskSettingsBackend KioskSettingsBackend = KioskSettingsBackend.Instance;

        public SchoolDismissalSettingsBackend SchoolDismissalSettingsBackend = SchoolDismissalSettingsBackend.Instance;

        public IdentityBackend IdentityBackend = IdentityBackend.Instance;

        public StudentBackend StudentBackend = StudentBackend.Instance;

        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile DataSourceBackend instance;
        private static readonly object syncRoot = new Object();

        private static bool isTestingMode = false;

        private DataSourceBackend()
        {
            // Avatar must be before Student, because Student needs the default avatar
            AvatarItemBackend = AvatarItemBackend.Instance;

            KioskSettingsBackend = KioskSettingsBackend.Instance;

            SchoolDismissalSettingsBackend = SchoolDismissalSettingsBackend.Instance;

            IdentityBackend = IdentityBackend.Instance;

            StudentBackend = StudentBackend.Instance;
        }

        public static DataSourceBackend Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new DataSourceBackend();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Call for all data sources to reset
        /// </summary>
        public void Reset()
        {
            AvatarItemBackend.Reset();

            KioskSettingsBackend.Reset();

            SchoolDismissalSettingsBackend.Reset();

            IdentityBackend.Reset();

            StudentBackend.Reset();

            SetTestingMode(false);
        }

        /// <summary>
        /// Changes the data source, does not call for a reset, that allows for how swapping but keeping the original data in place
        /// </summary>
        public void SetDataSource(DataSourceEnum dataSourceEnum)
        {
            // Set the Global DataSourceEnum Value
            SystemGlobalsModel.SetDataSourceEnum(dataSourceEnum);

            // Avatar must be reset before Student, because Student needs the default avatar

            AvatarItemBackend.SetDataSource(SystemGlobalsModel.Instance.DataSourceValue);
            KioskSettingsBackend.SetDataSource(SystemGlobalsModel.Instance.DataSourceValue);

            SchoolDismissalSettingsBackend.SetDataSource(SystemGlobalsModel.Instance.DataSourceValue);

            IdentityBackend.SetDataSource(SystemGlobalsModel.Instance.DataSourceValue);

            StudentBackend.SetDataSource(SystemGlobalsModel.Instance.DataSourceValue);
        }

        /// <summary>
        /// Change between demo, default, and UT data sets
        /// </summary>
        /// <param name="SetEnum"></param>
        public void SetDataSourceDataSet(DataSourceDataSetEnum SetEnum)
        {
            // Avatar must be reset before Student, because Student needs the default avatar

            AvatarItemBackend.SetDataSourceDataSet(SetEnum);
            KioskSettingsBackend.SetDataSourceDataSet(SetEnum);

            SchoolDismissalSettingsBackend.SetDataSourceDataSet(SetEnum);
            
            IdentityBackend.SetDataSourceDataSet(SetEnum);

            StudentBackend.SetDataSourceDataSet(SetEnum);
        }

        public static bool GetTestingMode()
        {
            return isTestingMode;
        }

        public static bool SetTestingMode(bool mode)
        {
            isTestingMode = mode;

            //set the testing mode for other backends
            //DataSourceBackend.SetTestingMode(mode);
            //Backend.StudentDataSourceMock.SetTestingMode(mode);

            return isTestingMode;
        }

        public bool IsUserNotInRole(string userID, _5051.Models.UserRoleEnum role)
        {
            if (isTestingMode)
            {
                return false; // all OK
            }

            if (IdentityBackend.UserHasClaimOfType(userID, role))
            {
                return false;
            }
            return true; // Not in role, so error
        }

        public object CreateCookie(string testCookieName, string testCookieValue, HttpContextBase @object)
        {
            throw new NotImplementedException();
        }
    }
}