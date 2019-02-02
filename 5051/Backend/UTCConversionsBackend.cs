using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5051.Models;

namespace _5051.Backend
{
    /// <summary>
    /// UTCConversionBackend handles converting to and from UTC and client time
    /// </summary>
    public static class UTCConversionsBackend
    {

        public static DateTime ToClientTime(this DateTime dt)
        {
            // If it is alreayd in local time, then return it as local time
            if (dt.Kind == DateTimeKind.Local)
            {
                return dt;
            }

            dt = DateTime.SpecifyKind(dt, DateTimeKind.Local);

            object timeOffSet;

            //If the session is a web api call, it won't have the httpcontext, so send back UTC
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
            {
                timeOffSet = null;
            }
            else
            {
                // read the value from session
                timeOffSet = HttpContext.Current.Session["timezoneoffset"];

            }

            // if there is no offset in session return the datetime in server timezone
            if (timeOffSet == null)
            {
                return dt.ToUniversalTime();
            }

            var offset = int.Parse(timeOffSet.ToString());

            if (dt.CompareTo(DateTime.MinValue) == 0)
            {
                return dt;
            }

            //Going from UTC to local requires flipping the offset
            var newDateTime = dt;

            try
            {
                newDateTime = dt.AddMinutes(-1 * offset);
            }
            catch (Exception)
            {
                return dt;
            }

            return newDateTime;
        }

        public static DateTime FromClientTime(this DateTime dt)
        {

            //dt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);

            // read the value from session
            var timeOffSet = HttpContext.Current.Session["timezoneoffset"];

            // if there is no offset in session return the datetime in server timezone
            if (timeOffSet == null)
            {
                return dt.ToUniversalTime();
            }
            else
            {
                dt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                timeOffSet = HttpContext.Current.Session["timezoneoffset"];
            }

            var offset = int.Parse(timeOffSet.ToString());

            //going from local to UTC
            dt = dt.AddMinutes(offset);

            return dt;
        }

        /// <summary>
        /// Convert from Utc to Kiosk time zone
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime UtcToKioskTime(DateTime dt)
        {
            var kioskTimeZone = KioskSettingsBackend.Instance.GetDefault().TimeZone;
            var result = TimeZoneInfo.ConvertTimeFromUtc(dt, kioskTimeZone);
            return result;
        }

        /// <summary>
        /// Convert from Kiosk time zone to Utc
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime KioskTimeToUtc(DateTime dt)
        {
            var kioskTimeZone = KioskSettingsBackend.Instance.GetDefault().TimeZone;
            var result = TimeZoneInfo.ConvertTimeToUtc(dt, kioskTimeZone);
            return result;
        }
    }
}