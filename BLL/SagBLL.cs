using System.Collections.Generic;
using Tidsregistrering.DTO.Models;
using Tidsregistrering.DAL.Repository;

namespace Tidsregistrering.BLL
{
    public class SagBLL
    {
        public SagDTO GetSag(int id)
        {
            if (id <= 0) return null;
            return SagRepository.GetSag(id);
        }

        public int CreateSag(SagDTO sag)
        {
            return SagRepository.AddSag(sag);
        }

        public List<SagDTO> GetAllSager()
        {
            return SagRepository.GetAllSager();
        }

        public List<SagDTO> GetSagerForAfdeling(int afdelingId)
        {
            return SagRepository.GetSagerForAfdeling(afdelingId);
        }
    }
}
