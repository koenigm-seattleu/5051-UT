using System;

namespace _5051.Backend
{
    /// <summary>
    /// Get DateTime helper functions
    /// </summary>
    public class DateTimeHelper
    {
        private static volatile DateTimeHelper instance;
        private static readonly object syncRoot = new Object();

        private DateTimeHelper() { }

        public static DateTimeHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new DateTimeHelper();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        // The forced DateTime number to use
        private static DateTime ForcedDateTime;

        // track if set is on or off
        public static bool isSetForced;

        /// <summary>
        /// Init  and set default values.
        /// </summary>
        public void Initialize()
        {
            // Default Number 
            SetForced(DateTime.UtcNow);

            //Turn off forced number
            isSetForced = EnableForced(false);  
        }

        /// <summary>
        /// Sets the forced Number to return
        /// </summary>
        /// <param name="number"></param>
        public void SetForced(DateTime setValue)
        {
            // generate DateTime number
            ForcedDateTime = setValue;
            isSetForced = true;
        }

        /// <summary>
        /// Sets the Forced Number to True or false
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool EnableForced(bool state)
        {
            isSetForced = state;
            return state;
        }

        /// <summary>
        /// Get DateTime Number will return Now in UTC, or
        /// If ForcedDateTime is true, it will return the forced value.
        /// </summary>
        /// <returns></returns>
        public DateTime GetDateTimeNowUTC()
        {
            DateTime myReturn;

            if (isSetForced)
            {
                myReturn = ForcedDateTime;
            }
            else
            {
                myReturn = DateTime.UtcNow;
            }

            return myReturn;
        }
    }
}