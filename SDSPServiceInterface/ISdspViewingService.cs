using SDSPServiceInterface.Entities;
using System;
using System.Collections.Generic;
namespace SDSPServiceInterface
{
    public interface ISdspViewingService
    {
        SdspInformation GetAllSdspInformation(int profileId, DateTime startdate, DateTime enddate, Departament Departament);
        
        IEnumerable<Departament> GetAllDepartaments();
        IEnumerable<Profile> GetAllProfiles();
    }
}
