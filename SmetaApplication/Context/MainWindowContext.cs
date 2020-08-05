using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using SmetaApplication.ViewModels;
using SmetaApplication.Methods;
using System.Windows;
using SmetaApplication.Models.Amount;
using SmetaApplication.Models.Team;
using SmetaApplication.Constants;
using Excel = Microsoft.Office.Interop.Excel;
using SmetaApplication.Models.Commentary;
using SmetaApplication.Models.Material;
using SmetaApplication.DbContexts;
using SmetaApplication.Models.GroupMaterial;
using SmetaApplication.Commands;
using System.Windows.Data;
using SmetaApplication.Models.WorkModels;
using SmetaApplication.Component;
using Microsoft.Win32;
using SmetaApplication.Windows.Report;
using System.IO;
using static Smeta.Methods.ReadXml;
using Smeta.Models;
using SmetaApplication.Models.Contract;
using SmetaApplication.Windows.List;
using SmetaApplication.Filtrs;
using SmetaApplication.Windows.Adds;

namespace SmetaApplication.Context
{
    public class MainWindowContext : INotifyPropertyChanged
    {
        public Contract contract = null;

        public WorkFilter Filter;

        private List<WorkDemView> EnteredSize;

        List<WorkSection> workSections;

        public WorkDemView CurrentWork { get; set; }
        public WorkDemView CurrentWorkKam { get; set; }
        public WorkDemView CurrentWorkLab { get; set; }

        public ObservableCollection<WorkDemView> Poliviy { get; set; }
        public ObservableCollection<WorkDemView> Laboratorniy { get; set; }
        public ObservableCollection<WorkDemView> Kameralniy { get; set; }

        public Visibility MainPlace {
            get
            {
                return contract == null ? Visibility.Collapsed : Visibility.Visible;       
            }
        }

        #region Prices
        private double? pricePoliviy;
        public double? PricePoliviy
        {
            get
            {
                pricePoliviy = Poliviy.Sum(x => x.AllCost);
                return pricePoliviy;
            }
        }

        private double? pricePolPay;
        public double? PricePolPay
        {
            get
            {
                pricePolPay = Poliviy.Sum(x => x.PricePayAllView);
                return pricePolPay;
            }
        }

        // Районный коэфф
        private double? pricePolPay15;
        public double? PricePolPay15
        {
            get
            {
                pricePolPay15 =  Math.Round((double)pricePolPay * Helper.Rayoniy, 2);
                return pricePolPay15;
            }
        }

        private double? pricePolMaterial;
        public double? PricePolMaterial
        {
            get
            {
                pricePolMaterial = Poliviy.Sum(x => x.PriceMaterialForSize);
                return pricePolMaterial;
            }
        }

        private double? pricePolPribor;
        public double? PricePolPribor
        {
            get
            {
                pricePolPribor = Poliviy.Sum(x => x.PricePriborForSize);
                return pricePolPribor;
            }
        }

        private double? pricePolAll;
        public double? PricePolAll
        {
            get
            {
                pricePolAll = pricePolPay15 + pricePolMaterial + PricePolPribor;
                return pricePolAll;
            }
        }

        private double? priceLabortorniy;
        public double? PriceLabortorniy
        {
            get
            {
                priceLabortorniy = Laboratorniy.Sum(x => x.AllCost);
                return priceLabortorniy;
            }
        }

        private double? priceKameralniy;
        public double? PriceKameralniy
        {
            get
            {
                priceKameralniy = Kameralniy.Sum(x => x.AllCost);
                return priceKameralniy;
            }
        }

        private double? priceAll;
        public double? PriceAll
        {
            get
            {
                priceAll = PricePolAll + priceLabortorniy + priceKameralniy;
                return priceAll;
            }
        }

        #endregion

        #region Commands
        public RelayCommand ShowProperties { get; }
        public RelayCommand ShowPropertiesKam { get; }
        public RelayCommand ShowPropertiesLab { get; }

        public RelayCommand PrintCommand { get; }
        public RelayCommand CopyCommand { get; }

        public RelayCommand AddWorkCommand { get; }
        public RelayCommand NewContractCommand { get; }
        public RelayCommand OpenContractsCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand HomeCommand { get; }
        
        #endregion

        public ReportContext ReportContext;

