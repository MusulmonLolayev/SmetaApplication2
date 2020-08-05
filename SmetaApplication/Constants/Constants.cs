using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmetaApplication.Constants
{
    public class Constants
    {
        public static List<string> GetPlaceWork()
        {
            List<string> list = new List<string>();
            list.Add("Полывий");
            list.Add("Камеральный");
            list.Add("Лабораторный");
            return list;
        }

        public const int Average_Time = 168;

        public const double Percent25 = 1.25;

        public const double AmartKoefInMonth = 1.0 / 1200;

        public const double PercentInHour = 720;

        public const double RayoniyKoef = 1.15;

        public const double OtherKoef = 0.21;

        public const double OtherFullKoef = 1.21;

        public const double ProfitKoef = 0.1;

        public const double ProfitFullKoef = 1.1;

        public const double NDSKoef = 0.15;

        public const double NDSFullKoef = 1.2;
    }
}
