using System;

namespace Tidsregistrering.DTO.Models
{
    public class TidsregistreringDTO
    {
        public int Id { get; set; }

        public DateTime StartTid { get; set; }
        public DateTime SlutTid { get; set; }
        public string Kommentar { get; set; }
        public int MedarbejderId { get; set; }
        public int SagId { get; set; }

        public double Timer
        {
            get
            {
                return (SlutTid - StartTid).TotalHours;
            }
        }

        public TidsregistreringDTO() { }

        public TidsregistreringDTO(DateTime startTid, DateTime slutTid, string kommentar, int medarbejderId, int sagId)
        {
            StartTid = startTid;
            SlutTid = slutTid;
            Kommentar = kommentar;
            MedarbejderId = medarbejderId;
            SagId = sagId;
        }
    }
}
