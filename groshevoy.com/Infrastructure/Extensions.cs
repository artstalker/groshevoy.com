using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace groshevoy.com.Infrastructure
{
    public static class Extensions
    {
        public static string ToConfigLocalTime(this DateTime utcDT)
        {
            //var timeZones = TimeZoneInfo.GetSystemTimeZones();

            var istTZ = TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["Timezone"]);
            return String.Format("{0} ({1})", TimeZoneInfo.ConvertTimeFromUtc(utcDT, istTZ).ToShortDateString(), ConfigurationManager.AppSettings["TimezoneAbbr"]);
        }
    }
}