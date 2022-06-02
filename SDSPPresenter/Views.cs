using SDSPPresenter.Presenters;
using SDSPServiceInterface.Entities;
using System;
using System.Collections.Generic;
namespace SDSPPresenter.Views
{
    public enum MessageToShowType
    {
        Error,
        Warning,
        Info,
        None
    }
    
    public interface ISdspView : IView<SdspPresenter>
    {
        /// <summary>
        /// Отображение списка департаментов
        /// </summary>
        /// <param name="Departaments">Департамент</param>
        void SetDepartamentsToShow(IEnumerable<Departament> Departaments);
        /// <summary>
        /// Возвращает ID выбранного профиля
        /// </summary>
        /// <returns></returns>
        void SetProfilesToShow(IEnumerable<Profile> Profiles);
        int ProfileID();
        /// <summary>
        /// Возвращает начальную дату периода
        /// </summary>
        /// <returns></returns>
        DateTime GetStartDate();
        /// <summary>
        /// Возвращает конечную дату периода
        /// </summary>
        /// <returns></returns>
        DateTime GetEndDate();
        /// <summary>
        /// Возвращает выбранный департамент
        /// </summary>
        /// <returns></returns>
        Departament GetSelectedDepartament();
        /// <summary>
        /// Разбор и отображение результатов
        /// </summary>
        /// <param name="sdspInformation"></param>
        void UpdateSdspContainers(SdspInformation sdspInformation);
        /// <summary>
        /// Возвращает путь к исполняемому файлу приложения
        /// </summary>
        /// <returns></returns>
        string GetStartupPath();
        /// <summary>
        /// Отображает окно сообщения
        /// </summary>
        /// <param name="messageType">Тип сообщения</param>
        /// <param name="message">Сообщение</param>
        void SetMessageToShow(MessageToShowType messageType, string message);
    }

    public interface IView<out TPresenter>
    {
        //TPresenter GetPresenter();
    }

    public interface ISdspViewing
    {
        SdspInformation GetAllSdspInformation(int profileId, DateTime startdate, DateTime enddate, Departament Departament);

        IEnumerable<Departament> GetAllDepartaments();
        IEnumerable<Profile> GetAllProfiles();
    }

    public interface IDepartamentsRepository
    {
        IEnumerable<Departament> GetAllDepartaments();
    }

    public interface IProfilesRepository
    {
        IEnumerable<Profile> GetAllProfiles();
    }

    public interface ISdspContainersRepository
    {
        IEnumerable<SdspContainer> GetContainers(Departament Departament, int profileId, DateTime firstDate, DateTime lastDate);
    }
}