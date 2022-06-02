using System;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Reflection;


using SDSPPresenter.Presenters;
using SDSPPresenter.Views;

using Shared;
using Shared.Commands;

namespace SDSP
{
    public partial class MainWindow : Window, ISdspView, IView<SdspPresenter>
    {
        private ICommand _commandConnectToDb;

        private ICommand _commandExportData;

        public ICommand CommandConnectToDb
        {
            get
            {
                if (_commandConnectToDb == null)
                {
                    _commandConnectToDb = new RelayCommand(p => OnConnectToDb());
                }

                return _commandConnectToDb;
            }
        }

        public ICommand CommandExportData
        {
            get
            {
                if (_commandExportData == null)
                {
                    _commandExportData = new RelayCommand(p => OnExportData(p), p => CanExportData);
                }

                return _commandExportData;
            }
        }

        public static readonly DependencyProperty IsConnectedProperty =
        DependencyProperty.Register("IsConnected", typeof(bool), typeof(MainWindow),
                 new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsArrange));

        public static readonly DependencyProperty IsNotConnectedProperty =
        DependencyProperty.Register("IsNotConnected", typeof(bool), typeof(MainWindow),
                 new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsArrange));

        public bool IsConnected
        {
            get { return (bool)GetValue(IsConnectedProperty); }
            set { SetValue(IsConnectedProperty, value); SetValue(IsNotConnectedProperty, !value); }
        }

        public bool IsNotConnected
        {
            get { return !IsConnected; }
        }

        private void OnConnectToDb()
        {
            try
            {
                this.Cursor = Cursors.Wait;

                if (!App.ConnectToService()) return;

                SdspPresenter sdspPresenter = App.Presenter();
                this._sdspPresenter = sdspPresenter;
                this._sdspPresenter.View = this;
                IsConnected = this._sdspPresenter.ConnectToDatabase();
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private void OnExportData(object parameter)
        {
            if (this._sdspPresenter == null)
            {
                //this.SetMessageToShow(MessageToShowType.Info, "Но ведь данных нет!");
                //return;
            }

            string reportFolderPath = Path.Combine(this.GetStartupPath(), "Отчёты");
            if (!Directory.Exists(reportFolderPath))
                try
                {
                    Directory.CreateDirectory(reportFolderPath);
                }
                catch (IOException)
                {
                    this.SetMessageToShow(MessageToShowType.Error, "Создание отчёта прервано!\nНе удалось создать папку для отчётов.\n" +
                                        "Проверьте имеются ли права на запись в папке:\n\t" + reportFolderPath);
                    return;
                }
            string reportFileName = string.Format("Отчет {0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString().Replace(".", "-").Replace(":", "-"));
            reportFileName = Path.Combine(reportFolderPath, reportFileName) + ".xlsx";

            if (! Directory.Exists(reportFolderPath)) Directory.CreateDirectory(reportFolderPath);

            try
            {
                this.Cursor = Cursors.Wait;
                Shared.Report.ReportExporter.ReportToExcel(this.report, reportFileName);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
                if (MessageBox.Show("Отчет сформирован.\nОткрыть его?", "Экспорт данных", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    System.Diagnostics.Process.Start(reportFileName);
            }
        }

        private bool CanExportData
        {
            get
            {
                return IsConnected;
            }
        }
    }
}
