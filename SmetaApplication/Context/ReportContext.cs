using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using SmetaApplication.Models.Report;
using System.IO;
using System.Windows;

namespace SmetaApplication.Context
{
    [Serializable]
    public class ReportContext
    {
        #region Properties
        public List<ReportStructure> UnfavourableMonth { get; set; }
        public ReportStructure SelectedUnfavourableMonth { get; set; }

        public List<ReportStructure> UnfavourableTime { get; set; }
        public ReportStructure SelectedUnfavourableTime { get; set; }

        public List<ReportStructure> Temperature { get; set; }
        public ReportStructure SelectedTemperature { get; set; }

        public List<ReportStructure> InternalTransportKm { get; set; }
        public ReportStructure SelectedInternalTransportKm { get; set; }

        public List<ReportStructure> ExternalTransportKm { get; set; }
        public ReportStructure SelectedExternalTransportKm { get; set; }

        public List<ReportStructure> ExternalTransportMonth { get; set; }
        public ReportStructure SelectedExternalTransportMonth { get; set; }

        public List<ReportStructure> TypeExplore { get; set; }
        public ReportStructure SelectedTypeExplore { get; set; }

        private string path = Directory.GetCurrentDirectory() + @"\Files\ReportSettings.dat";

        private string CurrentDirectory = Directory.GetCurrentDirectory() + @"\Files\";

        #endregion

        #region Properties Get koeffisent
        public double UnfavourableKoef
        {
            get
            {
                //double[,] d = GetMatrixFromFile(file_name: "Unfavourable.txt");
                //MessageBox.Show(SelectedUnfavourableTime + " : " + SelectedUnfavourableTime.N +
                //    "\n" + SelectedUnfavourableMonth + " : " + SelectedUnfavourableMonth.N);
                double[,] d =
                {
                    {1,1,1,1,1,1,1,1,1,1},
                    {1,1,1,1.11,1.11,1,1,1,1,1},
                    {1,1,1,1.11,1.11,1.11,1,1,1,1},
                    {1,1,1.05,1.11,1.18,1.18,1,1,1,1},
                    {1,1,1.11,1.18,1.25,1.25,1.18,1,1,1},
                    {1,1,1.18,1.25,1.43,1.43,1.25,1.18,1,1},
                    {1,1.11,1.25,1.43,1.67,1.67,1.43,1.25,1,1}
                };
                return d[SelectedUnfavourableTime.N, SelectedUnfavourableMonth.N];
            }
            private set { }
        }

        public double TemperatureKoef
        {
            get
            {
                return (double)SelectedTemperature.Percentage;
            }
        }

        public double TypeExploreKoef
        {
            get
            {
                return 1 + (double)SelectedTypeExplore.Percentage / 100;
            }
        }

        public double InternalKoef(double PolPrice)
        {
            int j = PolPrice <= 500000 ? 0 :
                PolPrice <= 1000000 ? 1 :
                PolPrice <= 2000000 ? 2 :
                PolPrice <= 5000000 ? 3 : 4;
            double[,] d =
            {
                {8.75,7.5,6.25,5.0,3.75},
                {11.25,10,8.75,7.5,6.25},
                {13.75,12.5,11.75,10,8.15},
                {16.25,15,13.75,12.5,11.25},
                {18.75,17.5,16.25,15,13.75}
            };

            return 1 + d[SelectedInternalTransportKm.N, j] / 100;

        }

        public double ExternalKoef
        {
            get
            {
                double[,] d =
                {
                    {14,11.48,9.1,4.2,3.22,2.52},
                    {19.6,15.4,12.7,6.16,4.76,3.64},
                    {25.2,21.0,16.8,8.12,6.3,4.76},
                    {30.8,25.2,19.6,9.66,7.28,5.46},
                    {36.4,32.2,28.0,13.16,9.80,7.27}
                };
                return 1 + d[SelectedExternalTransportKm.N, SelectedExternalTransportMonth.N] / 100;
            }
        }

