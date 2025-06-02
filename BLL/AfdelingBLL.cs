using System.Collections.Generic;
using Tidsregistrering.DTO.Models;
using Tidsregistrering.DAL.Repository;


namespace Tidsregistrering.BLL
{
    public class AfdelingBLL
    {
        public AfdelingDTO GetAfdeling(int id)
        {
            if (id <= 0) return null;

            return AfdelingRepository.GetAfdeling(id);
        }

        public int CreateAfdeling(AfdelingDTO afdeling)
        {
            return AfdelingRepository.AddAfdeling(afdeling);
        }

        public List<AfdelingDTO> GetAllAfdelinger()
        {
            return AfdelingRepository.GetAllAfdelinger();
        }
    }
}
