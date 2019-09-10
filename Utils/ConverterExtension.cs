using System;

namespace computer_check.Utils
{
    public static class ConverterExtension
    {
        public static string AsString(this object obj)
        {
            if (obj != null && obj.ToString() != "")
                return obj.ToString().Trim();

            return null;
        }

        public static DateTime? AsDate(this object obj)
        {
           if (obj != null && 
                DateTime.TryParseExact(
                    obj.ToString().Substring(0, 8),
                    "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out DateTime date))
            {
                return date;
            }

            return null;
        }

        public static bool AsBoolean(this object obj)
        {
            if (Boolean.TryParse(obj.AsString(), out bool boolean))
                return boolean;

            return false;
        }

        public static string AsGigabyte(this object obj)
        {
            if (long.TryParse(obj.AsString(), out long number))
                return $"{(Math.Round((decimal)number / 1024 / 1024 / 1024, 1))} GB";

            return null;
        }

         public static string AsUser(this object obj)
        {
           var user = obj.AsString();

            if (user != null && user.Contains("\\"))
                return user.Split("\\")[1];

            return user;
        }
    }
}