using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TyperacerFreeDataDownloader
{
    class Utils
    {
        public static int FormatRace(string race)
        {
            return Convert.ToInt32(race);
        }

        public static int FormatSpeed(string speed)
        {
            return Convert.ToInt32(speed.Substring(0, 2));
        }
        public static decimal FormatAccuracy(string accuracy)
        {
            return Convert.ToDecimal(accuracy.Substring(0, 2));
        }

        public static int FormatPoints(string points)
        {
            return Convert.ToInt32(points);
        }

        public static string FormatRowToCSV(Row row)
        {
            return $"{row.Race},{row.Speed},{row.Accuracy},{row.Points},{row.Place},{row.Date}";
        }
    }
}
