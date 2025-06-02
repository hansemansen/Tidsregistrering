using System;
using System.Linq;
using Tidsregistrering.DAL.Models;
using Tidsregistrering.DTO.Models;

namespace Tidsregistrering.DAL
{
    public static class AfdelingMapper
    {
        public static AfdelingDTO ToDTO(AfdelingDAL dal)
        {
            if (dal == null) throw new ArgumentNullException(nameof(dal));

            return new AfdelingDTO(dal.Navn, dal.Nummer)
            {
                Id = dal.Id,
                Medarbejdere = dal.Medarbejdere?.Select(MedarbejderMapper.ToDTO).ToList()
            };
        }

        public static AfdelingDAL ToDAL(AfdelingDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new AfdelingDAL(dto.Navn, dto.Nummer)
            {
                Id = dto.Id,
                Medarbejdere = dto.Medarbejdere?.Select(MedarbejderMapper.ToDAL).ToList()
            };
        }
    }
}
