using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Backend;
using _5051.Models;

namespace _5051.Maintain
{


    /*
     * Allen
     * 
     * So investigate what is wrong with the current calendar data
     * I am assuming each calendar record needs to be inspected and potentialy fixed up
     * 
     * Best way to do this is to make Unit Tests for each of the issues that can be fixed, write the unit test first,  then the code to fix it.
     * Repeat untill all issues are fixed
     * 
     */

    public class CalendarMaintenance
    {
        // remove duplicate dates in the calendar set
        // pre: the calendar set is ordered by date
        // post: all dates in the calendar set are unique
        public bool ResetCalendar()
        {
            var calendarSet = DataSourceBackend.Instance.SchoolCalendarBackend.Index();
            var cur = calendarSet[0].Date;
            for (int i = 1; i < calendarSet.Count(); i++)
            {
                var item = calendarSet[i];
                if (cur.Equals(item.Date))
                {
                    DataSourceBackend.Instance.SchoolCalendarBackend.Delete(item.Id);
                    i--;
                }
                else
                {
                    cur = item.Date;
                }
            }
            return true;
        }
    }
}