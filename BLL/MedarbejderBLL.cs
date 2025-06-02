using System.Collections.Generic;
using Tidsregistrering.DTO.Models;
using Tidsregistrering.DAL.Repository;

namespace Tidsregistrering.BLL
{
    public class MedarbejderBLL
    {
        public MedarbejderDTO GetMedarbejder(int id)
        {
            if (id <= 0) return null;
            return MedarbejderRepository.GetMedarbejder(id);
        }

        public int CreateMedarbejder(MedarbejderDTO medarbejder)
        {
            return MedarbejderRepository.AddMedarbejder(medarbejder);
        }

        public List<MedarbejderDTO> GetMedarbejdereFraAfdeling(int afdelingId)
        {
            return MedarbejderRepository.GetMedarbejdereFraAfdeling(afdelingId);
        }
    }
}
