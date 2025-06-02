using System.Collections.Generic;
using System.Linq;
using Tidsregistrering.DAL;
using Tidsregistrering.DTO.Models;
using Tidsregistrering.DAL.Context;


namespace Tidsregistrering.DAL.Repository
{
    public class MedarbejderRepository
    {
        public static MedarbejderDTO GetMedarbejder(int id)
        {
            using (var context = new AppDbContext())
            {
                var medarbejderDal = context.Medarbejdere.FirstOrDefault(m => m.Id == id);
                return medarbejderDal != null ? MedarbejderMapper.ToDTO(medarbejderDal) : null;
            }
        }

        public static int AddMedarbejder(MedarbejderDTO medarbejderDto)
        {
            var medarbejderDal = MedarbejderMapper.ToDAL(medarbejderDto);

            using (var context = new AppDbContext())
            {
                context.Medarbejdere.Add(medarbejderDal);
                context.SaveChanges();
                return medarbejderDal.Id;
            }
        }

        public static List<MedarbejderDTO> GetAllMedarbejdere()
        {
            using (var context = new AppDbContext())
            {
                var alle = context.Medarbejdere.ToList();
                return alle.Select(MedarbejderMapper.ToDTO).ToList();
            }
        }

        public static List<MedarbejderDTO> GetMedarbejdereFraAfdeling(int afdelingId)
        {
            using (var context = new AppDbContext())
            {
                var medarbejdere = context.Medarbejdere
                    .Where(m => m.AfdelingId == afdelingId)
                    .ToList();

                return medarbejdere.Select(MedarbejderMapper.ToDTO).ToList();
            }
        }
    }
}
