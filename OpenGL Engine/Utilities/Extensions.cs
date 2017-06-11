using System;
using System.Collections.Generic;
using System.Linq;
using CloneExtensions;

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

        public static T DeepClone<T>(this T obj)
        {
            return obj.GetClone();
        }

    }

}
