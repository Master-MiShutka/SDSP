using System.Collections.Generic;
using System.Data;
using System.Linq;
using SDSPServiceImplementation;
using SDSPServiceImplementation.Exceptions;
using SDSPServiceImplementation.Repositories.Abstract;
using SDSPServiceInterface.Entities;

namespace SDSPServiceImplementation.Repositories
{
    public class ProfilesRepositoryImp : IProfilesRepository
    {
        private DatabaseModel.AskueEntities _entities;
        public ProfilesRepositoryImp(DatabaseModel.AskueEntities entities)
        {
            this._entities = entities;
        }
        public IEnumerable<Profile> GetAllProfiles()
        {
            IEnumerable<Profile> result;
            try
            {
                result = this.DoGetAllProfiles();
            }
            catch (EntityException)
            {
                throw new LoadException("Unable to load Profiles!");
            }
            return result;
        }
        private IEnumerable<Profile> DoGetAllProfiles()
        {
            List<Profile> list = new List<Profile>();
            foreach (DatabaseModel.Profile current in (IEnumerable<DatabaseModel.Profile>)this._entities.Profile)
            {
                Profile profile = new Profile
                {
                    ID = current.ID,
                    Name = current.Name
                };
                list.Add(profile);
            }
            return list;
        }
    }
}
