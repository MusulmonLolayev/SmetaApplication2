using SmetaApplication.Models.GroupMaterial;
using SmetaApplication.Models.Material;
using SmetaApplication.Models.WorkModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmetaApplication.Methods
{
    public class Helper
    {
        public const int ToHourKoef = 720;

        public const double ToPriceOther = 0.21;

        public const double ToPriceProfit = 0.1;

        public static string getName(int i)
        {
            switch (i)
            {
                case 0: return "Полевые";
                case 1: return "Камеральные";
                case 2: return "Лабораторные";
                default: return "Ошибка:";
            }
        }

        public static double? Time(Work Work, int Diff)
        {
            double? res = 0;
            Diff++;
            switch (Diff)
            {
                case 1: res = Work.Time1; break;
                case 2: res = Work.Time2; break;
                case 3: res = Work.Time3; break;
                case 4: res = Work.Time4; break;
                case 5: res = Work.Time5; break;
                case 6: res = Work.Time6; break;
                case 7: res = Work.Time7; break;
                case 8: res = Work.Time8; break;
                case 9: res = Work.Time9; break;
                case 10: res = Work.Time10; break;
                case 11: res = Work.Time11; break;
                case 12: res = Work.Time12; break;
                case 13: res = Work.Time13; break;
                case 14: res = Work.Time14; break;
                case 15: res = Work.Time15; break;
            }
            return res;
        }

        public static double? Count(MaterialGroup MaterialGroup, int Diff)
        {
            double? res = 0;
            switch (Diff)
            {
                case 1: res = MaterialGroup.Count1; break;
                case 2: res = MaterialGroup.Count2; break;
                case 3: res = MaterialGroup.Count3; break;
                case 4: res = MaterialGroup.Count4; break;
                case 5: res = MaterialGroup.Count5; break;
                case 6: res = MaterialGroup.Count6; break;
                case 7: res = MaterialGroup.Count7; break;
                case 8: res = MaterialGroup.Count8; break;
                case 9: res = MaterialGroup.Count9; break;
                case 10: res = MaterialGroup.Count10; break;
                case 11: res = MaterialGroup.Count11; break;
                case 12: res = MaterialGroup.Count12; break;
                case 13: res = MaterialGroup.Count13; break;
                case 14: res = MaterialGroup.Count14; break;
                case 15: res = MaterialGroup.Count15; break;
            }
            return res;
        }

        public static List<bool> WorkCount(Work Work)
        {
            List<bool> list = new List<bool>();
            list.Add(Work.Time1 != 0 && Work.Time1 != 0 ? true : false);
            list.Add(Work.Time2 != null && Work.Time2 != 0 ? true : false);
            list.Add(Work.Time3 != null && Work.Time3 != 0 ? true : false);
            list.Add(Work.Time4 != null && Work.Time4 != 0 ? true : false);
            list.Add(Work.Time5 != null && Work.Time5 != 0 ? true : false);
            list.Add(Work.Time6 != null && Work.Time6 != 0 ? true : false);
            list.Add(Work.Time7 != null && Work.Time7 != 0 ? true : false);
            list.Add(Work.Time8 != null && Work.Time8 != 0 ? true : false);
            list.Add(Work.Time9 != null && Work.Time9 != 0 ? true : false);
            list.Add(Work.Time10 != null && Work.Time10 != 0 ? true : false);
            list.Add(Work.Time11 != null && Work.Time11 != 0 ? true : false);
            list.Add(Work.Time12 != null && Work.Time12 != 0 ? true : false);
            list.Add(Work.Time13 != null && Work.Time13 != 0 ? true : false);
            list.Add(Work.Time14 != null && Work.Time14 != 0 ? true : false);
            list.Add(Work.Time15 != null && Work.Time15 != 0 ? true : false);
            return list;
        }

        public static List<string> Diffucults = new List<string>()
        {
            "I",
            "II",
            "III",
            "IV",
            "V",
            "VI",
            "VII",
            "VIII",
            "IX",
            "X",
            "XI",
            "XII",
            "XIII",
            "XIV",
            "XV"
        };

        public const double Rayoniy = 1.15;

        public static int Max(params int[] a)
        {
            return a.Max();
        }

        public static DateTime ToDateTime(object str)
        {
            DateTime dateTime;
            //MessageBox.Show(str.ToString());
            string[] array = str.ToString().Split('.');
            int day = int.Parse(array[0]); ;
            int month = int.Parse(array[1]);
            int year = int.Parse(array[2]);
            dateTime = new DateTime(year, month, day);
            return dateTime;
        }

        public static string ToString(DateTime dateTime)
        {
            return dateTime.Day + "." + dateTime.Month + "." + dateTime.Year;
        }

        public static bool? ToBool(object str)
        {
            if (str == null)
                return null;
            string s = str.ToString();
            return s == "1" ? true : false;
        }

        public static string ToString(double? d)
        {
            if (d == null)
                return "0.0";
            return d.ToString().Replace(',', '.');
        }

        public static int ToInt(bool? status)
        {
            return status == true ? 1 : 0;
        }

        public static string ToStringNull(double? d)
        {
            if (d == null)
                return "null";
            else
                return d.ToString().Replace(',', '.');
        }

        public static double? ToDoubleNull(string str)
        {
            if (str == null || str == "")
                return null;
            else return double.Parse(str);
        }

        public static double? GetMaterialCount(string kot, MaterialGroup materialGroup)
        {
            int i = 0;
            for (; i < Diffucults.Count; i++)
            {
                if (Diffucults[i].CompareTo(kot) == 0)
                {
                    break;
                }
            }
            i++;
            return Count(materialGroup, i);
        }

        public static void ToTimeFromArray(Work Work, string [] split)
        {
            double time;
            bool IsBegin = true;
            if (double.TryParse(split[3].Replace('.', ','), out time))
            {
                Work.Time1 = time;
            }
            //else
            //    IsBegin = false;

            if (IsBegin && double.TryParse(split[4].Replace('.', ','), out time))
            {
                Work.Time2 = time;
            }
            //else
            //    IsBegin = false;

            if (IsBegin && double.TryParse(split[5].Replace('.', ','), out time))
            {
                Work.Time3 = time;
            }
            //else
            //    IsBegin = false;

            if (IsBegin && double.TryParse(split[6].Replace('.', ','), out time))
            {
                Work.Time4 = time;
            }
            //else
            //    IsBegin = false;

            if (IsBegin && double.TryParse(split[7].Replace('.', ','), out time))
            {
                Work.Time5 = time;
            }
            //else
            //    IsBegin = false;
        }

        public static void ToTimeFromArray(MaterialGroup material, string[] split)
        {
            double time;
            bool IsBegin = true;
            if (double.TryParse(split[3].Replace('.', ','), out time))
            {
                material.Count1 = time;
            }
            //else
            //    IsBegin = false;

            if (IsBegin && double.TryParse(split[4].Replace('.', ','), out time))
            {
                material.Count2 = time;
            }
            //else
            //    IsBegin = false;

            if (IsBegin && double.TryParse(split[5].Replace('.', ','), out time))
            {
                material.Count3 = time;
            }
            //else
            //    IsBegin = false;

            if (IsBegin && double.TryParse(split[6].Replace('.', ','), out time))
            {
                material.Count4 = time;
            }
            //else
            //    IsBegin = false;

            if (IsBegin && double.TryParse(split[7].Replace('.', ','), out time))
            {
                material.Count5 = time;
            }
            //else
            //    IsBegin = false;
        }

        public static string ClearZeros(string s)
        {
            while(s[0] == '0')
            {
                s = s.Remove(0, 1);
            }
            return s;
        }

        public static double? ToAmortizationinHour(Pribor pribor)
        {
            if (pribor != null)
                return (pribor.Percent * pribor.Price) / 100 / 12 / ToHourKoef;
            else return null;
        }

        public static string FillWithZero(string str)
        {
            while (str.Length < 5)
            {
                str = "0" + str;
            }
            return str;
        }

        public static string ToLowerFirstLetter(string str)
        {
            string result = str[0].ToString();
            result = result.ToUpper();
            result = result + str.Substring(1);
            return result;
        }
    }
}
