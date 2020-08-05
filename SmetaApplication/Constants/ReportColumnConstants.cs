using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmetaApplication.Constants
{
    public class ReportColumnConstants
    {
        // Columns numbers
        // for all
        public const int Number = 1;

        // Part one
        public const int Name = 2;
        public const int Diffucult = 3;
        public const int NumberStandart = 4;
        public const int Time = 5;
        public const int Measure = 6;
        public const int Size = 7;
        public const int TimeForWorker = 8;
        public const int AllTimeForWorker = 9;

        // part two
        public const int Razryad = 3;
        public const int PayInMonth = 4;
        public const int Avarge_Time = 5;
        public const int CountWorkers = 6;
        public const int PayForHour = 7;
        public const int PayPercent25 = 8;
        public const int PricePayForSize = 9;

        // part three
        public const int Koef_Value = 3;
        public const int Polviy_Dovol = 4;
        public const int Koef_All_Pay = 9;

        //part four
        public const int Material_Code = 3;
        public const int Material_Measure = 4;
        public const int Material_Price = 5;
        public const int Material_Size = 6;
        public const int Material_ForAllSize = 7;
        public const int Material_Price_ForCount = 9;
        
        // part five
        public const int Pribor_Code = 3;
        public const int Pribor_Measure = 4;
        public const int Pribor_Price = 5;
        public const int Pribor_Amart_Percent = 6;
        public const int Pribor_Amart_Price_Month = 7;
        public const int Pribor_Amart_Price_Hour = 8;
        public const int Pribor_Amart_Price_For_Size = 9;

        // part six
        public const int All_Price_Pay = 2;
        public const int All_Price_Material = 3;
        public const int All_Price_Pribor = 4;
        public const int All_Price = 5;
        public const int Other_Price = 6;
        public const int All_Profit = 7;
        public const int All_Price_Currect_Process = 9;

        // Report to excell constants
        public static int row_1_pol = 11;
        public static int row_1_lab = 12;
        public static int row_1_kam = 13;

        public static int row_2 = 16;
        public static int row_3 = 20;
        public static int row_4 = 23;
        public static int row_5 = 27;
        public static int row_6 = 31;
    }
}
