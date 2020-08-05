using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmetaApplication.Methods
{
    public class ConvertNumericalMoneyToTextMoney
    {
        public static string NumberToWords(long number)
        {
            try
            {
                if (number == 0)
                    return "zero";

                if (number < 0)
                    return "-" + NumberToWords(Math.Abs(number));

                string words = "";
                if ((number / 1000000000) > 0)
                {
                    words += NumberToWords(number / 1000000000) + " миллиард ";
                    number %= 1000000000;
                }
                if ((number / 1000000) > 0)
                {
                    words += NumberToWords(number / 1000000) + " миллион ";
                    number %= 1000000;
                }

                if ((number / 1000) > 0)
                {
                    words += NumberToWords(number / 1000) + " тысячь ";
                    number %= 1000;
                }

                if ((number / 100) > 0)
                {
                    var hundredsMap = new[] { "ноль", "сто", "двести", "триста", "четыреста", "пятьсот", "шестьсот", "семьсот", "восемьсот", "девятьсот" };
                    words += " " + hundredsMap[(number / 100)] + " ";
                    number %= 100;
                }

                if (number > 0)
                {
                    var unitsMap = new[] { "ноль", "один", "два", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять", "десять", "одиннадцать ", "двенадцать", "тринадцать", "четырнадцать", "пятнадцать", "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать" };
                    var tensMap = new[] { "ноль", "десять", "двадцать", "тридцать", "сорок", "пятьдесят", "шестьдесят", "семьдесят", "восемьдесят", "девяносто" };


                    if (number < 20)
                        words += unitsMap[number];
                    else
                    {
                        words += tensMap[number / 10];
                        if ((number % 10) > 0)
                            words += " " + unitsMap[number % 10];
                    }

                }

                return words;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return "";
            }

        }
    }
}
