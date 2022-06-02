using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Data.EntityClient;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows.Threading;

using SDSPPresenter.Presenters;
using SDSPServiceImplementation;
using SDSPServiceImplementation.DatabaseModel;
using SDSPServiceImplementation.Repositories.Abstract;
using SDSPServiceImplementation.Repositories;
using SDSPServiceInterface;

[assembly: CLSCompliant(true)]
namespace SDSP
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static string AppName = "Просмотр данных СДСП";

        private static IDepartamentsRepository dr = null;
        private static IProfilesRepository pr = null;
        private static ISdspContainersRepository cr = null;
        private static ISdspViewingService vs = null;
        
        public static SdspPresenter Presenter()
        {
            return new SdspPresenter(vs);
        }

        public static bool ConnectToService()
        {
            //EntityConnection entityConnection = new EntityConnection("name=AskueEntities");
            //EntityConnection entityConnection = new EntityConnection();

            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.Provider = "System.Data.SqlClient";
            entityBuilder.ProviderConnectionString = String.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};MultipleActiveResultSets=True;Connect Timeout={4}",
                SDSP.Properties.Settings.Default.ServerAddress + @"\" + SDSP.Properties.Settings.Default.ServerCatalog,
                SDSP.Properties.Settings.Default.DBName,
                SDSP.Properties.Settings.Default.UserName,
                SDSP.Properties.Settings.Default.UserPassword,
                SDSP.Properties.Settings.Default.ConnectTimeout);
            entityBuilder.Metadata = @"res://*/SDSPServiceImplementation.DatabaseModel.AskueModel.csdl|" +
                                     @"res://*/SDSPServiceImplementation.DatabaseModel.AskueModel.ssdl|" +
                                     @"res://*/SDSPServiceImplementation.DatabaseModel.AskueModel.msl";

            EntityConnection entityConnection = new EntityConnection(entityBuilder.ToString());

            try
            {
                entityConnection.Open();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(String.Format("Произошла ошибка:\n{0}\n\nОписание:\n{1}", Ex.ToString(), Ex.Message), 
                    AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            AskueEntities context = new AskueEntities(entityConnection);
            try
            {
                bool databaseExists = context.DatabaseExists();
                if (!databaseExists)
                {
                    entityConnection.Dispose();                    
                    MessageBox.Show("Не удалось найти базу данных на сервере.", AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (Exception)
            {
                entityConnection.Dispose();
                MessageBox.Show("Не удалось соединиться.", AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }                       

            dr = new DepartamentsRepositoryImp(context);
            pr = new ProfilesRepositoryImp(context);
            cr = new SdspContainersRepositoryImp(context);
            vs = new SdspServiceImp(dr, pr, cr);
            return true;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Process ThisProcess = Process.GetCurrentProcess();
			Process[] SameProcesses = Process.GetProcessesByName(ThisProcess.ProcessName);
            if (SameProcesses.Length > 1) // уже запущена
            {
                MessageBox.Show("Другой экземпляр программы уже запущен.", AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// Event handler for when the application crashes due to an unhandled exception
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            U.L(LogLevel.Error, "APP", "Аварийное завершение: " + e.Exception.Message);
            U.L(LogLevel.Error, "APP", e.Exception.StackTrace);
            U.L(LogLevel.Error, "APP", e.Exception.Source);
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            SDSP.Properties.Settings.Default.Save();
        }
    }
}
