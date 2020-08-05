using Microsoft.Win32;
using SmetaApplication.DbContexts;
using SmetaApplication.Methods;
using SmetaApplication.Models.Material;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;

namespace SmetaApplication.Windows.Other
{
    /// <summary>
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        BackgroundWorker worker;
        public UpdateWindow()
        {
            InitializeComponent();

            worker = new BackgroundWorker();
            worker.DoWork += DoWok;
            worker.ProgressChanged += ProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerCompleted += RunWorkerCompleted;

            using (var db = new SmetaDbAppContext())
            {
                pbUpdateStatus.Maximum = db.Works.Count;
            }
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnUpdate.IsEnabled = true;
            MessageBox.Show("Готова");
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbUpdateStatus.Value = e.ProgressPercentage;
        }

        private void DoWok(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (var db = new SmetaDbAppContext())
                {
                    List<Pribor> pribors = db.Pribors;
                    List<Material> materials = db.Materials;
                    int i = 0;
                    db.Works.ForEach(work =>
                    {
                        // Pribors
                        double s = 0;
                        db.PriborGroups.Where(y => y.WorkId == work.Id).ToList().ForEach(group =>
                        {
                            Pribor pribor = pribors.Find(x => x.Id == group.PriborId);
                            s += ((pribor.Price * pribor.Percent / 100) * group.Count) / 12;
                        });
                        work.PricePribor = s;
                        s = 0;
                        db.MaterialGroups.Where(y => y.WorkId == work.Id).ToList().ForEach(group =>
                        {
                            Material material = materials.Find(x => x.Id == group.MaterialId);
                            s += material.Price;
                        });
                        work.PriceMaterial = s;
                        work.Update();
                        i++;
                        worker.ReportProgress(i);
                    });
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void btnSaveTemplateMaterial(object sender, RoutedEventArgs e)
        {
            try
            {
                Excel.Application excel;
                Excel.Workbook wb;
                Excel._Worksheet ws;
                Excel.Range range;

                string file = System.IO.Directory.GetCurrentDirectory() + @"\Templates\material.xlsx";

                excel = new Excel.Application();
                wb = excel.Workbooks.Open(file);
                ws = excel.Sheets[1] as Excel.Worksheet;
                try
                {
                    int row = 1;
                    int first_row = row + 1;

                    using (var db = new SmetaDbAppContext())
                    {
                        foreach (var item in db.Materials)
                        {
                            row++;
                            // styles for cell code
                            range = ws.get_Range("A" + row, "A" + row);
                            range.NumberFormat = "@";

                            ws.Cells[row, 1] = Helper.FillWithZero(item.Code);
                            ws.Cells[row, 2] = item.Name;
                            ws.Cells[row, 3] = item.Price;
                            ws.Cells[row, 4] = item.Dimension;
                        }
                    }
                    
                    #region styling
                    range = ws.get_Range("A" + first_row, "D" + row);
                    range.WrapText = true;
                    range.Font.Name = "Times New Roman";
                    range.Font.Size = 14;
                    range.Borders.Weight = 2;

                    range = ws.get_Range("C" + first_row, "C" + row);
                    range.NumberFormat = "#,##0";
                    #endregion

                    #region saving
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "XLSX files (*.xlsx, *.xlsm, *.xltx, *.xltm)|*.xlsx;*.xlsm;*.xltx;*.xltm|XLS files (*.xls, *.xlt)|*.xls;*.xlt";
                    if (dialog.ShowDialog() == true)
                    {
                        wb.SaveAs(dialog.FileName);
                        excel.Visible = true;
                    }
                    else
                    {
                        wb.Close();
                        excel.Quit();
                        wb.Close();
                    }
                    #endregion
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void btnSaveTemplatePribor(object sender, RoutedEventArgs e)
        {
            try
            {
                Excel.Application excel;
                Excel.Workbook wb;
                Excel._Worksheet ws;
                Excel.Range range;

                string file = System.IO.Directory.GetCurrentDirectory() + @"\Templates\pribor.xlsx";

                excel = new Excel.Application();
                wb = excel.Workbooks.Open(file);
                ws = excel.Sheets[1] as Excel.Worksheet;
                try
                {
                    int row = 1;
                    int first_row = row + 1;

                    using (var db = new SmetaDbAppContext())
                    {
                        foreach (var item in db.Pribors)
                        {
                            row++;
                            // styles for cell code
                            range = ws.get_Range("A" + row, "A" + row);
                            range.NumberFormat = "@";

                            ws.Cells[row, 1] = Helper.FillWithZero(item.Code);
                            ws.Cells[row, 2] = item.Name;
                            ws.Cells[row, 3] = item.Price;
                            ws.Cells[row, 4] = item.Dimension;
                            ws.Cells[row, 5] = item.Percent;
                        }
                    }

                    #region styling
                    range = ws.get_Range("A" + first_row, "E" + row);
                    range.WrapText = true;
                    range.Font.Name = "Times New Roman";
                    range.Font.Size = 14;
                    range.Borders.Weight = 2;

                    range = ws.get_Range("C" + first_row, "C" + row);
                    range.NumberFormat = "#,##0";
                    #endregion

                    #region saving
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "XLSX files (*.xlsx, *.xlsm, *.xltx, *.xltm)|*.xlsx;*.xlsm;*.xltx;*.xltm|XLS files (*.xls, *.xlt)|*.xls;*.xlt";
                    if (dialog.ShowDialog() == true)
                    {
                        wb.SaveAs(dialog.FileName);
                        excel.Visible = true;
                    }
                    else
                    {
                        wb.Close();
                        excel.Quit();
                        wb.Close();
                    }
                    #endregion
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void btnUpdatePribor(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "XLSX files (*.xlsx, *.xlsm, *.xltx, *.xltm)|*.xlsx;*.xlsm;*.xltx;*.xltm|XLS files (*.xls, *.xlt)|*.xls;*.xlt";
            open.Title = "Obyektni ochish";
            if (open.ShowDialog() == true)
            {

                try
                {
                    
                    Excel.Application exApp = new Excel.Application();
                    Excel.Workbook workbook = exApp.Workbooks.Open(open.FileName, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true);
                    // 1-Listni olish
                    Excel.Worksheet ws = workbook.Worksheets.get_Item(1) as Excel.Worksheet;
                    int i = 0;

                    string[] ss = new string[5];
                   
                    int row = ws.UsedRange.Rows.Count;
                    for (i = 2; i <= row; i++)
                    {
                        ss[0] = GetString(ws.Cells[i, 1]);
                        ss[1] = GetString(ws.Cells[i, 2]);
                        ss[2] = GetString(ws.Cells[i, 3]);
                        ss[3] = GetString(ws.Cells[i, 4]);
                        ss[4] = GetString(ws.Cells[i, 5]);

                        Pribor pribor = new Pribor()
                        {
                            Code = Helper.ClearZeros(ss[0]),
                            Name = ss[1],
                            Price = double.Parse(ss[2]),
                            Dimension = ss[3],
                            Percent = double.Parse(ss[4])
                        };

                        using (var db = new SmetaDbAppContext())
                        {
                            Pribor find = db.Pribors.Where(x => int.Parse(x.Code) == int.Parse(pribor.Code)).FirstOrDefault();
                            if (find != null)
                            {
                                find.Name = pribor.Name;
                                find.Price = pribor.Price;
                                find.Dimension = pribor.Dimension;
                                find.Percent = pribor.Percent;

                                find.Update();
                            }
                        }
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message + "\n Ошибка");
                }
            }
        }

        private string GetString(object v)
        {
            Excel.Range range = v as Excel.Range;
            if (range.Value == null)
                return "";
            else
                return range.Value.ToString();
        }

        private void btnUpdateMaterial(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "XLSX files (*.xlsx, *.xlsm, *.xltx, *.xltm)|*.xlsx;*.xlsm;*.xltx;*.xltm|XLS files (*.xls, *.xlt)|*.xls;*.xlt";
            open.Title = "Obyektni ochish";
            if (open.ShowDialog() == true)
            {

                try
                {

                    Excel.Application exApp = new Excel.Application();
                    Excel.Workbook workbook = exApp.Workbooks.Open(open.FileName, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true);
                    // 1-Listni olish
                    Excel.Worksheet ws = workbook.Worksheets.get_Item(1) as Excel.Worksheet;
                    int i = 0;

                    string[] ss = new string[5];

                    int row = ws.UsedRange.Rows.Count;
                    for (i = 2; i <= row; i++)
                    {
                        ss[0] = GetString(ws.Cells[i, 1]);
                        ss[1] = GetString(ws.Cells[i, 2]);
                        ss[2] = GetString(ws.Cells[i, 3]);
                        ss[3] = GetString(ws.Cells[i, 4]);

                        Material material = new Material()
                        {
                            Code = Helper.ClearZeros(ss[0]),
                            Name = ss[1],
                            Price = double.Parse(ss[2]),
                            Dimension = ss[3],
                        };

                        using (var db = new SmetaDbAppContext())
                        {
                            Material find = db.Materials.Find(x => int.Parse(x.Code) == int.Parse(material.Code));
                            if (find != null)
                            {
                                find.Name = material.Name;
                                find.Price = material.Price;
                                find.Dimension = material.Dimension;

                                find.Update();
                            }
                        }
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message + "\n Ошибка");
                }
            }
        }

        private void btnBeginUpdate(object sender, RoutedEventArgs e)
        {
            btnUpdate.IsEnabled = false;
            worker.RunWorkerAsync();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (worker.IsBusy)
            {
                e.Cancel = true;
                MessageBox.Show("Невозможно закрыть");
            }
        }
    }
}
