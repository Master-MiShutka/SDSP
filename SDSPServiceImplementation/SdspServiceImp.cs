using SDSPServiceImplementation.Repositories.Abstract;
using SDSPServiceInterface;
using SDSPServiceInterface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SDSPServiceImplementation
{
    public class SdspServiceImp : ISdspViewingService
    {
        private IDepartamentsRepository _DepartamentsRepository;
        private IProfilesRepository _ProfilesRepository;

        private ISdspContainersRepository _sdspContainersRepository;
        public SdspServiceImp(IDepartamentsRepository DepartamentsRepository, IProfilesRepository ProfilesRepository, ISdspContainersRepository sdspContainersRepository)
        {
            this._DepartamentsRepository = DepartamentsRepository;
            this._ProfilesRepository = ProfilesRepository;
            this._sdspContainersRepository = sdspContainersRepository;
        }
        public SdspInformation GetAllSdspInformation(int profileId, DateTime startdate, DateTime enddate, Departament Departament)
        {
            DateTime firstDate;
            DateTime lastDate;
            this.GetDates(startdate, enddate, out firstDate, out lastDate);
            SdspInformation sdspInformation = new SdspInformation();
            sdspInformation.SdspContainers = this._sdspContainersRepository.GetContainers(Departament, profileId, firstDate, lastDate);
            sdspInformation.Departament = Departament;
            this.FillSdspInformation(sdspInformation, DateTime.Now, firstDate, lastDate);
            return sdspInformation;
        }
        private void FillSdspInformation(SdspInformation information, DateTime reportingDate, DateTime previousDate, DateTime nextDate)
        {
            information.StartDate = previousDate;
            information.EndDate = nextDate;
            information.ReportingDate = reportingDate;
            information.AccountPointsCount = this.GetCountersCount(information.SdspContainers);
            information.AnsweringModemsCount = information.SdspContainers.Count((SdspContainer s) => s.Status == "OK" || s.Status == "Счетчики не отвечают");
            information.NotAnsweringModemsCount = information.SdspContainers.Count((SdspContainer s) => s.Status != "OK" && s.Status != "Счетчики не отвечают");
            IEnumerable<Counter> source = this.GetCountersFromContainers(information.SdspContainers).ToArray<Counter>();
            information.AnsweringCountersCount = source.Count((Counter c) => c.Status == "OK");
            information.NotAnsweringCountersCount = source.Count((Counter c) => c.Status != "OK");
            information.PreviousSumm = source.Sum((Counter c) => (c.PreviousIndicationsDifference.HasValue && !float.IsNaN(c.PreviousIndicationsDifference.Value)) ? c.PreviousIndicationsDifference.Value : 0f);
            information.ReportingSumm = source.Sum((Counter c) => (c.IndicationsDifference.HasValue && !float.IsNaN(c.IndicationsDifference.Value)) ? c.IndicationsDifference.Value : 0f);
        }
        private int GetCountersCount(IEnumerable<SdspContainer> sdspContainers)
        {
            int num = 0;
            foreach (SdspContainer current in sdspContainers)
            {
                num += current.Counters.Count<Counter>();
            }
            return num;
        }
        private IEnumerable<Counter> GetCountersFromContainers(IEnumerable<SdspContainer> sdspContainers)
        {
            List<Counter> list = new List<Counter>();
            foreach (SdspContainer current in sdspContainers)
            {
                list.AddRange(current.Counters);
            }
            return list;
        }
        
        private void GetDates(DateTime startDate, DateTime endDate, out DateTime firstDate, out DateTime lastDate)
        {
            firstDate = startDate;
            lastDate = endDate;
        }
        
        public IEnumerable<Departament> GetAllDepartaments()
        {
            return this._DepartamentsRepository.GetAllDepartaments();
        }

        public IEnumerable<Profile> GetAllProfiles()
        {
            return this._ProfilesRepository.GetAllProfiles();
        }
    }
}
