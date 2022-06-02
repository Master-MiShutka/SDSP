using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SDSPServiceImplementation.DatabaseModel;
using SDSPServiceImplementation.Exceptions;
using SDSPServiceImplementation.Repositories.Abstract;
using SDSPServiceImplementation.Repositories.Converters;
using SDSPServiceImplementation.Repositories.DataEntities;
using SDSPServiceInterface.Entities;

namespace SDSPServiceImplementation.Repositories
{
    public class SdspContainersRepositoryImp : ISdspContainersRepository
    {
        private AskueEntities _entities;
        public SdspContainersRepositoryImp(AskueEntities entities)
        {
            this._entities = entities;
        }
        public IEnumerable<SdspContainer> GetContainers(Departament Departament, int profileId, DateTime firstDate, DateTime lastDate)
        {
            IEnumerable<SdspContainer> result;
            try
            {
                result = this.DoGetSdspContainers(Departament, profileId, firstDate, lastDate);
            }
            catch (EntityException)
            {
                throw new LoadException("Unable to get containers!");
            }
            return result;
        }
        private IEnumerable<SdspContainer> DoGetSdspContainers(Departament Departament, int profileId, DateTime firstDate, DateTime lastDate)
        {
            IEnumerable<Houses> housesFromDepartament = this.GetHousesFromDepartament(Departament);
            IEnumerable<SdspContainer> containersFromHouses = this.GetContainersFromHouses(housesFromDepartament, profileId, firstDate, lastDate);
            SdspContainer[] array = (containersFromHouses as SdspContainer[]) ?? containersFromHouses.ToArray<SdspContainer>();
            this.MarkContainerNumbers(array);
            return array;
        }
        private void MarkContainerNumbers(IEnumerable<SdspContainer> containers)
        {
            for (int i = 0; i < containers.Count<SdspContainer>(); i++)
            {
                containers.ElementAt(i).OrderNumber = i + 1;
            }
        }
        private IEnumerable<Houses> GetHousesFromDepartament(Departament Departament)
        {
            IEnumerable<Houses> result;
            switch (Departament.DepartamentType)
            {
                case DepartamentType.Region:
                    result =
                        from h in this._entities.Houses
                        where h.Streets.Places.RES.FES.Region_ID == (int?)Departament.Id
                        select h;
                    break;
                case DepartamentType.Fes:
                    result =
                        from h in this._entities.Houses
                        where h.Streets.Places.RES.Fes_ID == (int?)Departament.Id
                        select h;
                    break;
                case DepartamentType.Res:
                    result =
                        from h in this._entities.Houses
                        where h.Streets.Places.Res_ID == (int?)Departament.Id
                        select h;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return result;
        }
        private IEnumerable<SdspContainer> GetContainersFromHouses(IEnumerable<Houses> houses, int profileId, DateTime firstDate, DateTime lastDate)
        {
            IEnumerable<AccountPoint> accountPoints = this.GetAccountPointsFromHouses(houses).ToList<AccountPoint>();
            IEnumerable<Indications> indicationsByPrevAndNextDate = this.GetIndicationsByPrevAndNextDate(profileId, firstDate, lastDate);
            return new SdspContainersConverter().ConvertFromIndicationsAndAccountPoints(accountPoints, indicationsByPrevAndNextDate, firstDate, lastDate);
        }
        private IEnumerable<Indications> GetIndicationsByPrevAndNextDate(int profileId, DateTime firstDate, DateTime lastDate)
        {
            return
                from ind in this._entities.Indications
                where ind.Profile_ID == (int?)profileId
                where ind.Ts.Value.Equals(firstDate) || ind.Ts.Value.Equals(lastDate)
                select ind;
        }
        private IEnumerable<AccountPoint> GetAccountPointsFromHouses(IEnumerable<Houses> house)
        {
            return
                from Collector in this._entities.Collectors
                join CalcPoint in this._entities.CalcPoints on (int?)Collector.ID equals CalcPoint.Collector_id
                join f in this._entities.Flats on CalcPoint.Flat_id equals (int?)f.ID
                join h in this._entities.Houses on f.House_ID equals (int?)h.ID
                join ch in this._entities.Channels on Collector.Channel_ID equals (int?)ch.ID
                join st in this._entities.Collectors_Statuses on (int?)Collector.ID equals st.Collector_id into subclst
                from subclstat in subclst.DefaultIfEmpty<Collectors_Statuses>()
                join cnt in this._entities.Counters on (int?)CalcPoint.ID equals cnt.CalcPoint_ID
                join str in this._entities.Streets on h.Street_ID equals (int?)str.ID
                join cst in this._entities.Counters_Statuses on (int?)cnt.ID equals cst.Counter_id into subst
                from substat in subst.DefaultIfEmpty<Counters_Statuses>()
                where house.Contains(h)
                select new AccountPoint
                {
                    ColId = Collector.ID,
                    CalcId = CalcPoint.ID,
                    CounterName = f.Name,
                    CounterTypeName = cnt.Counters_type.Name,
                    TpName = h.Name,
                    DialNumber = ch.Name,
                    NetworkAdress = cnt.NetAdress,
                    ColStatus = subclstat.code,
                    LastSession = subclstat.time,
                    CounterStatus = substat.code,
                    Street = str.Name,
                    CounterSerial = cnt.Serial
                };
        }
    }
}
