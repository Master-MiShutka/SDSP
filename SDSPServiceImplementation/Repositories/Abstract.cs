using SDSPServiceInterface.Entities;
using System;
using System.Collections.Generic;
namespace SDSPServiceImplementation.Repositories.Abstract
{
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
