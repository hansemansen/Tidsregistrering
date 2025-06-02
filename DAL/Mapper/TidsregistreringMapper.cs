using System;
using Tidsregistrering.DAL.Models;
using Tidsregistrering.DTO.Models;

namespace Tidsregistrering.DAL
{
    public static class TidsregistreringMapper
    {
        public static TidsregistreringDTO ToDTO(TidsregistreringDAL dal)
        {
            if (dal == null) throw new ArgumentNullException(nameof(dal));

            return new TidsregistreringDTO
            {
                Id = dal.Id,
                StartTid = dal.StartTid,
                SlutTid = dal.SlutTid,
                Kommentar = dal.Kommentar,
                MedarbejderId = dal.MedarbejderId,
                SagId = dal.SagId
            };
        }

        public static TidsregistreringDAL ToDAL(TidsregistreringDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new TidsregistreringDAL(dto.StartTid, dto.SlutTid, dto.Kommentar, dto.MedarbejderId, dto.SagId)
            {
                Id = dto.Id
            };
        }
    }
}
