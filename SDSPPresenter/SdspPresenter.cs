using SDSPPresenter.Views;
using SDSPServiceImplementation.Exceptions;
using SDSPServiceInterface;
using SDSPServiceInterface.Entities;
using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.IO;
namespace SDSPPresenter.Presenters
{
    public class SdspPresenter
    {
        private ISdspViewingService _sdspViewingService;
        private SdspInformation _sdspInformation;
        public ISdspView View
        {
            private get;
            set;
        }
        public SdspPresenter(ISdspViewingService sdspViewingService)
        {
            this._sdspViewingService = sdspViewingService;
        }

        public bool ConnectToDatabase()
        {
            try
            {
                IEnumerable<Departament> allDepartaments = this._sdspViewingService.GetAllDepartaments();
                IEnumerable<Profile> allProfiles = this._sdspViewingService.GetAllProfiles();

                this.View.SetDepartamentsToShow(allDepartaments);
                this.View.SetProfilesToShow(allProfiles);

                return true;
            }
            catch (LoadException)
            {
                this.View.SetMessageToShow(MessageToShowType.Error, "Невозможно подключится к БД!");
                return false;
            }
        }

        public void UpdateSdspContainers()
        {
            try
            {
                DateTime startDate = this.View.GetStartDate();
                DateTime endDate = this.View.GetEndDate();
                Departament selectedDepartament = this.View.GetSelectedDepartament();
                int profileId = this.View.ProfileID();
                if (selectedDepartament != null)
                {
                    this._sdspInformation = this._sdspViewingService.GetAllSdspInformation(profileId, startDate, endDate, selectedDepartament);
                    this.View.UpdateSdspContainers(this._sdspInformation);
                }
            }
            catch (LoadException)
            {
                this.View.SetMessageToShow(MessageToShowType.Error, "Нет соединения с БД!");
            }
        }
    }
}
