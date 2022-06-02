using System.Collections.Generic;
using System.Data;
using System.Linq;
using SDSPServiceImplementation.DatabaseModel;
using SDSPServiceImplementation.Exceptions;
using SDSPServiceImplementation.Repositories.Abstract;
using SDSPServiceInterface.Entities;

namespace SDSPServiceImplementation.Repositories
{
    public class DepartamentsRepositoryImp : IDepartamentsRepository
    {
        private AskueEntities _entities;
        public DepartamentsRepositoryImp(AskueEntities entities)
        {
            this._entities = entities;
        }
        public IEnumerable<Departament> GetAllDepartaments()
        {
            IEnumerable<Departament> result;
            try
            {
                result = this.DoGetAllDepartaments();
            }
            catch (EntityException)
            {
                throw new LoadException("Unable to load Departaments!");
            }
            return result;
        }
        private IEnumerable<Departament> DoGetAllDepartaments()
        {
            List<Departament> list = new List<Departament>();
            foreach (Regions current in (IEnumerable<Regions>)this._entities.Regions)
            {
                Departament Departament = new Departament
                {
                    Id = current.ID,
                    Name = current.Name,
                    DepartamentType = DepartamentType.Region
                };
                this.AddFesesToRegion(Departament);
                list.Add(Departament);
            }
            return list;
        }
        private void AddFesesToRegion(Departament parentDepartament)
        {
            List<Departament> list = new List<Departament>();
            IQueryable<FES> queryable =
                from f in this._entities.FES
                where f.Region_ID == (int?)parentDepartament.Id
                select f;
            foreach (FES current in queryable)
            {
                Departament Departament = new Departament
                {
                    Id = current.ID,
                    Name = current.Name,
                    DepartamentType = DepartamentType.Fes
                };
                this.AddResesToFes(Departament);
                list.Add(Departament);
            }
            parentDepartament.ChildDepartaments = list;
        }
        private void AddResesToFes(Departament parentDepartament)
        {
            List<Departament> list = new List<Departament>();
            IQueryable<RES> queryable =
                from r in this._entities.RES
                where r.Fes_ID == (int?)parentDepartament.Id
                select r;
            foreach (RES current in queryable)
            {
                Departament item = new Departament
                {
                    Id = current.ID,
                    Name = current.Name,
                    DepartamentType = DepartamentType.Res
                };
                list.Add(item);
            }
            parentDepartament.ChildDepartaments = list;
        }
    }
}
