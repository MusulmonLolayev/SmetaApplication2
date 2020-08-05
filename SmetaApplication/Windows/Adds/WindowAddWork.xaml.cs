using SmetaApplication.Context;
using SmetaApplication.Methods;
using SmetaApplication.Models.Material;
using SmetaApplication.Models.WorkModels;
using SmetaApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmetaApplication.Windows.Adds
{
    /// <summary>
    /// Логика взаимодействия для WindowAddWork.xaml
    /// </summary>
    public partial class WindowAddWork : Window
    {
        WorkContext WorkContext;

        public static RoutedCommand InsertFromTable = new RoutedCommand();

        public WindowAddWork(WorkContext WorkContext)
        {
            InitializeComponent();
            this.WorkContext = WorkContext;
            DataContext = WorkContext;
            InsertFromTable.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Control));
        }

        private void OnClickEditTeam(object sender, RoutedEventArgs e)
        {
            WindowAddTeamGroup window = new WindowAddTeamGroup(WorkContext.TeamContexts);
            if (window.ShowDialog() == true)
            {
                //WorkContext.TeamContexts.Clear();
                //foreach (var item in window.list)
                //{
                //    if (item.Count != 0)
                //        WorkContext.TeamContexts.Add(item);
                //}

                foreach (var item in window.list)
                {
                    bool IsYes = WorkContext.TeamContexts.Where(x => x.Equals(item)).Any();
                    if (!IsYes && item.Count != 0)
                        WorkContext.TeamContexts.Add(item);
                    else
                    {
                        if (IsYes && item.Count == 0)
                        {
                            item.WorkTeam.Delete();
                            WorkContext.TeamContexts.Remove(item);
                        }
                    }
                }
            }
        }

        private void OnClickEditMaterial(object sender, RoutedEventArgs e)
        {
            WindowAddFromMaterialList window = new WindowAddFromMaterialList(WorkContext.MaterailContexts);
            if (window.ShowDialog() == true)
            {
                //WorkContext.MaterailContexts.Clear();
                //foreach (var item in window.list)
                //{
                //    if (item.MaterialGroup.Count1 != 0)
                //    {
                //        WorkContext.MaterailContexts.Add(item);
                //    }
                //}

                foreach (var item in window.list)
                {
                    bool IsYes = WorkContext.MaterailContexts.Where(x => x.Equals(item)).Any();
                    if (!IsYes && item.MaterialGroup.Count1 != 0)
                    {
                        WorkContext.MaterailContexts.Add(item);
                    }
                    else
                    {
                        if (IsYes && item.MaterialGroup.Count1 == 0)
                        {
                            item.MaterialGroup.Delete();
                            WorkContext.MaterailContexts.Remove(item);
                        }
                    }
                }
            }
        }

        private void OnClickEditPribor(object sender, RoutedEventArgs e)
        {
            WindowAddFromPriborList window = new WindowAddFromPriborList(WorkContext.PriborContexts);
            if (window.ShowDialog() == true)
            {
                //WorkContext.PriborContexts.Clear();
                //foreach (var item in window.list)
                //{
                //    if (item.PriborGroup.Count != 0)
                //    {
                //        WorkContext.PriborContexts.Add(item);
                //    }
                //}

                foreach (var item in window.list)
                {
                    bool IsYes = WorkContext.PriborContexts.Where(x => x.Equals(item)).Any();
                    if (!IsYes && item.PriborGroup.Count != 0)
                    {
                        WorkContext.PriborContexts.Add(item);
                    }
                    else
                    {
                        if (IsYes && item.PriborGroup.Count == 0)
                        {
                            item.PriborGroup.Delete();
                            WorkContext.PriborContexts.Remove(item);
                        }
                    }
                }
            }
        }

        private void OnClickOk(object sender, RoutedEventArgs e)
        {
            WorkContext.Work.WorkSectionId = WorkContext.SelectedSection.Id;
            DialogResult = true;
        }

        private void OnClickCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void OnClickContextMenuPasteMaterial(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsData(DataFormats.Text))
            {
                using (var db = new SmetaApplication.DbContexts.SmetaDbAppContext())
                {
                    string data = Clipboard.GetText();
                    string[] array = data.Split('\n');
                    //MessageBox.Show(array.Length.ToString());
                    foreach (var item in array)
                    {
                        string str = item.Replace("\n", "");
                        if (!WorkContext.MaterailContexts.Where(x => int.Parse(x.Material.Code) == int.Parse(str)).Any())
                        {
                            //MessageBox.Show(str);
                            str = int.Parse(str).ToString();
                            Material material = db.Materials.Where(x => int.Parse(x.Code) == int.Parse(str)).FirstOrDefault();
                            if (material != null)
                            {
                                WorkContext.MaterailContexts.Add(new MaterialContext(material));
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Ошибка формата");
            }
        }

        private void OnClickMenuPastePribor(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsData(DataFormats.Text))
            {
                using (var db = new SmetaApplication.DbContexts.SmetaDbAppContext())
                {
                    string data = Clipboard.GetText();
                    string[] array = Regex.Replace(data, @"\r\n?", ";").Split(';');
                    //MessageBox.Show(array.Length.ToString());
                    foreach (var item in array)
                    {
                        if (!WorkContext.PriborContexts.Where(x => int.Parse(x.Pribor.Code) == int.Parse(item)).Any())
                        {
                            //MessageBox.Show("=" + str + "=");
                            MessageBox.Show("=" + item + "=");
                            foreach (var t in db.Pribors)
                            {
                                MessageBox.Show(t.Code + "=" + int.Parse(item));
                            }
                            ////Pribor pribor = db.Pribors.Where(x => int.Parse(x.Code) == int.Parse(item)).First();
                            //if (pribor != null)
                            //{
                            //    WorkContext.PriborContexts.Add(new PriborContext(pribor));
                            //}
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Ошибка формата");
            }
        }

        private void MouseDoubleClick_SetEdit(object sender, MouseButtonEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.IsReadOnly = false;
            textBox.BorderThickness = new Thickness(1, 1, 1, 1);
        }

        private void MyCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (Clipboard.ContainsData(DataFormats.Text))
            {
                try
                {
                    // get all text
                    string data = Clipboard.GetText();
                    // split to lines
                    string[] array = data.Split('\n');
                    // First line is nomer norma
                    WorkContext.Work.Number = array[0];

                    // Second line is Name of process
                    WorkContext.Work.Name = array[1];
                    // Third line is measure of process without "Измеритель"
                    WorkContext.Work.Measure = array[2].Replace("Измеритель", "");
                    // 4, 5, 6, 7, 8, 9, 10 are useless's lines

                    List<PriborContext> priborContexts = new List<PriborContext>();

                    using (var db = new SmetaApplication.DbContexts.SmetaDbAppContext())
                    {
                        foreach (var item in db.Pribors)
                        {
                            priborContexts.Add(new PriborContext(item));
                        }

                        int i = 9;
                        bool IsMaterial = true;
                        for (; IsMaterial && i < array.Length; i++)
                        {
                            int code;
                            string[] split = array[i].Split('\t');
                            // first column is code of pribor, if it isn't pribor, then set IsMaterial = false
                            //MessageBox.Show(array[i]);
                            if (int.TryParse(split[0], out code))
                            {
                                PriborContext priborContext = priborContexts.Find(x => int.Parse(x.Pribor.Code) == code);

                                if (priborContext == null)
                                    throw new NullReferenceException("Прибор нет: " + split[0] + " : " + split[1]);

                                priborContext.PriborGroup.Count = 1;
                                WorkContext.PriborContexts.Add(priborContext);

                                // 2 column is name of pribor
                                // 3 column is measure of pribor
                                // begin from 4
                                if (i == 9)
                                {
                                    double time;
                                    bool IsBegin = true;
                                    if (double.TryParse(split[3].Replace('.', ','), out time))
                                    {
                                        WorkContext.Work.Time1 = time;
                                    }
                                    else
                                        IsBegin = false;

                                    if (IsBegin && double.TryParse(split[4].Replace('.', ','), out time))
                                    {
                                        WorkContext.Work.Time2 = time;
                                    }
                                    else
                                        IsBegin = false;

                                    if (IsBegin && double.TryParse(split[5].Replace('.', ','), out time))
                                    {
                                        WorkContext.Work.Time3 = time;
                                    }
                                    else
                                        IsBegin = false;

                                    if (IsBegin && double.TryParse(split[6].Replace('.', ','), out time))
                                    {
                                        WorkContext.Work.Time4 = time;
                                    }
                                    else
                                        IsBegin = false;

                                    if (IsBegin && double.TryParse(split[7].Replace('.', ','), out time))
                                    {
                                        WorkContext.Work.Time5 = time;
                                    }
                                    else
                                        IsBegin = false;
                                }
                            }
                            else
                            {
                                IsMaterial = false;
                            }
                        }

                        List<MaterialContext> materialContexts = new List<MaterialContext>();
                        foreach (var item in db.Materials)
                        {
                            materialContexts.Add(new MaterialContext(item));
                        }

                        // Materials
                        for (; i < array.Length; i++)
                        {
                            int code;
                            string[] split = array[i].Split('\t');
                            // first column is code of material, if it isn't material, then for does'nt work
                            //MessageBox.Show(array[i]);
                            if (int.TryParse(split[0], out code))
                            {
                                MaterialContext materialContext = materialContexts.Find(x => int.Parse(x.Material.Code) == code);
                                WorkContext.MaterailContexts.Add(materialContext);

                                if (materialContext == null)
                                    throw new NullReferenceException("Материал нет: " + split[0] + " : " + split[1]);

                                // 2 column is name of material
                                // 3 column is measure of material
                                // begin from 4

                                double time;
                                bool IsBegin = true;
                                //MessageBox.Show(split[3]);
                                if (double.TryParse(split[3].Replace('.', ','), out time))
                                {
                                    materialContext.MaterialGroup.Count1 = time;
                                }
                                else
                                    IsBegin = false;

                                if (IsBegin && double.TryParse(split[4].Replace('.', ','), out time))
                                {
                                    materialContext.MaterialGroup.Count2 = time;
                                }
                                else
                                    IsBegin = false;

                                if (IsBegin && double.TryParse(split[5].Replace('.', ','), out time))
                                {
                                    materialContext.MaterialGroup.Count3 = time;
                                }
                                else
                                    IsBegin = false;

                                if (IsBegin && double.TryParse(split[6].Replace('.', ','), out time))
                                {
                                    materialContext.MaterialGroup.Count4 = time;
                                }
                                else
                                    IsBegin = false;

                                if (IsBegin && double.TryParse(split[7].Replace('.', ','), out time))
                                {
                                    materialContext.MaterialGroup.Count5 = time;
                                }
                                else
                                    IsBegin = false;
                            }
                        }
                    }
                    //WorkContext.Work.PriceMaterial = WorkContext.MaterailContexts.Sum(x => x.Material.Price * x.MaterialGroup.Count1);
                    //WorkContext.Work.PricePribor = WorkContext.PriborContexts.Sum(x => x.Pribor.Price * x.PriborGroup.Count);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Ошибка формата: " + exc.ToString());
                }
            }
            else
            {
                MessageBox.Show("Ошибка формата");
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
