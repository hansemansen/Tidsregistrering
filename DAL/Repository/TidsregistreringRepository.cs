using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Tidsregistrering.DAL.Context;
using Tidsregistrering.DAL.Models;
using Tidsregistrering.DTO.Models;

namespace Tidsregistrering.DAL.Repository
{
    public static class TidsregistreringRepository
    {
        public static int AddTidsregistrering(TidsregistreringDTO dto)
        {
            var dal = TidsregistreringMapper.ToDAL(dto);

            using (var context = new AppDbContext())
            {
                context.Tidsregistreringer.Add(dal);
                context.SaveChanges();
                return dal.Id;
            }
        }

        public static List<TidsregistreringDTO> GetTidsregistreringerForMedarbejder(int medarbejderId)
        {
            using (var context = new AppDbContext())
            {
                var registreringer = context.Tidsregistreringer
                    .Where(t => t.MedarbejderId == medarbejderId)
                    .ToList();

                return registreringer.Select(TidsregistreringMapper.ToDTO).ToList();
            }
        }

        public static List<TidsregistreringDTO> GetTidsregistreringerForMedarbejderOgSag(int medarbejderId, int sagId)
        {
            using (var context = new AppDbContext())
            {
                var registreringer = context.Tidsregistreringer
                    .Where(t => t.MedarbejderId == medarbejderId && t.SagId == sagId)
                    .ToList();

                return registreringer.Select(TidsregistreringMapper.ToDTO).ToList();
            }
        }
        public static List<TidsregistreringDTO> GetTidsregistreringerForSag(int sagId)
        {
            using (var context = new AppDbContext())
            {
                var registreringer = context.Tidsregistreringer
                    .Where(t => t.SagId == sagId)
                    .ToList();

                return registreringer.Select(TidsregistreringMapper.ToDTO).ToList();
            }
        }

        public static bool HarTidsoverlap(int medarbejderId, DateTime start, DateTime slut)
        {
            using (var context = new AppDbContext())
            {
                return context.Tidsregistreringer.Any(t =>
                    t.MedarbejderId == medarbejderId &&
                    start < t.SlutTid &&
                    slut > t.StartTid);
            }
        }

        public static bool WillNewRegistrationExceed37Hours(int medarbejderId, DateTime start, double nyeTimer)
        {
            var ugeStart = start.Date.AddDays(-(int)start.DayOfWeek + (int)DayOfWeek.Monday);
            var ugeSlut = ugeStart.AddDays(7);

            using (var context = new AppDbContext())
            {
                var eksisterendeMinutter = context.Tidsregistreringer
                  .Where(t =>
                  t.MedarbejderId == medarbejderId &&
                  t.StartTid >= ugeStart &&
                  t.StartTid < ugeSlut)
                  .Select(t => DbFunctions.DiffMinutes(t.StartTid, t.SlutTid)) 
                  .Where(m => m.HasValue)       
                  .Select(m => m.Value)         
                  .DefaultIfEmpty(0)            
                  .Sum();                       

                var eksisterendeTimer = eksisterendeMinutter / 60.0;

                return (eksisterendeTimer + nyeTimer) > 37;
            }
        }


    }
}
