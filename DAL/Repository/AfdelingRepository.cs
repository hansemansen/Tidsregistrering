using System;
using System.Collections.Generic;
using System.Linq;
using Tidsregistrering.DAL;
using Tidsregistrering.DTO.Models;
using Tidsregistrering.DAL.Context;


namespace Tidsregistrering.DAL.Repository
{
    public class AfdelingRepository
    {
        public static AfdelingDTO GetAfdeling(int id)
        {
            using (var context = new AppDbContext())
            {
                var afdDal = context.Afdelinger.FirstOrDefault(a => a.Id == id);
                return afdDal != null ? AfdelingMapper.ToDTO(afdDal) : null;
            }
        }

        public static int AddAfdeling(AfdelingDTO afdDto)
        {
            var afdDal = AfdelingMapper.ToDAL(afdDto);

            using (var context = new AppDbContext())
            {
                context.Afdelinger.Add(afdDal);
                context.SaveChanges();
                return afdDal.Id;
            }
        }

        public static List<AfdelingDTO> GetAllAfdelinger()
        {
            using (var context = new AppDbContext())
            {
                var afdelinger = context.Afdelinger.ToList();
                return afdelinger.Select(AfdelingMapper.ToDTO).ToList();
            }
        }
    }
}
