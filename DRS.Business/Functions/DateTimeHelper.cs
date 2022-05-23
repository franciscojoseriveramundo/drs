using System;
using System.Collections.Generic;
using System.Text;

namespace DRS.Business.Functions
{
    public static class DateTimeHelper
    {
        public static DateTime getCurrentDateTimeWithTimeZone(DateTime dt, string strTimeZone = "Central Standard Time (Mexico)")
        {
            var localTimezone = TimeZoneInfo.Local;
            var userTimezone = TimeZoneInfo.FindSystemTimeZoneById(strTimeZone);

            var todayDate = DateTime.UtcNow;
            var todayLocal = new DateTimeOffset(dt,
                                                localTimezone.GetUtcOffset(dt));

            var todayUserOffset = TimeZoneInfo.ConvertTime(todayLocal, userTimezone);
            return todayUserOffset.DateTime;

        }
    }
}
