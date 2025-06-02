using System;
using Tidsregistrering.DAL.Models;
using Tidsregistrering.DTO.Models;

namespace Tidsregistrering.DAL
{
    public static class SagMapper
    {
        public static SagDTO ToDTO(SagDAL dal)
        {
            if (dal == null) throw new ArgumentNullException(nameof(dal));

            return new SagDTO(dal.Navn, dal.Beskrivelse, dal.AfdelingId)
            {
                Id = dal.Id
            };
        }

        public static SagDAL ToDAL(SagDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new SagDAL(dto.Navn, dto.Beskrivelse, dto.AfdelingId)
            {
                Id = dto.Id
            };
        }
    }
}
