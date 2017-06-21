using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenEngine
{
    public static class StringExtensions
    {

        public static string[] Split(this string str, string delimeter, StringSplitOptions options = StringSplitOptions.None)
        {
            return str.Split(new string[] { delimeter }, options);
        }

        public static string Trim(this string str, string trimString)
        {
            return str.Trim(trimString.ToCharArray());
        }

    }

    public static class ListExtensions
    {

        public static List<T> Copy<T>(this List<T> copyList)
        {
            return new List<T>(copyList);
        }

    }

    public static class ObjectExtensions
    {

        public static string ToDateString(this DateTime dt)
        {
            return dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Day + "_" + dt.Month + "_" + dt.Year;
        }

    }

}
