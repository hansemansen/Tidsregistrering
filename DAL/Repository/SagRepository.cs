using System;
using System.Collections.Generic;
using System.Linq;
using Tidsregistrering.DAL;
using Tidsregistrering.DTO.Models;
using Tidsregistrering.DAL.Context;

namespace Tidsregistrering.DAL.Repository
{
    public static class SagRepository
    {
        public static SagDTO GetSag(int id)
        {
            using (var context = new AppDbContext())
            {
                var sagDal = context.Sager.FirstOrDefault(s => s.Id == id);
                return sagDal != null ? SagMapper.ToDTO(sagDal) : null;
            }
        }

        public static int AddSag(SagDTO sagDto)
        {
            var sagDal = SagMapper.ToDAL(sagDto);

            using (var context = new AppDbContext())
            {
                context.Sager.Add(sagDal);
                context.SaveChanges();
                return sagDal.Id;
            }
        }

        public static List<SagDTO> GetAllSager()
        {
            using (var context = new AppDbContext())
            {
                var sager = context.Sager.ToList();
                return sager.Select(SagMapper.ToDTO).ToList();
            }
        }

        public static List<SagDTO> GetSagerForAfdeling(int afdelingId)
        {
            using (var context = new AppDbContext())
            {
                var sager = context.Sager.Where(s => s.AfdelingId == afdelingId).ToList();
                return sager.Select(SagMapper.ToDTO).ToList();
            }
        }
    }
}
