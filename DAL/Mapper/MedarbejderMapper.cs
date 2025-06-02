using System;
using System.Linq;
using Tidsregistrering.DAL.Models;
using Tidsregistrering.DTO.Models;

namespace Tidsregistrering.DAL
{
    public static class MedarbejderMapper
    {
        public static MedarbejderDTO ToDTO(MedarbejderDAL dal)
        {
            if (dal == null) throw new ArgumentNullException(nameof(dal));

            return new MedarbejderDTO(dal.Initialer, dal.Navn, dal.CPR, dal.AfdelingId)
            {
                Id = dal.Id,
                Tidsregistreringer = dal.Tidsregistreringer?.Select(TidsregistreringMapper.ToDTO).ToList()
            };
        }

        public static MedarbejderDAL ToDAL(MedarbejderDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new MedarbejderDAL(dto.Initialer, dto.Navn, dto.CPR, dto.AfdelingId)
            {
                Id = dto.Id,
                Tidsregistreringer = dto.Tidsregistreringer?.Select(TidsregistreringMapper.ToDAL).ToList()
            };
        }
    }
}
