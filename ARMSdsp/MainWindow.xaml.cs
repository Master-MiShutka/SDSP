using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Xml;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using SDSPPresenter.Presenters;
using SDSPPresenter.Views;
using SDSPServiceInterface.Entities;

using System.Xml.Serialization;
using System.IO;

using GUIControls;

namespace SDSP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISdspView, IView<SdspPresenter>
    {        
        private SdspPresenter _sdspPresenter;

        public SdspPresenter Presenter
        {
            get { return _sdspPresenter; }
        }

        public MainWindow()
        {
            //
            this.DataContext = this;
            InitializeComponent();
            this.datePickerStart.SelectedDate = new DateTime?(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
            this.datePickerEnd.SelectedDate = new DateTime?(DateTime.Now);
            //
            this.DepartamentsTree.SelectedItemChanged += new RoutedEventHandler(DepartamentsTree_SelectedItemChanged);
        }

        /// <summary>
        /// Получение даты начала периода
        /// </summary>
        /// <returns>Начальная дата</returns>
        public DateTime GetStartDate()
        {
            if (this.datePickerStart.SelectedDate == null) return DateTime.Now;
            return this.datePickerStart.SelectedDate.Value;
        }

        /// <summary>
        /// Получение даты окончания периода
        /// </summary>
        /// <returns>Конечная дата</returns>
        public DateTime GetEndDate()
        {
            if (this.datePickerEnd.SelectedDate == null) return DateTime.Now;
            return this.datePickerEnd.SelectedDate.Value;
        }

        public int ProfileID()
        {
            return cmbProfiles.SelectedIndex == -1 ? 0 : cmbProfiles.SelectedIndex+1;
        }

        /// <summary>
        /// Обновление информации
        /// </summary>
        /// <param name="sdspInformation"></param>
        public void UpdateSdspContainers(SdspInformation sdspInformation)
        {
            if (this.IsConnected)
            {
                try
                {
                    this.Cursor = Cursors.Wait;
                    CreateReport(sdspInformation);
                }
                finally
                {
                    this.Cursor = Cursors.Arrow;
                }
            }
        }

        /// <summary>
        /// Заполнение списка профилей
        /// </summary>
        /// <param name="Profiles"></param>
        public void SetProfilesToShow(IEnumerable<Profile> Profiles)
        {
            // заполняем
            cmbProfiles.ItemsSource = Profiles;
            cmbProfiles.DisplayMemberPath = "Name";
            //
            if (cmbProfiles.Items.Count > 0)
                cmbProfiles.SelectedIndex = 1;
            else
                cmbProfiles.SelectedIndex = 0;
        }

        /// <summary>
        /// Заполнение дерева департаментов
        /// </summary>
        /// <param name="Departaments">Список департаментов</param>
        public void SetDepartamentsToShow(IEnumerable<Departament> Departaments)
        {            
            this.DepartamentsTree.Tree.Items.Clear();
            this.AddDepartamentsToParentItems(this.DepartamentsTree.Tree.Items, Departaments);
        }

        /// <summary>
        /// Возращает путь к папке программы
        /// </summary>
        /// <returns>Абсолютный путь к папке с программой</returns>
        public string GetStartupPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// Подсчёт общего количества счётчиков
        /// </summary>
        /// <param name="containers">Список систем</param>
        /// <returns>Общее количество счётчиков</returns>
        private int GetCountersCount(IEnumerable<SdspContainer> containers)
        {
            int num = 0;
            foreach (SdspContainer current in containers)
            {
                num += current.Counters.Count<Counter>();
            }
            return num;
        }

        /// <summary>
        /// Возвращает выбранный в дереве департамент
        /// </summary>
        /// <returns>Текущий отдел</returns>
        public Departament GetSelectedDepartament()
        {
            TreeViewItem treeViewItem = (TreeViewItem)this.DepartamentsTree.Tree.SelectedItem;
            Departament result;
            if (treeViewItem == null)
            {
                result = null;
            }
            else
            {
                string value = treeViewItem.Name.Substring(treeViewItem.Name.IndexOf('_') + 1);
                DepartamentType DepartamentType = this.StringToDepartamentType(treeViewItem.Name.Substring(0, treeViewItem.Name.IndexOf('_')));
                result = new Departament
                {
                    Id = Convert.ToInt32(value),
                    Name = (string)treeViewItem.Header,
                    DepartamentType = DepartamentType
                };
            }
            return result;
        }

        private string DepartamentTypeToString(DepartamentType DepartamentType)
        {
            string result;
            switch (DepartamentType)
            {
                case DepartamentType.Region:
                    result = "Region";
                    break;
                case DepartamentType.Fes:
                    result = "Fes";
                    break;
                case DepartamentType.Res:
                    result = "Res";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("DepartamentType");
            }
            return result;
        }

        private DepartamentType StringToDepartamentType(string departamentType)
        {
            DepartamentType result;
            switch (departamentType)
            {
                case "Region":
                    result = DepartamentType.Region;
                    break;
                case "Fes":
                    result = DepartamentType.Fes;
                    break;
                case "Res":
                    result = DepartamentType.Res;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("DepartamentType");
            }
            return result;
        }

        /// <summary>
        /// Возвращает список счётчиков указанного контейнера
        /// </summary>
        /// <param name="containers">Контейнер</param>
        /// <returns>Список счётчиков</returns>
        private IEnumerable<Counter> GetAllCountersFromContainers(IEnumerable<SdspContainer> containers)
        {
            List<Counter> list = new List<Counter>();
            foreach (SdspContainer current in containers)
            {
                list.AddRange(current.Counters);
            }
            return list;
        }

        /// <summary>
        /// Отображает окно сообщения
        /// </summary>
        /// <param name="messageType">Тип сообщения</param>
        /// <param name="message">Сообщение</param>
        public void SetMessageToShow(MessageToShowType messageType, string message)
        {
            switch (messageType)
            {
                case MessageToShowType.Error:
                    MessageBox.Show(message, App.Current.MainWindow.Title, MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case MessageToShowType.Info:
                    MessageBox.Show(message, App.Current.MainWindow.Title, MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case MessageToShowType.Warning:
                    MessageBox.Show(message, App.Current.MainWindow.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case MessageToShowType.None:
                    MessageBox.Show(message, App.Current.MainWindow.Title);
                    break;
                default:
                    MessageBox.Show(message);
                    break;
            }
        }

        /// <summary>
        /// Добавляет дерево отделов в TreeView
        /// </summary>
        /// <param name="items">Корневой элемент дерева для добавления данных</param>
        /// <param name="Departaments">Иерарический список департаментов</param>
        private void AddDepartamentsToParentItems(ItemCollection items, IEnumerable<Departament> Departaments)
        {
            foreach (Departament current in Departaments)
            {
                TreeViewItem treeViewItem = new TreeViewItem
                {
                    Name = this.DepartamentTypeToString(current.DepartamentType) + "_" + current.Id.ToString(),
                    Header = this.CutString(current.Name, 100)
                };
                treeViewItem.IsExpanded = true;
                //treeViewItem.Collapsed += new RoutedEventHandler(this.item_Collapsed);
                items.Add(treeViewItem);
                if (current.ChildDepartaments != null && current.ChildDepartaments.Any<Departament>())
                {
                    this.AddDepartamentsToParentItems(treeViewItem.Items, current.ChildDepartaments);
                }
            }
        }

        /// <summary>
        /// Возвращает указанное количество символов из строки
        /// </summary>
        /// <param name="source">Строка</param>
        /// <param name="maxSize">Количество символов</param>
        /// <returns>Подстрока</returns>
        private string CutString(string source, int maxSize)
        {
            string result;
            if (source.Length > maxSize)
            {
                result = source.Remove(maxSize);
            }
            else
            {
                result = source;
            }
            return result;
        }

        /// <summary>
        /// Обработчик события при сворачивании элемента дерева
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void item_Collapsed(object sender, RoutedEventArgs e)
        {
            // не даем свернуть - разворачиваем
            (sender as TreeViewItem).IsExpanded = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddTipWhenTableIsEmpty();
            //
            MakeMenu();
        }

        private void cmbProfiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._sdspPresenter.UpdateSdspContainers();
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.DatePeriod.Text = String.Format("с {0} по {1}", GetStartDate().ToShortDateString(), GetEndDate().ToShortDateString());
            //
            if (this._sdspPresenter == null | this.datePickerEnd.SelectedDate == null | this.datePickerStart.SelectedDate == null) return;
            this.datePickerStart.IsDropDownOpen = false;
            this.datePickerEnd.IsDropDownOpen = false;
            this._sdspPresenter.UpdateSdspContainers();
        }

        private void DepartamentsTree_SelectedItemChanged(object sender, RoutedEventArgs e)
        {
            this._sdspPresenter.UpdateSdspContainers();
        }

        //Function to get random number
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SdspInformation inf = new SdspInformation();

            inf.AccountPointsCount = 23;
            inf.AnsweringCountersCount = 74;
            inf.AnsweringModemsCount = 45;
            inf.NotAnsweringCountersCount = 14;
            inf.NotAnsweringModemsCount = 4;

            inf.PreviousSumm = 7123354.23f;
            inf.ReportingSumm = 6865617.7f;

            inf.ReportingDate = DateTime.Now;

            inf.StartDate = new DateTime(2013, 02, 23);
            inf.EndDate = new DateTime(2013, 03, 1);

            inf.Departament = new Departament();
            inf.Departament.DepartamentType = DepartamentType.Fes;
            inf.Departament.Id = 2;
            inf.Departament.Name = "Ошмянские ЭС";

            Departament[] departaments = new Departament[4];

            string[] ress = new string[4] { "Ивьевский РЭС", "Островецкий РЭС", "Ошмянский РЭС", "Сморгонский РЭС" };

            for (int i = 0; i < 4; i++)
            {
                Departament d = new Departament();
                d.DepartamentType = DepartamentType.Res;
                d.Id = i + 1;
                d.Name = ress[i];
                d.ChildDepartaments = null;
                departaments[i] = d;
            }

            inf.Departament.ChildDepartaments = departaments.ToArray<Departament>();

            string[] dialnumber = new string[5] { "80447909157", "80335546243", "80235546243", "80295746243", "8023695874" };
            string[] streets = new string[5] { "СПК «Трабы», Мехсектор д. Трабы", "СПК «Трабы», ферма Сурвилишки", "СПК им.Баума, Мастерские Геранены", "СПК им. Баума, Комплекс д. Геранены ", "ЧСУП «Азот Агро», коровник Барово" };
            string[] tpnames = new string[5] { "ВЛ-10 кВ №167, ЗТП №826", "ВЛ-10 кВ №170, ЗТП №646", "ВЛ-10 кВ №240, ЗТП №182", "ВЛ-10 кВ №240, ЗТП №614", " ВЛ-10 кВ №355, ЗТП №78" };

            string[] counterTypes = new string[4] { "CC-301", "СE301BY", "ЦЭ6822", "ЦЭ6822Б" };

            Random rnd = new Random((int)DateTime.Now.Ticks);

            int sdsp = rnd.Next(15, 70);

            SdspContainer[] sc = new SdspContainer[sdsp];
            for (int i = 0; i < sdsp; i++)
            {
                rnd = new Random((int)DateTime.Now.Ticks);
                
                SdspContainer container = new SdspContainer();
                container.DialNumber = dialnumber[RandomNumber(0, 5)];
                container.LastSession = new DateTime(RandomNumber(2000, 2013), RandomNumber(1, 12), RandomNumber(1, 28));
                container.Street = streets[RandomNumber(0, 5)];
                container.TPName = tpnames[RandomNumber(0, 5)];
                container.OrderNumber = i + 1;
                container.Status = "Счетчики не отвечают";

                int Count = RandomNumber(1, 5);
                Counter[] counters = new Counter[Count];

                for (int j = 1; j <= Count; j++)
                {
                    rnd = new Random((int)DateTime.Now.Ticks);

                    Counter counter = new Counter();
                    counter.Container = container;

                    counter.Status = (RandomNumber(1, 1000) % 3) == 0 ? "Не отвечает" : "OK";

                    if (counter.Status == "OK")
                    {
                        counter.StartIndications = (float)Math.Abs(Math.Sin(rnd.NextDouble() * RandomNumber(1, 10))) * 1000.0f;
                        counter.IndicationsDifference = (float)Math.Abs(Math.Sin(rnd.NextDouble()) * RandomNumber(1, 5)) * 1000.0f;
                        counter.PreviousIndicationsDifference = (float)Math.Abs(Math.Cos(rnd.NextDouble() * 3)) * RandomNumber(1, 1000) / 100 * 1000.0f;
                        counter.EndIndications = counter.StartIndications + counter.IndicationsDifference;
                    }
                    else
                    {
                        counter.StartIndications = null;
                        counter.EndIndications = null;
                        counter.IndicationsDifference = null;
                        counter.PreviousIndicationsDifference = null;
                    }
                    counter.Name = "Т-" + j.ToString();
                    counter.Type = counterTypes[RandomNumber(0, 3)];
                    counter.NetworkAdress = j.ToString();

                    counter.SerialNumber = RandomNumber(100, 999).ToString() + "F1" + RandomNumber(100, 999).ToString();

                    counters[j - 1] = counter;
                }

                container.Counters = counters.ToArray<Counter>();

                sc[i] = container;
                
            }

            inf.SdspContainers = sc.ToArray<SdspContainer>();

            UpdateSdspContainers(inf);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            button1_Click(null, null);
        }

    }
}