using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using SmetaApplication.DbContexts;
using System.Linq;
using SmetaApplication.Windows.List;
using SmetaApplication.Models.WorkModels;
using System.Collections.Generic;
using SmetaApplication.Context;
using SmetaApplication.Methods;
using System.IO;
using SmetaApplication.Models.GroupMaterial;
using SmetaApplication.Models.Material;
using SmetaApplication.Filtrs;
using SmetaApplication.Windows.Other;
using System.Windows.Input;
using SmetaApplication.Windows.Adds;
using SmetaApplication.Models.Contract;

namespace SmetaApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker BackgroundWorker = new BackgroundWorker();
        public WorkFilter Filtr;

        public MainWindowContext MainWindowContext { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DBConnection.SetDefaultSettings();
            MainWindowContext = new MainWindowContext();
            DataContext = MainWindowContext;
            Filtr = new WorkFilter();


            InsertFromTable.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Control));

        }
                        
        private void OnClickNew(object sender, RoutedEventArgs e)
        {
            try
            {
                Contract contract = new Contract();
                WindowAddContract window = new WindowAddContract(contract);
                if (window.ShowDialog() == true)
                {
                    contract.Save();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void btnOpenContract(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowContractList window = new WindowContractList();
                if (window.ShowDialog() == true)
                {

                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void AddToCount(object sender, EventArgs e)
        {
            MainWindowContext.AddWorkToCount(sender);
        }

        private void ClickMaterialWindowShow(object sender, RoutedEventArgs e)
        {
            WindowMaterialList window = new WindowMaterialList();
            window.ShowDialog();
        }

        private void ClickPostWindowShow(object sender, RoutedEventArgs e)
        {
            WindowPostList window = new WindowPostList();
            window.ShowDialog();
        }

        private void ClickWorkWindowShow(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowWorkList window = new WindowWorkList(Filtr);
                window.ShowDialog();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            
        }

        private void ClickPriborWindowShow(object sender, RoutedEventArgs e)
        {
            WindowPriborList window = new WindowPriborList();
            window.ShowDialog();
        }

        private void ClickWorkTypeWindowShow(object sender, RoutedEventArgs e)
        {
            WindowWorkTypeList window = new WindowWorkTypeList();
            window.ShowDialog();
        }

        private void ClickWorkSectionWindowShow(object sender, RoutedEventArgs e)
        {
            WindowWorkSectionList window = new WindowWorkSectionList();
            window.ShowDialog();
        }

        private void Click_Search(object sender, RoutedEventArgs e)
        {
            //StreamReader reader = new StreamReader(new FileStream(@"D:\data.txt", FileMode.Open));
            //try
            //{
            //    string[] lines = reader.ReadToEnd().Split('\n');
            //    for (int i = 0; i < lines.Length - 1; i++)
            //    {
            //        string[] line0 = lines[i].Split('\t');
            //        string[] line1 = lines[i + 1].Split('\t');
            //        WorkSection section = new WorkSection();
            //        section.Name = line0[0];
            //        section.Place = int.Parse(line0[2]);
            //        int begin = int.Parse(line0[1]);
            //        int end = int.Parse(line1[1]);
            //        section.Save();
            //        section.WorkTypeId = 1;

            //        using (var db = new SmetaDbAppContext())
            //        {
            //            db.Works.ForEach(x =>
            //            {
            //                int number = int.Parse(x.Number);
            //                if (begin <= number && number < end)
            //                {
            //                    x.WorkSectionId = section.Id;
            //                    x.Save();
            //                }
            //            });
            //        }
            //    }
            //}
            //catch (Exception exc)
            //{
            //    MessageBox.Show(exc.ToString());
            //}
            //if (true) return;
            //string path = @"D:\\import.txt";
            //StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open), System.Text.Encoding.UTF8);

            //try
            //{
            //    List<string> lines = sr.ReadToEnd().Split('\n').ToList();
            //    //for (int i = 0; i < lines.Count; i++)
            //    //{
            //    //    bool t = true;
            //    //    string[] array = lines[i].Split('\t');
            //    //    for (int j = 0; j < array.Length - 1; j++)
            //    //    {
            //    //        //MessageBox.Show("=" + array[j] + "=");
            //    //        if (!string.IsNullOrEmpty(array[j]))
            //    //        {
            //    //            t = false;
            //    //            break;
            //    //        }
            //    //    }
            //    //    if (t)
            //    //    {
            //    //        lines.RemoveAt(i);
            //    //        i--;
            //    //    }
            //    //}

            //    for (int i = 0; i < lines.Count - 1;)
            //    {
            //        string allnumber = lines[i];
            //        //MessageBox.Show(allnumber);
            //        // next Name
            //        i++;
            //        // next Measure
            //        i++;
            //        // next KS
            //        i++;
            //        // next KS In number
            //        i++;
            //        // next Numbers
            //        i++;
            //        // next Part 1
            //        i++;

            //        // Works
            //        i++;
            //        string[] array = lines[i].Split('\t');
            //        string number = array[0];
            //        //MessageBox.Show("=" + number + "=");
            //        using (var db = new SmetaDbAppContext())
            //        {
            //            Work work = db.Works.Find(x => int.Parse(x.Number) == int.Parse(number));

            //            if (work != null)
            //            {
            //                //work.Number = allnumber;
            //                // next Part 2
            //                i++;
            //                // first row of the pribors
            //                i += 1;
            //                array = lines[i].Split('\t');
            //                Helper.ToTimeFromArray(work, array);
            //                int code;
            //                double s = 0;
            //                double d;

            //                // clear pribors
            //                //DBConnection.SqlQuery("Delete From PriborGroups Where WorkId = " + work.Id);
            //                db.PriborGroups.Where(x => x.WorkId == work.Id).ToList().ForEach(x =>
            //                {
            //                    x.Delete();
            //                });


            //                while (i < lines.Count() && array.Length > 0 && int.TryParse(array[0], out code))
            //                {
            //                    Pribor pribor = db.Pribors.Find(x => int.Parse(x.Code) == code);
            //                    // if the pribor be, then must update. Else entre new pribor.
            //                    if (pribor != null)
            //                    {
            //                        //array[10] = array[0].Remove(array[0].Length - 1);
            //                        //MessageBox.Show("=" + array[10] + "=");
            //                        if (double.TryParse(array[10], out d))
            //                            pribor.Price = d;
            //                        pribor.Update();
            //                    }
            //                    else
            //                    {
            //                        pribor = new Pribor()
            //                        {
            //                            Code = array[0],
            //                            Name = array[1],
            //                            Dimension = array[2],
            //                            Percent = 15,
            //                        };
            //                        if (double.TryParse(array[10], out d))
            //                            pribor.Price = d;
            //                        else
            //                            pribor.Price = 100;
            //                        pribor.Save();
            //                    }
            //                    //MessageBox.Show(array[1]);
            //                    // add pribor group
            //                    //MessageBox.Show("=" + array[8] + "=");
            //                    PriborGroup priborGroup = new PriborGroup()
            //                    {
            //                        PriborId = pribor.Id,
            //                        WorkId = work.Id,
            //                        Count = double.Parse(array[8].Replace('.', ','))
            //                    };
            //                    priborGroup.Save();

            //                    s += priborGroup.Count * (pribor.Price * pribor.Percent * 0.01) / (12 * 30 * 24);

            //                    i++;
            //                    array = lines[i].Split('\t');
            //                    //MessageBox.Show(array[0]);
            //                }

            //                work.PricePribor = s;

            //                // begin material
            //                // clear materials
            //                //DBConnection.SqlQuery("Delete From MaterialGroups Where WorkId = " + work.Id);
            //                db.MaterialGroups.Where(x => x.WorkId == work.Id).ToList().ForEach(x =>
            //                {
            //                    x.Delete();
            //                });
            //                i++;
            //                array = lines[i].Split('\t');
            //                s = 0;
            //                while (i < lines.Count() && array.Length > 0 && int.TryParse(array[0], out code))
            //                {
            //                    Material material = db.Materials.Find(x => int.Parse(x.Code) == code);
            //                    // if the pribor be, then must update. Else entre new pribor.
            //                    if (material != null)
            //                    {
            //                        //MessageBox.Show(material.Id.ToString());
            //                        if (double.TryParse(array[10], out d))
            //                            material.Price = d;
            //                        material.Update().ToString();
            //                    }
            //                    else
            //                    {
            //                        material = new Material()
            //                        {
            //                            Code = array[0],
            //                            Name = array[1],
            //                            Dimension = array[2],
            //                        };
            //                        if (double.TryParse(array[10], out d))
            //                            material.Price = d;
            //                        else
            //                            material.Price = 100;
            //                        material.Save();
            //                    }

            //                    // add pribor group
            //                    MaterialGroup materialGroup = new MaterialGroup()
            //                    {
            //                        MaterialId = material.Id,
            //                        WorkId = work.Id,
            //                    };

            //                    Helper.ToTimeFromArray(materialGroup, array);

            //                    materialGroup.Save();

            //                    s += material.Price * materialGroup.Count1;

            //                    i++;
            //                    if (i < lines.Count)
            //                        array = lines[i].Split('\t');
            //                }
            //                work.PriceMaterial = s;

            //                work.Update();
            //            }
            //        }
            //    }
            //    StreamWriter sw = new StreamWriter(@"D:\Programs\Projects\C#\Smeta\Documents 2\base in.txt", true);
            //    lines.ForEach(x => { sw.Write(x); });
            //    sw.Close();
            //    sr.Close();
            //}
            //catch (Exception exc)
            //{
            //    MessageBox.Show(exc.ToString());
            //    sr.Close();
            //}
        }

        private void OnClickAdd(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateWindow window = new UpdateWindow();
            window.ShowDialog();
        }

        public static RoutedCommand InsertFromTable = new RoutedCommand();

        private void MyCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (Clipboard.ContainsData(DataFormats.Text))
                {
                    List<string> lines = Clipboard.GetText().Split('\n').ToList();
                    //for (int i = 0; i < lines.Count; i++)
                    //{
                    //    bool t = true;
                    //    string[] array = lines[i].Split('\t');
                    //    for (int j = 0; j < array.Length - 1; j++)
                    //    {
                    //        //MessageBox.Show("=" + array[j] + "=");
                    //        if (!string.IsNullOrEmpty(array[j]))
                    //        {
                    //            t = false;
                    //            break;
                    //        }
                    //    }
                    //    if (t)
                    //    {
                    //        lines.RemoveAt(i);
                    //        i--;
                    //    }
                    //}

                    for (int i = 0; i < lines.Count - 1;)
                    {
                        string allnumber = lines[i];
                        //MessageBox.Show(allnumber);
                        // next Name
                        i++;
                        // next Measure
                        i++;
                        // next KS
                        i++;
                        // next KS In number
                        i++;
                        // next Numbers
                        i++;
                        // next Part 1
                        i++;

                        // Works
                        i++;
                        string[] array = lines[i].Split('\t');
                        string number = array[0];
                        //MessageBox.Show("=" + number + "=");
                        using (var db = new SmetaDbAppContext())
                        {
                            Work work = db.Works.Find(x => int.Parse(x.Number) == int.Parse(number) + 216);
                            work.Number2 = number;

                            if (work != null)
                            {
                                //work.Number = allnumber;
                                // next Part 2
                                i++;
                                // first row of the pribors
                                i += 1;
                                array = lines[i].Split('\t');
                                Helper.ToTimeFromArray(work, array);
                                int code;
                                double s = 0;
                                double d;

                                // clear pribors
                                //DBConnection.SqlQuery("Delete From PriborGroups Where WorkId = " + work.Id);
                                db.PriborGroups.Where(x => x.WorkId == work.Id).ToList().ForEach(x =>
                                {
                                    x.Delete();
                                });


                                while (i < lines.Count() && array.Length > 0 && int.TryParse(array[0], out code))
                                {
                                    Pribor pribor = db.Pribors.Find(x => int.Parse(x.Code) == code);
                                    // if the pribor be, then must update. Else entre new pribor.
                                    if (pribor != null)
                                    {
                                        //array[10] = array[0].Remove(array[0].Length - 1);
                                        //MessageBox.Show("=" + array[10] + "=");
                                        if (double.TryParse(array[10], out d))
                                            pribor.Price = d;
                                        pribor.Update();
                                    }
                                    else
                                    {
                                        pribor = new Pribor()
                                        {
                                            Code = array[0],
                                            Name = array[1],
                                            Dimension = array[2],
                                            Percent = 15,
                                        };
                                        if (double.TryParse(array[10], out d))
                                            pribor.Price = d;
                                        else
                                            pribor.Price = 100;
                                        pribor.Save();
                                    }
                                    //MessageBox.Show(array[1]);
                                    // add pribor group
                                    //MessageBox.Show("=" + array[0] + "=");
                                    PriborGroup priborGroup = new PriborGroup()
                                    {
                                        PriborId = pribor.Id,
                                        WorkId = work.Id,
                                        Count = double.Parse(array[8].Replace('.', ','))
                                    };
                                    priborGroup.Save();

                                    s += priborGroup.Count * (pribor.Price * pribor.Percent * 0.01) / (12 * 30 * 24);

                                    i++;
                                    array = lines[i].Split('\t');
                                    //MessageBox.Show(array[0]);
                                }

                                //List<Pribor> pribors = new List<Pribor>
                                //{
                                //    new Pribor()
                                //    {
                                //        Code = "5006"
                                //    },
                                //    new Pribor()
                                //    {
                                //        Code = "5007"
                                //    },
                                //    new Pribor()
                                //    {
                                //        Code = "5008"
                                //    },
                                //    new Pribor()
                                //    {
                                //        Code = "5009"
                                //    },
                                //    new Pribor()
                                //    {
                                //        Code = "5010"
                                //    }
                                //};
                                //int team_count = db.WorkTeams.Where(x => x.WorkDemId == work.Id).Sum(x => x.Count);
                                //pribors.ForEach(x =>
                                //{
                                //    x = db.Pribors.Find(y => int.Parse(x.Code) == int.Parse(y.Code));
                                //    PriborGroup priborGroup = new PriborGroup()
                                //    {
                                //        PriborId = x.Id,
                                //        WorkId = work.Id,
                                //        Count = team_count
                                //    };
                                //    priborGroup.Save();

                                //    s += priborGroup.Count * (x.Price * x.Percent * 0.01) / (12 * 30 * 24);
                                //});

                                work.PricePribor = s;

                                // begin material
                                // clear materials
                                //DBConnection.SqlQuery("Delete From MaterialGroups Where WorkId = " + work.Id);
                                db.MaterialGroups.Where(x => x.WorkId == work.Id).ToList().ForEach(x =>
                                {
                                    x.Delete();
                                });
                                i++;
                                array = lines[i].Split('\t');
                                s = 0;
                                while (i < lines.Count() && array.Length > 0 && int.TryParse(array[0], out code))
                                {
                                    Material material = db.Materials.Find(x => int.Parse(x.Code) == code);
                                    // if the pribor be, then must update. Else entre new pribor.
                                    if (material != null)
                                    {
                                        //MessageBox.Show(material.Id.ToString());
                                        if (double.TryParse(array[10], out d))
                                            material.Price = d;
                                        material.Update().ToString();
                                    }
                                    else
                                    {
                                        material = new Material()
                                        {
                                            Code = array[0],
                                            Name = array[1],
                                            Dimension = array[2],
                                        };
                                        if (double.TryParse(array[10], out d))
                                            material.Price = d;
                                        else
                                            material.Price = 100;
                                        material.Save();
                                    }

                                    // add pribor group
                                    MaterialGroup materialGroup = new MaterialGroup()
                                    {
                                        MaterialId = material.Id,
                                        WorkId = work.Id,
                                    };

                                    Helper.ToTimeFromArray(materialGroup, array);

                                    materialGroup.Save();

                                    s += material.Price * materialGroup.Count1;

                                    i++;
                                    if (i < lines.Count)
                                        array = lines[i].Split('\t');
                                }
                                work.PriceMaterial = s;

                                work.Update();
                            }
                        }
                    }
                    StreamWriter sw = new StreamWriter(@"D:\Programs\Projects\C#\Smeta\Documents 2\base in.txt", true);
                    lines.ForEach(x => { sw.Write(x); });
                    sw.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        
    }
}