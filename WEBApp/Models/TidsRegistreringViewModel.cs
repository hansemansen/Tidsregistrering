using System;
using System.Collections.Generic;
using System.Linq;
using Tidsregistrering.DTO.Models;

namespace WEBApp.Models
{
    public class TidsregistreringViewModel
    {
        public int AfdelingId { get; set; }
        public int MedarbejderId { get; set; }
        public string MedarbejderNavn { get; set; }
        public int ValgtSagId { get; set; }
        public string Kommentar { get; set; }
        public DateTime StartTid { get; set; }
        public DateTime SlutTid { get; set; }     
        public List<SagDTO> Sager { get; set; }
        public List<TidsregistreringDTO> Tidsregistreringer { get; set; }

        public TimeSpan TotalTid
        {
            get
            {
                return TimeSpan.FromHours(Tidsregistreringer?.Sum(t => t.Timer) ?? 0);
            }
        }
    }
}