        private double[,] GetMatrixFromFile(string file_name)
        {
            file_name = CurrentDirectory + file_name;
            double[,] d;
            int n, m;
            try
            {
                StreamReader reader = new StreamReader(file_name);
                string[] lines = reader.ReadToEnd().Split('\n');
                string[] array = lines[0].Split(' ');
                n = lines.Length;
                m = array.Length;

                d = new double[n, m];

                for (int i = 0; i < n; i++)
                {
                    array = lines[i].Split(' ');
                    for (int j = 0; j < m; j++)
                    {
                        d[i, j] = double.Parse(array[j]);
                    }
                }
                return d;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            return null;
        }

        public double OtherExpenditure { get; set; }

        #endregion

        public ReportContext()
        {
            UnfavourableMonth = new List<ReportStructure>
            {
                new ReportStructure()
                {
                    Name = "Сентябрь",
                },
                new ReportStructure()
                {
                    Name = "Октябрь"
                },
                new ReportStructure()
                {
                    Name = "Ноябрь"
                },
                new ReportStructure()
                {
                    Name = "Декабрь"
                },
                new ReportStructure()
                {
                    Name = "Январь"
                },
                new ReportStructure()
                {
                    Name = "Февраль"
                },
                new ReportStructure()
                {
                    Name = "Март"
                },
                new ReportStructure()
                {
                    Name = "Апрель"
                },
                new ReportStructure()
                {
                    Name = "Май"
                },
                new ReportStructure()
                {
                    Name = "Июнь"
                },
            };
            SelectedUnfavourableMonth = UnfavourableMonth[0];
            SetNumber(UnfavourableMonth);

            UnfavourableTime = new List<ReportStructure>
            {
                new ReportStructure()
                {
                    Name = "1",
                },
                new ReportStructure()
                {
                    Name = "2"
                },
                new ReportStructure()
                {
                    Name = "3"
                },
                new ReportStructure()
                {
                    Name = "4"
                },
                new ReportStructure()
                {
                    Name = "5"
                },
                new ReportStructure()
                {
                    Name = "6"
                },
                new ReportStructure()
                {
                    Name = "7"
                },
            };
            SelectedUnfavourableTime = UnfavourableTime[0];
            SetNumber(UnfavourableTime);

            Temperature = new List<ReportStructure>
            {
                new ReportStructure()
                {
                    Name = "От 0 до минус 10",
                    Percentage = 1.1,
                },
                new ReportStructure()
                {
                    Name = "От минус 10 до минус 20",
                    Percentage = 1.2,
                },
                new ReportStructure()
                {
                    Name = "От минус 20 до минус 30",
                    Percentage = 1.25,
                },
                new ReportStructure()
                {
                    Name = "От минус 30 до минус 40",
                    Percentage = 1.35,
                },
                new ReportStructure()
                {
                    Name = "От минус 40",
                    Percentage = 1.5,
                },
            };
            SelectedTemperature = Temperature[0];
            SetNumber(Temperature);

            InternalTransportKm = new List<ReportStructure>
            {
                new ReportStructure()
                {
                    Name = "До-5",
                },
                new ReportStructure()
                {
                    Name = "5-10"
                },
                new ReportStructure()
                {
                    Name = "10-15"
                },
                new ReportStructure()
                {
                    Name = "15-20"
                },
                new ReportStructure()
                {
                    Name = "20-25"
                },
            };
            SelectedInternalTransportKm = InternalTransportKm[0];
            SetNumber(InternalTransportKm);

            ExternalTransportKm = new List<ReportStructure>
            {
                new ReportStructure()
                {
                    Name = "25-100",
                },
                new ReportStructure()
                {
                    Name = "100-300"
                },
                new ReportStructure()
                {
                    Name = "300-500"
                },
                new ReportStructure()
                {
                    Name = "500-1000"
                },
                new ReportStructure()
                {
                    Name = "1000-2000"
                },
            };
            SelectedExternalTransportKm = ExternalTransportKm[0];
            SetNumber(ExternalTransportKm);

            ExternalTransportMonth = new List<ReportStructure>
            {
                new ReportStructure()
                {
                    Name = "1",
                },
                new ReportStructure()
                {
                    Name = "2"
                },
                new ReportStructure()
                {
                    Name = "3"
                },
                new ReportStructure()
                {
                    Name = "6"
                },
                new ReportStructure()
                {
                    Name = "9"
                },
                new ReportStructure()
                {
                    Name = "12"
                },
            };
            SelectedExternalTransportMonth = ExternalTransportMonth[0];
            SetNumber(ExternalTransportMonth);

            TypeExplore = new List<ReportStructure>
            {
                new ReportStructure()
                {
                    Name = "Инженерно-геологические",
                    Percentage = 4,
                },
                new ReportStructure()
                {
                    Name = "Геофизические работы",
                    Percentage = 11,
                },
                new ReportStructure()
                {
                    Name = "Инженерно-геодезические, геологосъемочные, гидрометеорологические и др.",
                    Percentage = 6,
                },
            };
            SelectedTypeExplore = TypeExplore[0];
            SetNumber(TypeExplore);

            //if (File.Exists(path))
            //{
            //    try
            //    {
            //        BinaryFormatter formatter = new BinaryFormatter();
            //        using (FileStream fs = new FileStream(path, FileMode.Open))
            //        {
            //            SelectedInternalTransportKm.N = (int)formatter.Deserialize(fs);
            //            SelectedInternalTransportKm = InternalTransportKm[SelectedInternalTransportKm.N];

            //            SelectedExternalTransportKm.N = (int)formatter.Deserialize(fs);
            //            SelectedExternalTransportKm = ExternalTransportKm[SelectedExternalTransportKm.N];

            //            SelectedExternalTransportMonth.N = (int)formatter.Deserialize(fs);
            //            SelectedExternalTransportMonth = ExternalTransportMonth[SelectedExternalTransportMonth.N];

            //            SelectedTypeExplore.N = (int)formatter.Deserialize(fs);
            //            SelectedTypeExplore = TypeExplore[SelectedTypeExplore.N];

            //            SelectedUnfavourableMonth.N = (int)formatter.Deserialize(fs);
            //            SelectedUnfavourableMonth = UnfavourableMonth[SelectedUnfavourableMonth.N];

            //            SelectedUnfavourableTime.N = (int)formatter.Deserialize(fs);
            //            SelectedUnfavourableTime = UnfavourableTime[SelectedUnfavourableTime.N];

            //            SelectedTemperature.N = (int)formatter.Deserialize(fs);
            //            SelectedTemperature = Temperature[SelectedTemperature.N];
            //        }
            //    }
            //    catch
            //    {

            //    }
            //}
        }

        private void SetNumber(List<ReportStructure> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].N = i;
            }

            OtherExpenditure = 21;
        }

        ~ReportContext()
        {
            //BinaryFormatter formatter = new BinaryFormatter();
            //using (FileStream fs = new FileStream(path, FileMode.Create))
            //{
            //    formatter.Serialize(fs, SelectedInternalTransportKm.N);
            //    formatter.Serialize(fs, SelectedExternalTransportKm.N);
            //    formatter.Serialize(fs, SelectedExternalTransportMonth.N);
            //    formatter.Serialize(fs, SelectedTypeExplore.N);
            //    formatter.Serialize(fs, SelectedUnfavourableMonth.N);
            //    formatter.Serialize(fs, SelectedUnfavourableTime.N);
            //    formatter.Serialize(fs, SelectedTemperature.N);
            //}
        }
    }
}