        #region functions of commands
        private void CopyCommandExcute(object sender)
        {
            //ReportSettings report = new ReportSettings(ReportContext);
            //report.ShowDialog();
            try
            {
                //StreamReader reader = new StreamReader(@"D:\import.txt");
                //string str;
                //while ((str = reader.ReadLine()) != null)
                //{
                //    int Id = int.Parse(str);
                //    using (var db = new SmetaDbAppContext())
                //    {
                //        AddWorkToCount(db.Works.Where(x => x.Id == Id).FirstOrDefault());
                //    }
                //}


                //List<int> ids = new List<int>()
                //{
                //    1, 2, 3, 4, 6, 8
                //};


                //List<Smeta.Models.Ish> list = SelectIsh(null, null);
                //List<SostavBrigada> sostavs = SelectSostavBrigada(null, null);
                //List<IshTuri> ishes = SelectIshTuri(null, null);
                //List<Post> posts = (new SmetaDbAppContext()).Posts;

                //ishes = ishes.Where(x => ids.Contains((int)x.Id)).ToList();

                //foreach (var item in ishes)
                //{
                //    //MessageBox.Show(list.Where(x => x.IshTuriId == item.Id).Count().ToString());
                //    List<Ish> helperish = list.Where(x => x.IshTuriId == item.Id).ToList();
                //    for (int i = 0; i < helperish.Count; i++)
                //    {
                //        bool t = true;
                //        for (int j = 0; j < i; j++)
                //        {
                //            if (helperish[j].NomerNorma == helperish[i].NomerNorma)
                //            {
                //                t = false;
                //                break;
                //            }
                //        }
                //        if (t)
                //        {
                //            List<SostavBrigada> brigadas = sostavs.Where(x => x.IshId == helperish[i].Id).ToList();
                //            Work work = new Work()
                //            {
                //                WorkSectionId = 1,
                //                Name = helperish[i].Nom,
                //                Number = helperish[i].NomerNorma,
                //                Measure = helperish[i].OlchovBirligi,
                //                PriceMaterial = 0,
                //                PricePribor = 0,
                //                Time1 = helperish[i].IshVaqti
                //            };
                //            double price = 0;
                //            foreach (var br in brigadas)
                //            {
                //                Post post = posts.Where(x => x.Id == br.LavozimId).SingleOrDefault();
                //                price += post.Pay / 168;
                //            }
                //            work.PricePay = price;

                //            //DBConnection.BeginTransaction();

                //            work.Save();
                //            //MessageBox.Show("Keldi...");

                //            brigadas.ForEach(x =>
                //            {
                //                Post post = posts.Where(y => y.Id == x.LavozimId).SingleOrDefault();
                //                WorkTeam team = new WorkTeam()
                //                {
                //                    Count = (int)x.Son,
                //                    Koef = 1,
                //                    WorkDemId = work.Id,
                //                    PostId = post.Id
                //                };
                //                team.Save();
                //            });
                //        }
                //    }
                //}

                //List<Table> tables = new List<Table>();
                //Excel.Worksheet ws = (new Excel.Application()).Workbooks.Open(@"D:\Разные геодезические работы.xlsx", (object)0, (object)true, (object)5, (object)"", (object)"", (object)true, (object)Excel.XlPlatform.xlWindows, (object)"\t", (object)false, (object)false, (object)0, (object)true, System.Type.Missing, System.Type.Missing).Worksheets.get_Item((object)1) as Excel.Worksheet;



                //for (int i = 2; i <= ws.UsedRange.Rows.Count; i++)
                //{
                //    Table table = new Table();

                //    int n;

                //    //MessageBox.Show(i.ToString());

                //    string str;

                //    if ((ws.Cells[i, 1] as Excel.Range).get_Value(System.Type.Missing) != null &&
                //            (str = (ws.Cells[i, 1] as Excel.Range).get_Value(System.Type.Missing).ToString()) != null
                //        && int.TryParse(str, out n))
                //    {
                //        table.N = n;
                //    }
                //    else
                //    {
                //        table.Name = null;
                //    }

                //    table.Name = (ws.Cells[i, 2] as Excel.Range).get_Value(System.Type.Missing).ToString();

                //    if ((ws.Cells[i, 3] as Excel.Range).get_Value(System.Type.Missing) != null &&
                //            (str = (ws.Cells[i, 3] as Excel.Range).get_Value(System.Type.Missing).ToString()) != null
                //        && int.TryParse(str, out n))
                //    {
                //        table.Type = n;
                //    }
                //    else
                //    {
                //        table.Type = null;
                //    }

                //    double d;

                //    if ((ws.Cells[i, 4] as Excel.Range).get_Value(System.Type.Missing) != null &&
                //        (str = (ws.Cells[i, 4] as Excel.Range).get_Value(System.Type.Missing).ToString()) != null
                //        && double.TryParse(str, out d))
                //    {
                //        table.Koef = d;
                //    }
                //    else
                //    {
                //        table.Koef = 0;
                //    }

                //    if ((ws.Cells[i, 5] as Excel.Range).get_Value(System.Type.Missing) != null &&
                //        (str = (ws.Cells[i, 5] as Excel.Range).get_Value(System.Type.Missing).ToString()) != null)
                //    {
                //        table.Content = str;
                //    }
                //    else
                //    {
                //        table.Content = null;
                //    }

                //    tables.Add(table);
                //}

                //for (int i = 0; i < tables.Count - 1; i++)
                //{
                //    Table begin = tables[i];
                //    WorkSection section = new WorkSection()
                //    {
                //        Name = tables[i].Name,
                //        Content = tables[i].Content,
                //        WorkTypeId = 8,
                //        Place = (int)tables[i].Type
                //    };

                //    //List<Commentary> commentaries = new List<Commentary>();
                //    //// Doimiy koeflar
                //    //commentaries.Add(new Commentary()
                //    //{
                //    //    Koef = 1.1,
                //    //    Name = "При производстве работ на полотне существующих " +
                //    //    "железных дорог и при съемке главных и приемо-отправочных путей, " +
                //    //    "горловин и основных парков сортировки железнодорожных станций к ЗТС " +
                //    //    "применяются коэффициенты при движении поездов в сутки: от 12 до 24 пар 1,1"
                //    //});
                //    //commentaries.Add(new Commentary()
                //    //{
                //    //    Koef = 1.1,
                //    //    Name = "При производстве работ на полотне существующих " +
                //    //    "железных дорог и при съемке главных и приемо-отправочных путей, " +
                //    //    "горловин и основных парков сортировки железнодорожных станций к ЗТС " +
                //    //    "применяются коэффициенты при движении поездов в сутки: от 25 до 48 пар 1,2"
                //    //});
                //    //commentaries.Add(new Commentary()
                //    //{
                //    //    Koef = 1.1,
                //    //    Name = "При производстве работ на полотне существующих " +
                //    //    "железных дорог и при съемке главных и приемо-отправочных путей, " +
                //    //    "горловин и основных парков сортировки железнодорожных станций к ЗТС " +
                //    //    "применяются коэффициенты при движении поездов в сутки: от 49 до 72 пар 1,3"
                //    //});
                //    //commentaries.Add(new Commentary()
                //    //{
                //    //    Koef = 1.1,
                //    //    Name = "При интенсивном движении транспорта и механизмов к Н.вр. применяются коэффициенты: " +
                //    //    "при числе маневровых подач в сутки от 11 до 20 или движении 21-40 механизмов в 1ч – 1,1"
                //    //});
                //    //commentaries.Add(new Commentary()
                //    //{
                //    //    Koef = 1.2,
                //    //    Name = "При интенсивном движении транспорта и механизмов к Н.вр. применяются коэффициенты: " +
                //    //    "при числе маневровых подач в сутки от 21 до 30 или движении 41 – 60 механизмов в 1 ч – 1,2"
                //    //});
                //    //commentaries.Add(new Commentary()
                //    //{
                //    //    Koef = 1.3,
                //    //    Name = "При интенсивном движении транспорта и механизмов к Н.вр. применяются коэффициенты: " +
                //    //    "при числе маневровых подач более 30 или движении механизмов в 1 ч более 60 – 1,3"
                //    //});
                //    while (tables[i + 1].N == null)
                //    {
                //        i++;
                //        Commentary commentary = new Commentary()
                //        {
                //            Koef = tables[i].Koef,
                //            Name = tables[i].Name,
                //        };
                //    }
                //    section.Save();
                //    //commentaries.ForEach(x =>
                //    //{
                //    //    x.WorkSectionId = section.Id;
                //    //    x.Save();
                //    //});

                //    using (var db = new SmetaDbAppContext())
                //    {
                //        //MessageBox.Show(begin.Koef.ToString() + "\n" + tables[i + 1].Koef.ToString());
                //        db.Works.Where(x => (int)begin.Koef + 216 <= int.Parse(x.Number)
                //        && int.Parse(x.Number) < (int)tables[i + 1].Koef + 216)
                //            .ToList().ForEach(x =>
                //            {
                //                x.WorkSectionId = section.Id;
                //                x.IsUpdated = true;
                //                x.Update();
                //            });
                //    }
                //}
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private bool CanCopyCommand(object sender)
        {
            return true;
        }

        private void ShowCurrentWorkProperties(object sender)
        {
            ShowCurrentWorkProperties(CurrentWork);
        }

        private  bool CanShowCurrentWorkProperties(object sender)
        {
            return CurrentWork != null;
        }

        private void ShowCurrentWorkProperties(WorkDemView work)
        {
            Window window = new Window();
            work.FillOrClearTeamWorkers(true);
            window.Content = new CustomResultItem(work);
            window.ShowDialog();
            work.FillOrClearTeamWorkers(false);
            work.ChangedKoef();
        }

        private bool CanShowCurrentWorkPropertiesLab(object arg)
        {
            return CurrentWorkLab != null;
        }

        private void ShowCurrentWorkPropertiesLab(object obj)
        {
            ShowCurrentWorkProperties(CurrentWorkLab);
        }

        private bool CanShowCurrentWorkPropertiesKam(object arg)
        {
            return CurrentWorkKam != null;
        }

        private void ShowCurrentWorkPropertiesKam(object obj)
        {
            ShowCurrentWorkProperties(CurrentWorkKam);
        }

        private bool CanPrint(object arg)
        {
            return EnteredSize.Count > 0;
        }

        private bool CanAddWorkCommandExecute(object arg)
        {
            return contract != null;
        }

        private void AddWorkCommandExecute(object obj)
        {
            WindowWorkList window = new WindowWorkList(Filter, true);
            window.AddToCount += AddToCount;
            window.Show();
        }

        private void AddToCount(object sender, EventArgs e)
        {
            AddWorkToCount(sender);
        }

        private bool CanNewContractCommandExecute(object arg)
        {
            return true;
        }

        private void NewContractCommandExecute(object obj)
        {
            try
            {
                Contract contract = new Contract();
                WindowAddContract window = new WindowAddContract(contract);
                if (window.ShowDialog() == true)
                {
                    contract.Save();
                    this.contract = contract;
                    ContractChanged();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private bool CanOpenContractsCommandExecute(object arg)
        {
            return true;
        }

        private void OpenContractsCommandExecute(object obj)
        {
            try
            {
                WindowContractList window = new WindowContractList();
                if (window.ShowDialog() == true)
                {
                    Poliviy.Clear();
                    Laboratorniy.Clear();
                    Kameralniy.Clear();
                    EnteredSize.Clear();

                    contract = window.Selected;
                    using (var db = new SmetaDbAppContext())
                    {
                        db.WorkDatas.Where(x => x.ContractId == contract.Id).ToList().ForEach(x =>
                        {
                            Work work = db.Works.Find(y => y.Id == x.WorkId);
                            AddWorkToCount(work, x.Size, x.WorkDem, x.CommentIds);
                        });
                    }
                    ContractChanged();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private bool CanSaveCommandExecute(object arg)
        {
            return contract != null && EnteredSize.Count > 0;
        }

        private void SaveCommandExecute(object obj)
        {
            try
            {
                DBConnection.SqlQuery("Delete From WorkData Where ContractId = " + contract.Id);
                EnteredSize.ForEach(x =>
                {
                    WorkData workData = new WorkData(x, contract.Id);
                    workData.Save();
                });
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private bool CanHomeCommandExecute(object arg)
        {
            return contract != null;
        }

        private void HomeCommandExecute(object obj)
        {
            contract = null;
            ContractChanged();
        }

        private void ContractChanged()
        {
            OnPropertyChanged("AddWorkCommand");
            OnPropertyChanged("MainPlace");
            OnPropertyChanged("SaveCommand");
            OnPropertyChanged("HomeCommand");
        }
        #endregion

        public MainWindowContext()
        {
            Filter = new WorkFilter();

            Poliviy = new ObservableCollection<WorkDemView>();
            Laboratorniy = new ObservableCollection<WorkDemView>();
            Kameralniy = new ObservableCollection<WorkDemView>();
            EnteredSize = new List<WorkDemView>();
            
            using (var db = new SmetaDbAppContext())
            {
                workSections = db.WorkSections.ToList();
            }

            LoadData();

            ShowProperties = new RelayCommand(ShowCurrentWorkProperties, CanShowCurrentWorkProperties);
            ShowPropertiesKam = new RelayCommand(ShowCurrentWorkPropertiesKam, CanShowCurrentWorkPropertiesKam);
            ShowPropertiesLab = new RelayCommand(ShowCurrentWorkPropertiesLab, CanShowCurrentWorkPropertiesLab);
            CopyCommand = new RelayCommand(CopyCommandExcute, CanCopyCommand);
            PrintCommand = new RelayCommand(Print, CanPrint);
            AddWorkCommand = new RelayCommand(AddWorkCommandExecute, CanAddWorkCommandExecute);
            NewContractCommand = new RelayCommand(NewContractCommandExecute, CanNewContractCommandExecute);
            OpenContractsCommand = new RelayCommand(OpenContractsCommandExecute, CanOpenContractsCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute, CanSaveCommandExecute);
            HomeCommand = new RelayCommand(HomeCommandExecute, CanHomeCommandExecute);

            ReportContext = new ReportContext();
        }

        #region Loads data
        private void LoadData()
        {
            BackgroundWorker BackgroundWorker;
            BackgroundWorker = new BackgroundWorker();
            BackgroundWorker.DoWork += DoWok;
            BackgroundWorker.ProgressChanged += ProgressChanged;
            BackgroundWorker.WorkerReportsProgress = true;
            BackgroundWorker.RunWorkerCompleted += RunWorkerCompleted;
            //BackgroundWorker.RunWorkerAsync();
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        private void DoWok(object sender, DoWorkEventArgs e)
        {
            try
            {
                int n = 1;
                using (var db = new SmetaDbAppContext())
                {
                    //var db = new SmetaDbAppContext();
                    db.Works.OrderBy(x => int.Parse(x.Number)).ToList().ForEach(x =>
                    { 
                        if (db.PriborGroups.Any(z => z.WorkId == x.Id))
                        {
                            int i = 0;
                            //if (n < 10)
                                Helper.WorkCount(x).ForEach(y =>
                                {
                                    if (y)
                                    {
                                        (sender as BackgroundWorker).ReportProgress(1, new WorkDemView(x, i) { N = n });
                                        i++;
                                        n++;
                                    }
                                });
                            //else return;
                        }
                    });
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        public void AddWorkToCount(Work work, double? size = null, int diff = -1, string comment = null)
        {
            //if (work == null)
            //    MessageBox.Show("Error");
            int i = 0;
            //MessageBox.Show(Helper.WorkCount(work).Count.ToString());
            Helper.WorkCount(work).ForEach(y =>
            {
                if (y)
                {
                    WorkDemView demView = new WorkDemView(work, i);
                    demView.EnterSize += EnterSizeChanged;
                    int place = (workSections.Where(x => x.Id == demView.WorkSectionId).FirstOrDefault()).Place;
                    switch (place)
                    {
                        case 0:
                            demView.N = Poliviy.Count + 1;
                            Poliviy.Add(demView);
                            break;
                        case 1:
                            demView.N = Kameralniy.Count + 1;
                            Kameralniy.Add(demView);
                            break;
                        case 2:
                            demView.N = Laboratorniy.Count + 1;
                            Laboratorniy.Add(demView);
                            break;
                    }
                    if (size != null && diff == i)
                    {
                        demView.Size = size;
                        if (!string.IsNullOrEmpty(comment))
                        {
                            foreach (var item in demView.Commentaries)
                            {
                                string[] array = comment.Split(',');
                                if (array.Contains(item.Commentary.Id.ToString()))
                                {
                                    item.IsYes = true;
                                }
                            }
                        }
                    }
                    i++;
                }
            });
        }

        public void AddWorkToCount(object sender)
        {
            Work work = sender as Work;
            AddWorkToCount(work);
        }

        public void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            WorkDemView demView = (WorkDemView)e.UserState;
            demView.EnterSize += EnterSizeChanged;
            switch ((workSections.Where(x => x.Id == demView.WorkSectionId).FirstOrDefault()).Place)
            {
                case 0 : Poliviy.Add(demView); break;
                case 1 : Kameralniy.Add(demView); break;
                case 2 : Laboratorniy.Add(demView); break;
            }
        }

        #endregion

        private void EnterSizeChanged(object sender, EventArgs e)
        {
            WorkDemView WorkDemView = (WorkDemView)sender;
            if (WorkDemView.Size != null)
            {
                if (!EnteredSize.Exists(x => x == WorkDemView))
                    EnteredSize.Add(WorkDemView);
            }
            else
                EnteredSize.Remove(WorkDemView);

            OnPropertyChanged("PricePolPay");
            OnPropertyChanged("PricePolPay15");
            OnPropertyChanged("PricePolMaterial");
            OnPropertyChanged("PricePolPribor");
            OnPropertyChanged("PricePolAll");
            OnPropertyChanged("PriceLabortorniy");
            OnPropertyChanged("PriceKameralniy");
            OnPropertyChanged("PriceAll");
        }

        public void Print(object sender)
        {
            try
            {
                ReportSettings report = new ReportSettings(ReportContext);
                if (report.ShowDialog() == false)
                {
                    return;
                }

                Excel.Application excel;
                Excel.Workbook wb;
                Excel._Worksheet ws;
                Excel.Range range;

                string file = Directory.GetCurrentDirectory() + @"\Templates\report.xlsx";

                excel = new Excel.Application();
                wb = excel.Workbooks.Open(file);
                ws = excel.Sheets[1] as Excel.Worksheet;

                ws.Cells[3, 3] = contract.Name;

                try
                {
                    //if (true)
                    //    return;
                    List<Post> Posts;
                    List<Pribor> pribors;
                    List<Material> materials;
                    List<WorkSection> workSections;
                    using (var db = new SmetaDbAppContext())
                    {
                        Posts = db.Posts;
                        pribors = db.Pribors;
                        materials = db.Materials;
                        workSections = db.WorkSections;
                    }

                    int row = 10;
                    int first_row = row;
                    int begin_row;
                    int number = 0;


                    ///sum all price

                    // All pay price
                    double? pricePolPay = 0;
                    double? priceLabPay = 0;
                    double? priceKamPay = 0;

                    // All pribors price
                    double? pricePolPribor = 0;
                    double? priceLabPribor = 0;
                    double? priceKamPribor = 0;

                    // All material price
                    double? pricePolMaterial = 0;
                    double? priceLabMaterial = 0;
                    double? priceKamMaterial = 0;

                    // Thera are six parts of templete
                    if (EnteredSize.Count > 0)
                    {

                        #region Имя работы
                        begin_row = ++row;
                        #region Polivie raboti
                        List<WorkDemView> pol = EnteredSize.Where(x =>
                            workSections.Where(y => x.WorkSectionId == y.Id).First().Place == 0).ToList();

                        if (pol != null && pol.Count > 0)
                        {
                            ws.Cells[row, ReportColumnConstants.Name] = "Полевые работы";
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Merge(Type.Missing);
                            range.Font.Bold = true;
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            // report first part of data
                            foreach (var item in pol)
                            {
                                number++;
                                List<WorkTeam> teams = (new SmetaDbAppContext()).WorkTeams.
                                    ToList().Where(x => x.WorkDemId == item.Id).ToList();

                                int countOfWorkers = teams.Select(x => x.Count).Sum();
                                //MessageBox.Show(countOfWorkers.ToString());
                                int current_row = ++row;

                                ws.Cells[row, ReportColumnConstants.Number] = number;
                                ws.Cells[row, ReportColumnConstants.Name] = item.Name;
                                ws.Cells[row, ReportColumnConstants.Diffucult] = item.Diff;
                                ws.Cells[row, ReportColumnConstants.NumberStandart] = item.Number + (string.IsNullOrEmpty(item.Number2) ? "" : "(" + item.Number2 + ")");
                                ws.Cells[row, ReportColumnConstants.Time] = item.Time;
                                ws.Cells[row, ReportColumnConstants.Measure] = item.Measure;
                                ws.Cells[row, ReportColumnConstants.Size] = item.Size;
                                ws.Cells[row, ReportColumnConstants.TimeForWorker] = countOfWorkers * item.Time;
                                ws.Cells[row, ReportColumnConstants.AllTimeForWorker] = countOfWorkers * item.Size * item.Time;
                            }
                        }
                        #endregion

                        #region Laboratornie raboti
                        List<WorkDemView> lob = EnteredSize.Where(x =>
                            workSections.Where(y => x.WorkSectionId == y.Id).First().Place == 2).ToList();

                        if (lob != null && lob.Count > 0)
                        {
                            ws.Cells[++row, ReportColumnConstants.Name] = "Лабораторные работы";
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Merge(Type.Missing);
                            range.Font.Bold = true;
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            // report first part of data
                            foreach (var item in lob)
                            {
                                number++;
                                List<WorkTeam> teams = (new SmetaDbAppContext()).WorkTeams.
                                    ToList().Where(x => x.WorkDemId == item.Id).ToList();

                                int countOfWorkers = teams.Select(x => x.Count).Sum();
                                //MessageBox.Show(countOfWorkers.ToString());
                                int current_row = ++row;

                                ws.Cells[row, ReportColumnConstants.Number] = number;
                                ws.Cells[row, ReportColumnConstants.Name] = item.Name;
                                ws.Cells[row, ReportColumnConstants.Diffucult] = item.Diff;
                                ws.Cells[row, ReportColumnConstants.NumberStandart] = item.Number + (string.IsNullOrEmpty(item.Number2) ? "" : "(" + item.Number2 + ")");
                                ws.Cells[row, ReportColumnConstants.Time] = item.Time;
                                ws.Cells[row, ReportColumnConstants.Measure] = item.Measure;
                                ws.Cells[row, ReportColumnConstants.Size] = item.Size;
                                ws.Cells[row, ReportColumnConstants.TimeForWorker] = countOfWorkers * item.Time;
                                ws.Cells[row, ReportColumnConstants.AllTimeForWorker] = countOfWorkers * item.Size * item.Time;
                            }
                        }
                        #endregion

                        #region Kameralnie raboti
                        List<WorkDemView> kam = EnteredSize.Where(x =>
                            workSections.Where(y => x.WorkSectionId == y.Id).First().Place == 1).ToList();

                        if (kam != null && kam.Count > 0)
                        {
                            ws.Cells[++row, ReportColumnConstants.Name] = "Камеральные работы";
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Merge(Type.Missing);
                            range.Font.Bold = true;
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            // report first part of data
                            foreach (var item in kam)
                            {
                                number++;
                                List<WorkTeam> teams = (new SmetaDbAppContext()).WorkTeams.
                                    ToList().Where(x => x.WorkDemId == item.Id).ToList();

                                int countOfWorkers = teams.Select(x => x.Count).Sum();
                                //MessageBox.Show(countOfWorkers.ToString());
                                int current_row = ++row;

                                ws.Cells[row, ReportColumnConstants.Number] = number;
                                ws.Cells[row, ReportColumnConstants.Name] = item.Name;
                                ws.Cells[row, ReportColumnConstants.Diffucult] = item.Diff;
                                ws.Cells[row, ReportColumnConstants.NumberStandart] = item.Number + (string.IsNullOrEmpty(item.Number2) ? "" : "(" + item.Number2 + ")");
                                ws.Cells[row, ReportColumnConstants.Time] = item.Time;
                                ws.Cells[row, ReportColumnConstants.Measure] = item.Measure;
                                ws.Cells[row, ReportColumnConstants.Size] = item.Size;
                                ws.Cells[row, ReportColumnConstants.TimeForWorker] = countOfWorkers * item.Time;
                                ws.Cells[row, ReportColumnConstants.AllTimeForWorker] = countOfWorkers * item.Size * item.Time;
                            }
                        }
                        #endregion

                        #region styles
                        //range = ws.get_Range("C" + begin_row, "C" + row);
                        //range.NumberFormat = "#,##0";
                        //range = ws.get_Range("D" + begin_row, "D" + row);
                        //range.NumberFormat = "#,##0";
                        range = ws.get_Range("E" + begin_row, "E" + row);
                        range.NumberFormat = "";
                        //range = ws.get_Range("F" + begin_row, "F" + row);
                        //range.NumberFormat = "#,###0";
                        range = ws.get_Range("G" + begin_row, "G" + row);
                        range.NumberFormat = "";
                        //range = ws.get_Range("H" + begin_row, "H" + row);
                        //range.NumberFormat = "#,##0";
                        //range = ws.get_Range("I" + begin_row, "I" + row);
                        //range.NumberFormat = "#,##0";
                        #endregion

                        #endregion

                        // report second part of data
                        #region Труда затрат
                        number = 0;

                        row++;
                        ws.Cells[row, 2] = "Заработная плата(З.П)";
                        range = ws.get_Range("B" + row, "I" + row);
                        range.Merge(Type.Missing);
                        range.WrapText = true;
                        range.Font.Bold = true;
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                        // set title of worker
                        ws.Cells[++row, ReportColumnConstants.Name] = "Исполнители (должность)";
                        ws.Cells[row, ReportColumnConstants.Razryad] = "Разряд";
                        ws.Cells[row, ReportColumnConstants.PayInMonth] = "Месячный оклад";
                        ws.Cells[row, ReportColumnConstants.Avarge_Time] = "Среднее количество часов в месяце 168";
                        ws.Cells[row, ReportColumnConstants.CountWorkers] = "Численность (Чис)";
                        ws.Cells[row, ReportColumnConstants.PayForHour] = "Часовая тарифная ставка исполнителей п13/п14хп15";
                        ws.Cells[row, ReportColumnConstants.PayPercent25] = "Итого после начисление на 25% з/пл 1,25xп16";
                        ws.Cells[row, ReportColumnConstants.PricePayForSize] = "Заработная плата за объем п17хп7xп5";

                        range = ws.get_Range("B" + row, "I" + row);
                        range.WrapText = true;
                        range.Font.Bold = true;
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        row++;

                        for (int i = 1; i < 10; i++)
                        {
                            ws.Cells[row, i] = i + 9;
                        }
                        range = ws.get_Range("A" + row, "I" + row);
                        range.NumberFormat = "#,##0";
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        begin_row = ++row;

                        #region Polivie raboti
                        if (pol != null && pol.Count > 0)
                        {
                            ws.Cells[row, ReportColumnConstants.Name] = "Полевые работы";
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Merge(Type.Missing);
                            range.Font.Bold = true;
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            for (int i = 0; i < pol.Count; i++)
                            {
                                double Summa_Emp = 0;
                                double? Summa_Pay_Hour = 0;
                                double? Summa_Pay_Hour_25 = 0;
                                WorkDemView item = pol[i];
                                row++;
                                List<WorkTeam> teams = (new SmetaDbAppContext()).WorkTeams.
                                    ToList().Where(x => x.WorkDemId == item.Id).ToList();

                                if (teams.Count > 0)
                                {
                                    range = ws.get_Range("A" + row, "A" + (row + teams.Count));
                                    range.Merge(Type.Missing);
                                }
                                number++;
                                ws.Cells[row, ReportColumnConstants.Number] = number;
                                //Workers
                                // 10 => 18
                                double? s = 0;
                                teams.ForEach(x =>
                                {
                                    Summa_Emp += x.Count;
                                    Post post = Posts.Where(p => p.Id == x.PostId).First();
                                    double? PayForHour = Math.Round(post.Pay / 168 * x.Count * x.Koef, 2);
                                    Summa_Pay_Hour += PayForHour;
                                    double? PersentForPay25 = Math.Round((double)(PayForHour * Constants.Constants.Percent25), 2);
                                    Summa_Pay_Hour_25 += PersentForPay25;
                                    double? AllPayPrice = Math.Round((double)(PersentForPay25 * item.Size * item.Time), 2);

                                    ws.Cells[row, ReportColumnConstants.Name] = post.Name;
                                    ws.Cells[row, ReportColumnConstants.Razryad] = post.Raz;
                                    ws.Cells[row, ReportColumnConstants.PayInMonth] = post.Pay;
                                    ws.Cells[row, ReportColumnConstants.Avarge_Time] = Constants.Constants.Average_Time;
                                    ws.Cells[row, ReportColumnConstants.CountWorkers] = x.Count;
                                    ws.Cells[row, ReportColumnConstants.PayForHour] = PayForHour;
                                    ws.Cells[row, ReportColumnConstants.PayPercent25] = PersentForPay25;
                                    ws.Cells[row, ReportColumnConstants.PricePayForSize] = AllPayPrice;
                                    s += AllPayPrice;
                                    row++;
                                });

                                // set итого row
                                ws.Cells[row, ReportColumnConstants.Name] = "Итого";
                                ws.Cells[row, ReportColumnConstants.CountWorkers] = Summa_Emp;
                                ws.Cells[row, ReportColumnConstants.PayForHour] = Summa_Pay_Hour;
                                ws.Cells[row, ReportColumnConstants.PayPercent25] = Summa_Pay_Hour_25;
                                ws.Cells[row, ReportColumnConstants.PricePayForSize] = s;

                                range = ws.get_Range("A" + row, "I" + row);
                                range.Font.Bold = true;

                                pol[i].PricePayAll = s;
                                pricePolPay += s;
                            }
                            row++;
                            ws.Cells[row, ReportColumnConstants.Name] = "Итого полевые работы";
                            ws.Cells[row, ReportColumnConstants.PricePayForSize] = pricePolPay;
                            range = ws.get_Range("A" + row, "I" + row);
                            range.Font.Bold = true;

                            // if lab and kam not exist, then set one empty row
                            //if (lob.Count == 0 && kam.Count == 0)
                            //    row++;
                        }
                        #endregion

                        #region Laboratornie raboti
                        if (lob != null && lob.Count > 0)
                        {
                            ws.Cells[++row, ReportColumnConstants.Name] = "Лабораторные работы";
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Merge(Type.Missing);
                            range.Font.Bold = true;
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            for (int i = 0; i < lob.Count; i++)
                            {
                                double Summa_Emp = 0;
                                double? Summa_Pay_Hour = 0;
                                double? Summa_Pay_Hour_25 = 0;

                                WorkDemView item = lob[i];

                                row++;
                                List<WorkTeam> teams = (new SmetaDbAppContext()).WorkTeams.
                                    ToList().Where(x => x.WorkDemId == item.Id).ToList();

                                if (teams.Count > 0)
                                {
                                    range = ws.get_Range("A" + row, "A" + (row + teams.Count));
                                    range.Merge(Type.Missing);
                                }
                                number++;
                                ws.Cells[row, ReportColumnConstants.Number] = number;
                                //Workers
                                // 10 => 18
                                double? s = 0;
                                teams.ForEach(x =>
                                {
                                    Summa_Emp += x.Count;
                                    Post post = Posts.Where(p => p.Id == x.PostId).First();
                                    double? PayForHour = Math.Round(post.Pay / 168 * x.Count * x.Koef, 2);
                                    Summa_Pay_Hour += PayForHour;
                                    double? PersentForPay25 = Math.Round((double)(PayForHour * Constants.Constants.Percent25), 2);
                                    Summa_Pay_Hour_25 += PersentForPay25;
                                    double? AllPayPrice = Math.Round((double)(PersentForPay25 * item.Size * item.Time), 2);

                                    ws.Cells[row, ReportColumnConstants.Name] = post.Name;
                                    ws.Cells[row, ReportColumnConstants.Razryad] = post.Raz;
                                    ws.Cells[row, ReportColumnConstants.PayInMonth] = post.Pay;
                                    ws.Cells[row, ReportColumnConstants.Avarge_Time] = Constants.Constants.Average_Time;
                                    ws.Cells[row, ReportColumnConstants.CountWorkers] = x.Count;
                                    ws.Cells[row, ReportColumnConstants.PayForHour] = PayForHour;
                                    ws.Cells[row, ReportColumnConstants.PayPercent25] = PersentForPay25;
                                    ws.Cells[row, ReportColumnConstants.PricePayForSize] = AllPayPrice;
                                    s += AllPayPrice;
                                    row++;
                                });

                                // set итого row
                                ws.Cells[row, ReportColumnConstants.Name] = "Итого";
                                ws.Cells[row, ReportColumnConstants.CountWorkers] = Summa_Emp;
                                ws.Cells[row, ReportColumnConstants.PayForHour] = Summa_Pay_Hour;
                                ws.Cells[row, ReportColumnConstants.PayPercent25] = Summa_Pay_Hour_25;
                                ws.Cells[row, ReportColumnConstants.PricePayForSize] = s;

                                range = ws.get_Range("A" + row, "I" + row);
                                range.Font.Bold = true;

                                //pricePay += s;
                                lob[i].PricePayAll = s;
                                priceLabPay += s;
                            }
                            row++;
                            ws.Cells[row, ReportColumnConstants.Name] = "Итого лабораторные работы";
                            ws.Cells[row, ReportColumnConstants.PricePayForSize] = priceLabPay;
                            range = ws.get_Range("A" + row, "I" + row);
                            range.Font.Bold = true;

                            // if kam not exist, then set one empty row
                            //if (kam.Count == 0)
                            //    row++;
                        }
                        #endregion

                        #region Kameralnie raboti
                        if (kam != null && kam.Count > 0)
                        {
                            ws.Cells[++row, ReportColumnConstants.Name] = "Камеральные работы";
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Merge(Type.Missing);
                            range.Font.Bold = true;
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            for (int i = 0; i < kam.Count; i++)
                            {
                                double Summa_Emp = 0;
                                double? Summa_Pay_Hour = 0;
                                double? Summa_Pay_Hour_25 = 0;

                                WorkDemView item = kam[i];

                                row++;
                                List<WorkTeam> teams = (new SmetaDbAppContext()).WorkTeams.
                                    ToList().Where(x => x.WorkDemId == item.Id).ToList();

                                if (teams.Count > 0)
                                {
                                    range = ws.get_Range("A" + row, "A" + (row + teams.Count));
                                    range.Merge(Type.Missing);
                                }
                                number++;
                                ws.Cells[row, ReportColumnConstants.Number] = number;
                                //Workers
                                // 10 => 18
                                double? s = 0;
                                teams.ForEach(x =>
                                {
                                    Summa_Emp += x.Count;
                                    Post post = Posts.Where(p => p.Id == x.PostId).First();
                                    double? PayForHour = Math.Round(post.Pay / 168 * x.Count * x.Koef, 2);
                                    Summa_Pay_Hour += PayForHour;
                                    double? PersentForPay25 = Math.Round((double)(PayForHour * Constants.Constants.Percent25), 2);
                                    Summa_Pay_Hour_25 += PersentForPay25;
                                    double? AllPayPrice = Math.Round((double)(PersentForPay25 * item.Size * item.Time), 2);

                                    ws.Cells[row, ReportColumnConstants.Name] = post.Name;
                                    ws.Cells[row, ReportColumnConstants.Razryad] = post.Raz;
                                    ws.Cells[row, ReportColumnConstants.PayInMonth] = post.Pay;
                                    ws.Cells[row, ReportColumnConstants.Avarge_Time] = Constants.Constants.Average_Time;
                                    ws.Cells[row, ReportColumnConstants.CountWorkers] = x.Count;
                                    ws.Cells[row, ReportColumnConstants.PayForHour] = PayForHour;
                                    ws.Cells[row, ReportColumnConstants.PayPercent25] = PersentForPay25;
                                    ws.Cells[row, ReportColumnConstants.PricePayForSize] = AllPayPrice;
                                    s += AllPayPrice;
                                    row++;
                                });

                                // set итого row
                                ws.Cells[row, ReportColumnConstants.Name] = "Итого";
                                ws.Cells[row, ReportColumnConstants.CountWorkers] = Summa_Emp;
                                ws.Cells[row, ReportColumnConstants.PayForHour] = Summa_Pay_Hour;
                                ws.Cells[row, ReportColumnConstants.PayPercent25] = Summa_Pay_Hour_25;
                                ws.Cells[row, ReportColumnConstants.PricePayForSize] = s;

                                range = ws.get_Range("A" + row, "I" + row);
                                range.Font.Bold = true;

                                kam[i].PricePayAll = s;
                                priceKamPay += s;
                            }
                            row++;
                            ws.Cells[row, ReportColumnConstants.Name] = "Итого камеральные работы";
                            ws.Cells[row, ReportColumnConstants.PricePayForSize] = priceKamPay;
                            range = ws.get_Range("A" + row, "I" + row);
                            range.Font.Bold = true;

                        }
                        #endregion

                        ws.Cells[++row, ReportColumnConstants.Name] = "Всего труда затрат";
                        ws.Cells[row, ReportColumnConstants.PricePayForSize] = pricePolPay + priceLabPay + priceKamPay;
                        range = ws.get_Range("B" + row, "I" + row);
                        range.Font.Bold = true;

                        #region styles
                        //range = ws.get_Range("C" + begin_row, "C" + row);
                        //range.NumberFormat = "#,##0";
                        range = ws.get_Range("D" + begin_row, "D" + row);
                        range.NumberFormat = "#,##0.00";
                        range = ws.get_Range("E" + begin_row, "E" + row);
                        range.NumberFormat = "";
                        range = ws.get_Range("F" + begin_row, "F" + row);
                        range.NumberFormat = "";
                        //range = ws.get_Range("G" + begin_row, "G" + row);
                        //range.NumberFormat = "#,##0";
                        //range = ws.get_Range("H" + begin_row, "H" + row);
                        //range.NumberFormat = "#,##0";
                        //range = ws.get_Range("I" + begin_row, "I" + row);
                        //range.NumberFormat = "#,##0";
                        #endregion

                        row++;
                        #endregion

                        #region Koefs

                        List<Commentary> commentaries = new List<Commentary>();

                        // All pay price
                        pricePolPay = 0;
                        priceLabPay = 0;
                        priceKamPay = 0;

                        number = 0;

                        row++;
                        ws.Cells[row, 1] = "Коэффициенты и полевое довольствие в сум";
                        range = ws.get_Range("A" + row, "I" + row);
                        range.Merge(Type.Missing);
                        range.WrapText = true;
                        range.Font.Bold = true;
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                        // set title of worker
                        //ws.Cells[++row, ReportColumnConstants.Name] = "Районный коэффициент";
                        ws.Cells[++row, ReportColumnConstants.Razryad] = "Повышающие";
                        ws.Cells[row, ReportColumnConstants.PayInMonth] = "Понижающие";
                        ws.Cells[row, ReportColumnConstants.Avarge_Time] = "Полевое довольствие сум";
                        ws.Cells[row, ReportColumnConstants.CountWorkers] = "Итого коэффициенты п21xп22";
                        //ws.Cells[row, ReportColumnConstants.PayForHour] = "Часовая тарифная ставка исполнителей п4/п5хп6";
                        ws.Cells[row, ReportColumnConstants.PayPercent25] = "Заработная плата";
                        ws.Cells[row, ReportColumnConstants.PricePayForSize] = "Итого заработная плата п24xп18+п23";

                        range = ws.get_Range("B" + row, "I" + row);
                        range.WrapText = true;
                        range.Font.Bold = true;
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                        row++;
                        for (int i = 1; i < 10; i++)
                        {
                            ws.Cells[row, i] = i + 18;
                        }
                        range = ws.get_Range("A" + row, "I" + row);
                        range.NumberFormat = "#,##0";
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        begin_row = ++row;

                        #region Polivie raboti
                        if (pol != null && pol.Count > 0)
                        {
                            ws.Cells[row, ReportColumnConstants.Name] = "Полевые работы";
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Merge(Type.Missing);
                            range.Font.Bold = true;
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            begin_row = row + 1;

                            for (int i = 0; i < pol.Count; i++)
                            {
                                WorkDemView item = pol[i];
                                row++;
                                number++;
                                ws.Cells[row, ReportColumnConstants.Number] = number;
                                //ws.Cells[row, ReportColumnConstants.Name] = 1.15;
                                //Workers
                                // 10 => 18
                                double s1 = 1;
                                double s2 = 1;
                                double? s = 1;

                                item.Commentaries.Where(x => x.IsYes).ToList().ForEach(x =>
                                {
                                    if (x.Commentary.Koef > 1)
                                        s1 *= x.Commentary.Koef;
                                    else
                                        s2 *= x.Commentary.Koef;
                                    //row++;
                                    commentaries.Add(x.Commentary);
                                });

                                double allkoef = s1 * s2;
                                s = allkoef * item.PricePayAll + (item.PolviyDovol == null ? 0 : item.PolviyDovol);
                                s = Math.Round((double)s, 2);

                                //ws.Cells[++row, ReportColumnConstants.Name] = "Районный коэффициент";
                                ws.Cells[row, ReportColumnConstants.Razryad] = s1;
                                ws.Cells[row, ReportColumnConstants.PayInMonth] = s2;
                                ws.Cells[row, ReportColumnConstants.Avarge_Time] = item.PolviyDovol;
                                ws.Cells[row, ReportColumnConstants.CountWorkers] = allkoef;
                                //ws.Cells[row, ReportColumnConstants.PayForHour] = "Часовая тарифная ставка исполнителей п4/п5хп6";
                                ws.Cells[row, ReportColumnConstants.PayPercent25] = item.PricePayAll;
                                ws.Cells[row, ReportColumnConstants.PricePayForSize] = s;

                                item.PricePayAll = s;
                                pricePolPay += s;
                            }

                            // styles
                            range = ws.get_Range("B" + begin_row, "B" + row);
                            range.NumberFormat = "#,##0.00";
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            range = ws.get_Range("C" + begin_row, "C" + row);
                            range.NumberFormat = "#,##0.00";
                            range = ws.get_Range("D" + begin_row, "D" + row);
                            range.NumberFormat = "#,##0.00";
                            range = ws.get_Range("E" + begin_row, "E" + row);
                            range.NumberFormat = "#,##0.00";
                            range = ws.get_Range("F" + begin_row, "F" + row);
                            range.NumberFormat = "#,##0.00";

                            ws.Cells[++row, ReportColumnConstants.Name] = "Итого полевые работы";
                            ws.Cells[row, ReportColumnConstants.PricePayForSize] = pricePolPay;

                            range = ws.get_Range("B" + row, "B" + row);
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

                            ws.Cells[++row, ReportColumnConstants.Name] = "Районный коэффициент к заработной плате";
                            ws.Cells[row, ReportColumnConstants.PricePayForSize] = 1.15;

                            // Районный коэффициент к заработной плате
                            pricePolPay = Math.Round(Constants.Constants.RayoniyKoef * (double)pricePolPay, 2);
                            ws.Cells[++row, ReportColumnConstants.Name] = "Итого полевые работы";
                            ws.Cells[row, ReportColumnConstants.PricePayForSize] = pricePolPay;

                            range = ws.get_Range("A" + (row - 2), "I" + row);
                            range.Font.Bold = true;

                            //// if lab and kam not exist, then one empty row
                            //if (kam.Count == 0 && lob.Count == 0)
                            //    row++;
                        }
                        #endregion

                        #region Laboratorniy raboti
                        if (lob != null && lob.Count > 0)
                        {
                            ws.Cells[row, ReportColumnConstants.Name] = "Лабораторные работы";
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Merge(Type.Missing);
                            range.Font.Bold = true;
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            begin_row = row + 1;

                            foreach (var item in lob)
                            {
                                row++;
                                number++;
                                ws.Cells[row, ReportColumnConstants.Number] = number;
                                //ws.Cells[row, ReportColumnConstants.Name] = 1;
                                //Workers
                                // 10 => 18
                                double s1 = 1;
                                double s2 = 1;
                                double? s = 1;

                                item.Commentaries.Where(x => x.IsYes).ToList().ForEach(x =>
                                {
                                    if (x.Commentary.Koef > 1)
                                        s1 *= x.Commentary.Koef;
                                    else
                                        s2 *= x.Commentary.Koef;
                                    //row++;
                                    commentaries.Add(x.Commentary);
                                });

                                double allkoef = s1 * s2;
                                s = allkoef * item.PricePayAll + (item.PolviyDovol == null ? 0 : item.PolviyDovol);

                                //ws.Cells[++row, ReportColumnConstants.Name] = "Районный коэффициент";
                                ws.Cells[row, ReportColumnConstants.Razryad] = s1;
                                ws.Cells[row, ReportColumnConstants.PayInMonth] = s2;
                                ws.Cells[row, ReportColumnConstants.Avarge_Time] = item.PolviyDovol;
                                ws.Cells[row, ReportColumnConstants.CountWorkers] = allkoef;
                                ws.Cells[row, ReportColumnConstants.PayPercent25] = item.PricePayAll;
                                //ws.Cells[row, ReportColumnConstants.PayPercent25] = "Итого после начисление на з/пл 1,25xп8";
                                ws.Cells[row, ReportColumnConstants.PricePayForSize] = s;

                                priceLabPay += s;
                                item.PricePayAll = s;
                            }

                            // styles
                            range = ws.get_Range("B" + begin_row, "B" + row);
                            range.NumberFormat = "#,##0.00";
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            range = ws.get_Range("C" + begin_row, "C" + row);
                            range.NumberFormat = "#,##0.00";
                            range = ws.get_Range("D" + begin_row, "D" + row);
                            range.NumberFormat = "#,##0.00";
                            range = ws.get_Range("E" + begin_row, "E" + row);
                            range.NumberFormat = "#,##0.00";
                            range = ws.get_Range("F" + begin_row, "F" + row);
                            range.NumberFormat = "#,##0.00";

                            ws.Cells[++row, ReportColumnConstants.Name] = "Итого лабораторные работы";
                            ws.Cells[row, ReportColumnConstants.PricePayForSize] = priceLabPay;
                            range = ws.get_Range("A" + row, "I" + row);
                            range.Font.Bold = true;
                            range = ws.get_Range("B" + row, "B" + row);
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

                            //// if lab and kam not exist, then one empty row
                            //if (kam.Count == 0)
                            //    row++;
                        }
                        #endregion

                        #region Kameralnie raboti
                        if (kam != null && kam.Count > 0)
                        {
                            ws.Cells[++row, ReportColumnConstants.Name] = "Камеральные работы";
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Merge(Type.Missing);
                            range.Font.Bold = true;
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            begin_row = row + 1;

                            for (int i = 0; i < kam.Count; i++)
                            {
                                WorkDemView item = kam[i];
                                row++;
                                number++;
                                ws.Cells[row, ReportColumnConstants.Number] = number;
                                //ws.Cells[row, ReportColumnConstants.Name] = 1.0;
                                //Workers
                                // 10 => 18
                                double s1 = 1;
                                double s2 = 1;
                                double? s = 1;

                                item.Commentaries.Where(x => x.IsYes).ToList().ForEach(x =>
                                {
                                    if (x.Commentary.Koef > 1)
                                        s1 *= x.Commentary.Koef;
                                    else
                                        s2 *= x.Commentary.Koef;
                                    //row++;
                                    commentaries.Add(x.Commentary);
                                });

                                double allkoef = s1 * s2;
                                s = allkoef * item.PricePayAll + (item.PolviyDovol == null ? 0 : item.PolviyDovol);

                                //ws.Cells[++row, ReportColumnConstants.Name] = "Районный коэффициент";
                                ws.Cells[row, ReportColumnConstants.Razryad] = s1;
                                ws.Cells[row, ReportColumnConstants.PayInMonth] = s2;
                                ws.Cells[row, ReportColumnConstants.Avarge_Time] = item.PolviyDovol;
                                ws.Cells[row, ReportColumnConstants.CountWorkers] = allkoef;
                                ws.Cells[row, ReportColumnConstants.PayPercent25] = item.PricePayAll;
                                //ws.Cells[row, ReportColumnConstants.PayPercent25] = "Итого после начисление на з/пл 1,25xп8";
                                ws.Cells[row, ReportColumnConstants.PricePayForSize] = s;

                                priceKamPay += s;
                                item.PricePayAll = s;
                            }

                            // styles
                            range = ws.get_Range("B" + begin_row, "B" + row);
                            range.NumberFormat = "#,##0.00";
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            range = ws.get_Range("C" + begin_row, "C" + row);
                            range.NumberFormat = "#,##0.00";
                            range = ws.get_Range("D" + begin_row, "D" + row);
                            range.NumberFormat = "#,##0.00";
                            range = ws.get_Range("E" + begin_row, "E" + row);
                            range.NumberFormat = "#,##0.00";
                            range = ws.get_Range("F" + begin_row, "F" + row);
                            range.NumberFormat = "#,##0.00";

                            ws.Cells[++row, ReportColumnConstants.Name] = "Итого камеральные работы";
                            ws.Cells[row, ReportColumnConstants.PricePayForSize] = priceKamPay;
                            range = ws.get_Range("A" + row, "I" + row);
                            range.Font.Bold = true;
                            range = ws.get_Range("B" + row, "B" + row);
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        }
                        #endregion

                        ws.Cells[++row, ReportColumnConstants.Name] = "Всего труда затрат";
                        ws.Cells[row, ReportColumnConstants.PricePayForSize] = pricePolPay + priceLabPay + priceKamPay;
                        range = ws.get_Range("B" + row, "I" + row);
                        range.Font.Bold = true;

                        row++;
                        #endregion

                        #region Машины, приборы и оборудование (амортизация)(А)
                        number = 0;

                        row++;
                        ws.Cells[row, 1] = "Машины, приборы и оборудование (амортизация)(А)";
                        range = ws.get_Range("A" + row, "I" + row);
                        range.Merge(Type.Missing);
                        range.WrapText = true;
                        range.Font.Bold = true;
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                        // set title of worker
                        ws.Cells[++row, ReportColumnConstants.Name] = "Наименование";
                        ws.Cells[row, ReportColumnConstants.Razryad] = "Код";
                        ws.Cells[row, ReportColumnConstants.PayInMonth] = "Единица измерения";
                        ws.Cells[row, ReportColumnConstants.Avarge_Time] = "Балансовая стоимость";
                        ws.Cells[row, ReportColumnConstants.CountWorkers] = "Норматив амортизации %";
                        ws.Cells[row, ReportColumnConstants.PayForHour] = "Количество";
                        ws.Cells[row, ReportColumnConstants.PayPercent25] = "Амортизация за  час";
                        ws.Cells[row, ReportColumnConstants.PricePayForSize] = "Амортизация за  весь объем п5хп7хп35 сум";

                        range = ws.get_Range("B" + row, "I" + row);
                        range.WrapText = true;
                        range.Font.Bold = true;
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        row++;

                        for (int i = 1; i < 10; i++)
                        {
                            ws.Cells[row, i] = i + 27;
                        }
                        range = ws.get_Range("A" + row, "I" + row);
                        range.NumberFormat = "#,##0";
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                        begin_row = ++row;
                        #region Polivie raboti
                        if (pol != null && pol.Count > 0)
                        {
                            ws.Cells[row, ReportColumnConstants.Name] = "Полевые работы";
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Merge(Type.Missing);
                            range.Font.Bold = true;
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            foreach (var item in pol)
                            {
                                row++;

                                int countPribors = item.Pribors.Where(x => x.IsYes).Count();
                                if (countPribors > 0)
                                {
                                    range = ws.get_Range("A" + row, "A" + (row + countPribors));
                                    range.Merge(Type.Missing);
                                }
                                number++;
                                ws.Cells[row, ReportColumnConstants.Number] = number;
                                //Workers
                                // 10 => 18
                                double? s = 0;
                                item.Pribors.Where(x => x.IsYes).ToList().ForEach(x =>
                                {
                                    Pribor pribor = x.Pribor;

                                    double? amort = pribor.Price * pribor.Percent * 0.01 * x.Count;
                                    double? MonthPrice = amort / 12;
                                    double? HourPrice = Math.Round((double)MonthPrice / 720, 2);
                                    double? AllPayPrice = Math.Round((double)(HourPrice * item.Size * item.Time), 2);

                                    // styles for cell C
                                    range = ws.get_Range("C" + row, "C" + row);
                                    range.NumberFormat = "@";

                                    ws.Cells[row, ReportColumnConstants.Name] = pribor.Name;
                                    ws.Cells[row, ReportColumnConstants.Razryad] = Helper.FillWithZero(pribor.Code);
                                    ws.Cells[row, ReportColumnConstants.PayInMonth] = pribor.Dimension;
                                    ws.Cells[row, ReportColumnConstants.Avarge_Time] = pribor.Price;
                                    ws.Cells[row, ReportColumnConstants.CountWorkers] = pribor.Percent;
                                    ws.Cells[row, ReportColumnConstants.PayForHour] = x.Count;
                                    ws.Cells[row, ReportColumnConstants.PayPercent25] = HourPrice;
                                    ws.Cells[row, ReportColumnConstants.PricePayForSize] = AllPayPrice;
                                    s += AllPayPrice;
                                    row++;
                                });

                                // set итого row
                                ws.Cells[row, ReportColumnConstants.Name] = "Итого";
                                ws.Cells[row, ReportColumnConstants.Pribor_Amart_Price_Hour] = item.PricePriborInHour;
                                ws.Cells[row, ReportColumnConstants.PricePayForSize] = s;
                                range = ws.get_Range("B" + row, "I" + row);
                                range.Font.Bold = true;

                                pricePolPribor += s;
                            }

                            ws.Cells[++row, ReportColumnConstants.Name] = "Итого полевые работы";
                            ws.Cells[row, ReportColumnConstants.PricePayForSize] = pricePolPribor;
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Font.Bold = true;

                            //if (kam.Count == 0 && lob.Count == 0)
                            //    row++;
                        }
                        #endregion

                        #region Laboratornie raboti
                        if (lob != null && lob.Count > 0)
                        {
                            ws.Cells[++row, ReportColumnConstants.Name] = "Лабораторные работы";
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Merge(Type.Missing);
                            range.Font.Bold = true;
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            foreach (var item in lob)
                            {
                                row++;
                                int countPribors = item.Pribors.Where(x => x.IsYes).Count();
                                if (countPribors > 0)
                                {
                                    range = ws.get_Range("A" + row, "A" + (row + countPribors));
                                    range.Merge(Type.Missing);
                                }
                                number++;
                                ws.Cells[row, ReportColumnConstants.Number] = number;
                                //Workers
                                // 10 => 18
                                double? s = 0;
                                item.Pribors.Where(x => x.IsYes).ToList().ForEach(x =>
                                {
                                    Pribor pribor = x.Pribor;

                                    double? amort = pribor.Price * pribor.Percent * 0.01 * x.Count;
                                    double? MonthPrice = (amort / 12);
                                    double? HourPrice = Math.Round((double)MonthPrice / 720, 2);
                                    double? AllPayPrice = Math.Round((double)(HourPrice * item.Size * item.Time), 2);

                                    // styles for cell C
                                    range = ws.get_Range("C" + row, "C" + row);
                                    range.NumberFormat = "@";

                                    ws.Cells[row, ReportColumnConstants.Name] = pribor.Name;
                                    ws.Cells[row, ReportColumnConstants.Razryad] = Helper.FillWithZero(pribor.Code);
                                    ws.Cells[row, ReportColumnConstants.PayInMonth] = pribor.Dimension;
                                    ws.Cells[row, ReportColumnConstants.Avarge_Time] = pribor.Price;
                                    ws.Cells[row, ReportColumnConstants.CountWorkers] = pribor.Percent;
                                    ws.Cells[row, ReportColumnConstants.PayForHour] = x.Count;
                                    ws.Cells[row, ReportColumnConstants.PayPercent25] = HourPrice;
                                    ws.Cells[row, ReportColumnConstants.PricePayForSize] = AllPayPrice;
                                    s += AllPayPrice;
                                    row++;
                                });

                                // set итого row
                                ws.Cells[row, ReportColumnConstants.Name] = "Итого";
                                ws.Cells[row, ReportColumnConstants.PricePayForSize] = s;
                                range = ws.get_Range("B" + row, "I" + row);
                                range.Font.Bold = true;
                                priceLabPribor += s;
                            }

                            ws.Cells[++row, ReportColumnConstants.Name] = "Итого лабораторные работы";
                            ws.Cells[row, ReportColumnConstants.PricePayForSize] = priceLabPribor;
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Font.Bold = true;

                            //if (kam.Count == 0)
                            //    row++;
                        }
                        #endregion

                        #region Kameralnie raboti
                        if (kam != null && kam.Count > 0)
                        {
                            ws.Cells[++row, ReportColumnConstants.Name] = "Камеральные работы";
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Merge(Type.Missing);
                            range.Font.Bold = true;
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            foreach (var item in kam)
                            {
                                row++;
                                int countPribors = item.Pribors.Where(x => x.IsYes).Count();
                                if (countPribors > 0)
                                {
                                    range = ws.get_Range("A" + row, "A" + (row + countPribors));
                                    range.Merge(Type.Missing);
                                }
                                number++;
                                ws.Cells[row, ReportColumnConstants.Number] = number;
                                //Workers
                                // 10 => 18
                                double? s = 0;
                                item.Pribors.Where(x => x.IsYes).ToList().ForEach(x =>
                                {
                                    Pribor pribor = x.Pribor;

                                    double? amort = pribor.Price * pribor.Percent * 0.01 * x.Count;
                                    double? MonthPrice = (amort / 12);
                                    double? HourPrice = Math.Round((double)MonthPrice / 720, 2);
                                    double? AllPayPrice = Math.Round((double)(HourPrice * item.Size * item.Time), 2);

                                    // styles for cell C
                                    range = ws.get_Range("C" + row, "C" + row);
                                    range.NumberFormat = "@";

                                    ws.Cells[row, ReportColumnConstants.Name] = pribor.Name;
                                    ws.Cells[row, ReportColumnConstants.Razryad] = Helper.FillWithZero(pribor.Code);
                                    ws.Cells[row, ReportColumnConstants.PayInMonth] = pribor.Dimension;
                                    ws.Cells[row, ReportColumnConstants.Avarge_Time] = pribor.Price;
                                    ws.Cells[row, ReportColumnConstants.CountWorkers] = pribor.Percent;
                                    ws.Cells[row, ReportColumnConstants.PayForHour] = x.Count;
                                    ws.Cells[row, ReportColumnConstants.PayPercent25] = HourPrice;
                                    ws.Cells[row, ReportColumnConstants.PricePayForSize] = AllPayPrice;
                                    s += AllPayPrice;
                                    row++;
                                });

                                // set итого row
                                ws.Cells[row, ReportColumnConstants.Name] = "Итого";
                                ws.Cells[row, ReportColumnConstants.PricePayForSize] = s;
                                range = ws.get_Range("B" + row, "I" + row);
                                range.Font.Bold = true;
                                priceKamPribor += s;
                            }
                            ws.Cells[++row, ReportColumnConstants.Name] = "Итого камеральные работы";
                            ws.Cells[row, ReportColumnConstants.PricePayForSize] = priceKamPribor;
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Font.Bold = true;
                        }
                        #endregion

                        ws.Cells[++row, ReportColumnConstants.Name] = "Всего амортизация";
                        ws.Cells[row, ReportColumnConstants.PricePayForSize] = pricePolPribor + priceLabPribor + priceKamPribor;
                        range = ws.get_Range("B" + row, "I" + row);
                        range.Font.Bold = true;

                        #region styles
                        //range = ws.get_Range("C" + begin_row, "C" + row);
                        //range.NumberFormat = "@";
                        //range.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        //range = ws.get_Range("D" + begin_row, "D" + row);
                        //range.NumberFormat = "#,##0";
                        range = ws.get_Range("E" + begin_row, "E" + row);
                        range.NumberFormat = "#,##0.00";
                        //range = ws.get_Range("F" + begin_row, "F" + row);
                        //range.NumberFormat = "#,##0";
                        range = ws.get_Range("G" + begin_row, "G" + row);
                        range.NumberFormat = "";
                        //range = ws.get_Range("H" + begin_row, "H" + row);
                        //range.NumberFormat = "#,##0";
                        //range = ws.get_Range("I" + begin_row, "I" + row);
                        //range.NumberFormat = "#,##0";
                        #endregion

                        row++;

                        #endregion

                        #region Материалы(М)
                        number = 0;

                        row++;
                        ws.Cells[row, 1] = "Материалы(М)";
                        range = ws.get_Range("A" + row, "I" + row);
                        range.Merge(Type.Missing);
                        range.WrapText = true;
                        range.Font.Bold = true;
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                        // set title of worker
                        ws.Cells[++row, ReportColumnConstants.Name] = "Наименование";
                        ws.Cells[row, ReportColumnConstants.Razryad] = "Код";
                        ws.Cells[row, ReportColumnConstants.PayInMonth] = "Единица измерения штук";
                        ws.Cells[row, ReportColumnConstants.Avarge_Time] = "Стоимость единицы материала (сум)";
                        ws.Cells[row, ReportColumnConstants.CountWorkers] = "Норматив на единицу объема";
                        ws.Cells[row, ReportColumnConstants.PayForHour] = "За вес объём";
                        //ws.Cells[row, ReportColumnConstants.PayPercent25] = "Амортизация за  час п7/720 сум";
                        ws.Cells[row, ReportColumnConstants.PricePayForSize] = " Стоимость материалов в сум п.5хп.6х(Ф.О)";

                        range = ws.get_Range("B" + row, "I" + row);
                        range.WrapText = true;
                        range.Font.Bold = true;
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                        row++;
                        for (int i = 1; i < 10; i++)
                        {
                            ws.Cells[row, i] = i + 36;
                        }
                        range = ws.get_Range("A" + row, "I" + row);
                        range.NumberFormat = "#,##0";
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        begin_row = ++row;

                        #region Polivie raboti
                        if (pol != null && pol.Count > 0)
                        {
                            ws.Cells[row, ReportColumnConstants.Name] = "Полевые работы";
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Merge(Type.Missing);
                            range.Font.Bold = true;
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            foreach (var item in pol)
                            {
                                row++;

                                int countMaterial = item.Materials.Where(x => x.IsYes).Count();
                                if (countMaterial > 0)
                                {
                                    range = ws.get_Range("A" + row, "A" + (row + countMaterial));
                                    range.Merge(Type.Missing);
                                }
                                number++;
                                ws.Cells[row, ReportColumnConstants.Number] = number;
                                //Workers
                                // 10 => 18
                                double? s = 0;
                                item.Materials.Where(y => y.IsYes).ToList().ForEach(x =>
                                {
                                    Material material = x.Material;

                                    double? AllPayPrice = 0;
                                    double? count = x.Count;

                                    // styles for cell C
                                    range = ws.get_Range("C" + row, "C" + row);
                                    range.NumberFormat = "@";

                                    ws.Cells[row, ReportColumnConstants.Name] = material.Name;
                                    ws.Cells[row, ReportColumnConstants.Razryad] = Helper.FillWithZero(material.Code);
                                    ws.Cells[row, ReportColumnConstants.PayInMonth] = material.Dimension;
                                    ws.Cells[row, ReportColumnConstants.Avarge_Time] = material.Price;
                                    ws.Cells[row, ReportColumnConstants.CountWorkers] = count;
                                    if (x.ForAllObject)
                                    {
                                        ws.Cells[row, ReportColumnConstants.PayForHour] = "Да";
                                        double? price = Math.Round((double)(material.Price * count), 2);
                                        AllPayPrice = price;
                                    }
                                    else
                                    {
                                        //ws.Cells[row, ReportColumnConstants.PayForHour] = "Нет";
                                        double? price = Math.Round((double)(material.Price * count * item.Size), 2);
                                        AllPayPrice = price;
                                    }
                                    //ws.Cells[row, ReportColumnConstants.PayPercent25] = HourPrice;
                                    ws.Cells[row, ReportColumnConstants.PricePayForSize] = AllPayPrice;
                                    s += AllPayPrice;
                                    row++;
                                });

                                // set итого row
                                ws.Cells[row, ReportColumnConstants.Name] = "Итого";
                                ws.Cells[row, ReportColumnConstants.PricePayForSize] = s;
                                pricePolMaterial += s;
                                range = ws.get_Range("B" + row, "I" + row);
                                range.Font.Bold = true;
                                //row++;
                            }

                            ws.Cells[++row, ReportColumnConstants.Name] = "Итого полевые работы";
                            ws.Cells[row, ReportColumnConstants.PricePayForSize] = pricePolMaterial;
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Font.Bold = true;

                            //if (kam.Count == 0 && lob.Count == 0)
                            //    row++;
                        }
                        #endregion

                        #region Laboratornie raboti
                        if (lob != null && lob.Count > 0)
                        {
                            ws.Cells[++row, ReportColumnConstants.Name] = "Лабораторные работы";
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Merge(Type.Missing);
                            range.Font.Bold = true;
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            foreach (var item in lob)
                            {
                                row++;

                                int countMaterial = item.Materials.Where(x => x.IsYes).Count();
                                if (countMaterial > 0)
                                {
                                    range = ws.get_Range("A" + row, "A" + (row + countMaterial));
                                    range.Merge(Type.Missing);
                                }
                                number++;
                                ws.Cells[row, ReportColumnConstants.Number] = number;
                                //Workers
                                // 10 => 18
                                double? s = 0;
                                item.Materials.Where(x => x.IsYes).ToList().ForEach(x =>
                                {
                                    Material material = x.Material;

                                    double? AllPayPrice = 0;
                                    double? count = x.Count;

                                    // styles for cell C
                                    range = ws.get_Range("C" + row, "C" + row);
                                    range.NumberFormat = "@";

                                    ws.Cells[row, ReportColumnConstants.Name] = material.Name;
                                    ws.Cells[row, ReportColumnConstants.Razryad] = Helper.FillWithZero(material.Code);
                                    ws.Cells[row, ReportColumnConstants.PayInMonth] = material.Dimension;
                                    ws.Cells[row, ReportColumnConstants.Avarge_Time] = material.Price;
                                    ws.Cells[row, ReportColumnConstants.CountWorkers] = count;
                                    if (x.ForAllObject)
                                    {
                                        ws.Cells[row, ReportColumnConstants.PayForHour] = "Да";
                                        double? price = Math.Round((double)(material.Price * count), 2);
                                        AllPayPrice = price;
                                    }
                                    else
                                    {
                                        //ws.Cells[row, ReportColumnConstants.PayForHour] = "Нет";
                                        double? price = Math.Round((double)(material.Price * count * item.Size), 2);
                                        AllPayPrice = price;
                                    }
                                    //ws.Cells[row, ReportColumnConstants.PayPercent25] = HourPrice;
                                    ws.Cells[row, ReportColumnConstants.PricePayForSize] = AllPayPrice;
                                    s += AllPayPrice;
                                    row++;
                                });

                                // set итого row
                                ws.Cells[row, ReportColumnConstants.Name] = "Итого";
                                ws.Cells[row, ReportColumnConstants.PricePayForSize] = s;
                                priceLabPribor += s;
                                range = ws.get_Range("B" + row, "I" + row);
                                range.Font.Bold = true;
                                //row++;
                            }

                            ws.Cells[++row, ReportColumnConstants.Name] = "Итого лабораторные работы";
                            ws.Cells[row, ReportColumnConstants.PricePayForSize] = priceLabPribor;
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Font.Bold = true;

                            //if (kam.Count == 0)
                            //    row++;
                        }
                        #endregion

                        #region Kameralnie raboti
                        if (kam != null && kam.Count > 0)
                        {
                            ws.Cells[++row, ReportColumnConstants.Name] = "Камеральные работы";
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Merge(Type.Missing);
                            range.Font.Bold = true;
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            foreach (var item in kam)
                            {
                                row++;

                                int countMaterial = item.Materials.Where(x => x.IsYes).Count();
                                if (countMaterial > 0)
                                {
                                    range = ws.get_Range("A" + row, "A" + (row + countMaterial));
                                    range.Merge(Type.Missing);
                                }
                                number++;
                                ws.Cells[row, ReportColumnConstants.Number] = number;
                                //Workers
                                // 10 => 18
                                double? s = 0;
                                item.Materials.Where(x => x.IsYes).ToList().ForEach(x =>
                                {
                                    Material material = x.Material;

                                    double? AllPayPrice = 0;
                                    double? count = x.Count;

                                    // styles for cell C
                                    range = ws.get_Range("C" + row, "C" + row);
                                    range.NumberFormat = "@";

                                    ws.Cells[row, ReportColumnConstants.Name] = material.Name;
                                    ws.Cells[row, ReportColumnConstants.Razryad] = Helper.FillWithZero(material.Code);
                                    ws.Cells[row, ReportColumnConstants.PayInMonth] = material.Dimension;
                                    ws.Cells[row, ReportColumnConstants.Avarge_Time] = material.Price;
                                    ws.Cells[row, ReportColumnConstants.CountWorkers] = count;
                                    if (x.ForAllObject)
                                    {
                                        ws.Cells[row, ReportColumnConstants.PayForHour] = "Да";
                                        double? price = Math.Round((double)(material.Price * count), 2);
                                        AllPayPrice = price;
                                    }
                                    else
                                    {
                                        //ws.Cells[row, ReportColumnConstants.PayForHour] = "Нет";
                                        double? price = Math.Round((double)(material.Price * count * item.Size), 2);
                                        AllPayPrice = price;
                                    }
                                    //ws.Cells[row, ReportColumnConstants.PayPercent25] = HourPrice;
                                    ws.Cells[row, ReportColumnConstants.PricePayForSize] = AllPayPrice;
                                    s += AllPayPrice;
                                    row++;
                                });

                                // set итого row
                                ws.Cells[row, ReportColumnConstants.Name] = "ВСЕГО";
                                ws.Cells[row, ReportColumnConstants.PricePayForSize] = s;
                                priceKamMaterial += s;
                                range = ws.get_Range("B" + row, "I" + row);
                                range.Font.Bold = true;
                                //row++;
                            }

                            ws.Cells[++row, ReportColumnConstants.Name] = "Итого камеральные работы";
                            ws.Cells[row, ReportColumnConstants.PricePayForSize] = priceKamMaterial;
                            range = ws.get_Range("B" + row, "I" + row);
                            range.Font.Bold = true;
                        }
                        #endregion

                        ws.Cells[++row, ReportColumnConstants.Name] = "Всего материалы";
                        ws.Cells[row, ReportColumnConstants.PricePayForSize] = pricePolMaterial + priceLabMaterial + priceKamMaterial;
                        range = ws.get_Range("B" + row, "I" + row);
                        range.Font.Bold = true;

                        #region styles
                        //range = ws.get_Range("C" + begin_row, "C" + row);
                        //range.NumberFormat = "#,##0";
                        //range.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        //range = ws.get_Range("D" + begin_row, "D" + row);
                        //range.NumberFormat = "#,##0";
                        range = ws.get_Range("E" + begin_row, "E" + row);
                        range.NumberFormat = "#,##0.00";
                        range = ws.get_Range("F" + begin_row, "F" + row);
                        range.NumberFormat = "";
                        //range = ws.get_Range("G" + begin_row, "G" + row);
                        //range.NumberFormat = "0";
                        //range = ws.get_Range("H" + begin_row, "H" + row);
                        //range.NumberFormat = "#,##0";
                        //range = ws.get_Range("I" + begin_row, "I" + row);
                        //range.NumberFormat = "#,##0";
                        #endregion

                        row++;
                        #endregion

                        #region Коэффициенты
                        number = 0;
                        ws.Cells[++row, 1] = "Коэффициенты";
                        range = ws.get_Range("A" + row, "I" + row);
                        range.Merge(Type.Missing);
                        range.WrapText = true;
                        range.Font.Bold = true;
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                        double koef = 1;
                        double kk;
                        double? AllPricePol = pricePolPay + pricePolPribor + pricePolMaterial;

                        range = ws.get_Range("B" + (row + 1), "H" + (row + 1));
                        range.WrapText = false;
                        ws.Cells[++row, ReportColumnConstants.Name] = "При выполнении изысканий в неблагоприятный период года к трудозатратам на полевые работы применяются коэффициенты";
                        kk = ReportContext.UnfavourableKoef;
                        koef *= kk;
                        ws.Cells[row, ReportColumnConstants.PricePayForSize] = kk;
                        range.Merge(Type.Missing);

                        range = ws.get_Range("B" + (row + 1), "H" + (row + 1));
                        range.WrapText = false;
                        ws.Cells[++row, ReportColumnConstants.Name] = "Средняя температура воздуха, ˚С";
                        kk = ReportContext.TemperatureKoef;
                        koef *= kk;
                        ws.Cells[row, ReportColumnConstants.PricePayForSize] = ReportContext.TemperatureKoef;
                        range.Merge(Type.Missing);

                        range = ws.get_Range("B" + (row + 1), "H" + (row + 1));
                        range.WrapText = false;
                        ws.Cells[++row, ReportColumnConstants.Name] = "Затраты по внутреннему транспорту";
                        kk = ReportContext.InternalKoef((double)AllPricePol);
                        koef *= kk;
                        ws.Cells[row, ReportColumnConstants.PricePayForSize] = kk;
                        range.Merge(Type.Missing);

                        range = ws.get_Range("B" + (row + 1), "H" + (row + 1));
                        range.WrapText = false;
                        ws.Cells[++row, ReportColumnConstants.Name] = "Затраты по внешнему траспорту";
                        kk = ReportContext.ExternalKoef;
                        koef *= kk;
                        ws.Cells[row, ReportColumnConstants.PricePayForSize] = kk;
                        range.Merge(Type.Missing);

                        range = ws.get_Range("B" + (row + 1), "H" + (row + 1));
                        range.WrapText = false;
                        ws.Cells[++row, ReportColumnConstants.Name] = "Затраты по организации и ликвидации изысканий";
                        kk = ReportContext.TypeExploreKoef;
                        koef *= kk;
                        ws.Cells[row, ReportColumnConstants.PricePayForSize] = kk;
                        range.Merge(Type.Missing);

                        range = ws.get_Range("B" + (row + 1), "H" + (row + 1));
                        range.WrapText = false;
                        ws.Cells[++row, ReportColumnConstants.Name] = "Метрология 5%";
                        kk = 1.05;
                        koef *= kk;
                        ws.Cells[row, ReportColumnConstants.PricePayForSize] = kk;
                        range.Merge(Type.Missing);

                        range = ws.get_Range("B" + (row + 1), "H" + (row + 1));
                        range.WrapText = false;
                        ws.Cells[++row, ReportColumnConstants.Name] = "Итого коэффициентов";
                        ws.Cells[row, ReportColumnConstants.PricePayForSize] = koef;
                        range.Merge(Type.Missing);

                        range = ws.get_Range("I" + (row - 5), "I" + row);
                        range.NumberFormat = "";

                        range = ws.get_Range("B" + (row + 1), "H" + (row + 1));
                        range.WrapText = false;
                        ws.Cells[++row, ReportColumnConstants.Name] = "Итого затрат полевых работ";
                        ws.Cells[row, ReportColumnConstants.PricePayForSize] = AllPricePol;
                        range.Merge(Type.Missing);

                        range = ws.get_Range("B" + (row + 1), "H" + (row + 1));
                        range.WrapText = false;
                        ws.Cells[++row, ReportColumnConstants.Name] = "Всего затрат полевых работ";
                        AllPricePol = Math.Round((double)AllPricePol * koef, 2);
                        ws.Cells[row, ReportColumnConstants.PricePayForSize] = AllPricePol;
                        range.Merge(Type.Missing);

                        range = ws.get_Range("B" + (row - 2), "I" + row);
                        range.Font.Bold = true;

                        pricePolPay = Math.Round((double)pricePolPay * koef, 2);
                        pricePolPribor = Math.Round((double)pricePolPribor * koef, 2);
                        pricePolMaterial = Math.Round((double)pricePolMaterial * koef, 2);

                        #endregion

                        #region Всего затрат
                        number = 0;
                        row++;
                        ws.Cells[row, 1] = "Всего затрат";
                        range = ws.get_Range("A" + row, "I" + row);
                        range.Merge(Type.Missing);
                        range.WrapText = true;
                        range.Font.Bold = true;
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                        // set title of worker
                        ws.Cells[++row, ReportColumnConstants.Razryad] = "Зарабатная плата";
                        ws.Cells[row, ReportColumnConstants.PayInMonth] = "Машины, приб. и обор. (амортизация)(А)";
                        ws.Cells[row, ReportColumnConstants.Avarge_Time] = "Материалы(М)";
                        ws.Cells[row, ReportColumnConstants.CountWorkers] = "Итого затрат  (п48+п49+п50) в сум";
                        ws.Cells[row, ReportColumnConstants.PayForHour] = "Прочие расходы п51*" + ReportContext.OtherExpenditure + " %";
                        ws.Cells[row, ReportColumnConstants.PayPercent25] = "Прибыль         (п51+п52)*10% ";
                        ws.Cells[row, ReportColumnConstants.PricePayForSize] = "Стоимость  продукции (п51+п52+п53)";

                        range = ws.get_Range("B" + row, "I" + row);
                        range.WrapText = true;
                        range.Font.Bold = true;
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                        row++;
                        for (int i = 1; i < 10; i++)
                        {
                            ws.Cells[row, i] = i + 45;
                        }
                        range = ws.get_Range("A" + row, "I" + row);
                        range.NumberFormat = "#,##0";
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                        begin_row = row + 1;

                        // Полевые
                        ws.Cells[++row, ReportColumnConstants.Name] = "Полевые";
                        ws.Cells[row, ReportColumnConstants.Razryad] = pricePolPay;
                        ws.Cells[row, ReportColumnConstants.PayInMonth] = pricePolPribor;
                        ws.Cells[row, ReportColumnConstants.Avarge_Time] = pricePolMaterial;

                        // Лабораторные
                        ws.Cells[++row, ReportColumnConstants.Name] = "Лабораторные";
                        ws.Cells[row, ReportColumnConstants.Razryad] = priceLabPay;
                        ws.Cells[row, ReportColumnConstants.PayInMonth] = priceLabPribor;
                        ws.Cells[row, ReportColumnConstants.Avarge_Time] = priceLabMaterial;

                        // Камералные
                        ws.Cells[++row, ReportColumnConstants.Name] = "Камералные";
                        ws.Cells[row, ReportColumnConstants.Razryad] = priceKamPay;
                        ws.Cells[row, ReportColumnConstants.PayInMonth] = priceKamPribor;
                        ws.Cells[row, ReportColumnConstants.Avarge_Time] = priceKamMaterial;

                        // Камералные
                        ws.Cells[++row, ReportColumnConstants.Name] = "Итого";
                        ws.Cells[row, ReportColumnConstants.Razryad] = pricePolPay + priceLabPay + priceKamPay;
                        ws.Cells[row, ReportColumnConstants.PayInMonth] = pricePolPribor + priceLabPribor + priceKamPribor;
                        ws.Cells[row, ReportColumnConstants.Avarge_Time] = pricePolMaterial + priceLabMaterial + priceKamMaterial;
                        double? allprice = pricePolPay + priceLabPay + priceKamPay +
                            pricePolPribor + priceLabPribor + priceKamPribor +
                            pricePolMaterial + priceLabMaterial + priceKamMaterial;
                        ws.Cells[row, ReportColumnConstants.Pribor_Amart_Percent] = allprice;

                        double otherExp = ReportContext.OtherExpenditure / 100;

                        double? allprice21 = Math.Round((double)allprice * otherExp, 2);
                        ws.Cells[row, ReportColumnConstants.PayForHour] = allprice21;

                        allprice += allprice21;

                        double? allprice10 = Math.Round((double)allprice * Constants.Constants.ProfitKoef, 2);
                        ws.Cells[row, ReportColumnConstants.PayPercent25] = allprice10;

                        allprice += allprice10;
                        ws.Cells[row, ReportColumnConstants.PricePayForSize] = allprice;

                        double? allpriceNDS = Math.Round((double)allprice * Constants.Constants.NDSKoef, 2);
                        ws.Cells[++row, ReportColumnConstants.Name] = "НДС 15 %";
                        ws.Cells[row, ReportColumnConstants.All_Price_Currect_Process] = allpriceNDS;
                        allprice += allpriceNDS;
                        range = ws.get_Range("B" + row, "H" + row);
                        range.Merge(Type.Missing);

                        ws.Cells[++row, ReportColumnConstants.Name] = "Всего стоимость";
                        ws.Cells[row, ReportColumnConstants.All_Price_Currect_Process] = allprice;
                        range = ws.get_Range("B" + row, "H" + row);
                        range.Merge(Type.Missing);

                        #region styles
                        range = ws.get_Range("C" + begin_row, "I" + row);
                        range.NumberFormat = "#,##0.00";
                        range = ws.get_Range("B" + begin_row, "I" + row);
                        range.Font.Bold = true;
                        #endregion

                        #endregion

                        #region styling
                        range = ws.get_Range("A" + first_row, "I" + row);
                        // set border weigth = 1 for all cells
                        range.Borders.Weight = 2;
                        // set text wrapping settings on Name cells
                        range = ws.get_Range("B" + first_row, "B" + row);
                        range.WrapText = true;
                        #endregion

                        

                        row += 2;
                        ws.get_Range("B" + row, "I" + row).Merge(Type.Missing);
                        ws.Cells[row, 2] = "*Примечания: (xxxx) - номера работ по РСН";
                        commentaries.ForEach(x =>
                        {
                            row++;
                            range = ws.get_Range("B" + row, "I" + row);
                            ws.Cells[row, 2] = x.Name;
                            range.Merge(Type.Missing);
                            range.WrapText = true;
                        });

                        // footer of report
                        row += 2;
                        ws.get_Range("B" + row, "I" + row).Merge(Type.Missing);
                        ws.Cells[row, ReportColumnConstants.Name] = "Всего:  " +
                            Helper.ToLowerFirstLetter(ConvertNumericalMoneyToTextMoney.NumberToWords((long)allprice));

                        row++;
                        ws.get_Range("B" + row, "I" + row).Merge(Type.Missing);
                        ws.Cells[row, ReportColumnConstants.Name] = "Заместитель генерального директора";

                        row++;
                        ws.get_Range("B" + row, "I" + row).Merge(Type.Missing);
                        ws.Cells[row, ReportColumnConstants.Name] = "Начальник отдела";

                        ws.get_Range("B" + row, "I" + row).Merge(Type.Missing);
                        range = ws.get_Range("B" + (row - 3), "I" + row);
                        range.Font.Bold = true;

                        range = ws.get_Range("A1", "I" + row);
                        range.Font.Name = "Times New Roman";
                        range.Font.Size = 11;

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
                    else
                        MessageBox.Show("Ошибка");
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

        #region Properties change
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public void ChangedPrice()
        {
            //PricePoliviy = Poliviy.Select(x => (double)x.Cost).Sum();
            //PriceKameralniy = Kameralniy.Select(x => (double)x.Cost).Sum();
            //PriceLabortorniy = Laboratorniy.Select(x => (double)x.Cost).Sum();

            //PriceAll = PriceKameralniy + PriceLabortorniy + PricePoliviy;
        }

        #endregion
    }

    class Table
    {
        public int? N { get; set; }
        public string Name { get; set; }
        public int? Type { get; set; }
        public double Koef { get; set; }
        public string Content { get; set; }

    }
}
