using System;
using System.Collections.Generic;
using Tidsregistrering.DTO.Models;
using Tidsregistrering.DAL.Repository;

namespace Tidsregistrering.BLL
{
    public class TidsregistreringBLL
    {
        public int CreateTidsregistrering(TidsregistreringDTO tidsregistrering)
        {
            return TidsregistreringRepository.AddTidsregistrering(tidsregistrering);
        }

        public List<TidsregistreringDTO> GetTidsregistreringerForMedarbejder(int medarbejderId)
        {
            return TidsregistreringRepository.GetTidsregistreringerForMedarbejder(medarbejderId);
        }

        public List<TidsregistreringDTO> GetTidsregistreringerForMedarbejderOgSag(int medarbejderId, int sagId)
        {
            return TidsregistreringRepository.GetTidsregistreringerForMedarbejderOgSag(medarbejderId, sagId);
        }

        public List<TidsregistreringDTO> GetTidsregistreringerForSag(int sagId)
        {
            return TidsregistreringRepository.GetTidsregistreringerForSag(sagId);
        }

        public bool HarTidsoverlap(int medarbejderId, DateTime start, DateTime slut)
        {
            return TidsregistreringRepository.HarTidsoverlap(medarbejderId, start, slut);
        }

        public bool WillNewRegistrationExceed37Hours(int medarbejderId, DateTime start, DateTime slut)
        {
            double nyeTimer = (slut - start).TotalHours;
            return TidsregistreringRepository.WillNewRegistrationExceed37Hours(medarbejderId, start, nyeTimer);
        }

    }
}

