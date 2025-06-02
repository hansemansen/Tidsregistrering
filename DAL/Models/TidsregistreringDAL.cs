using System;

namespace Tidsregistrering.DAL.Models
{
    public class TidsregistreringDAL
    {
        public int Id { get; set; }

        public DateTime StartTid { get; set; }
        public DateTime SlutTid { get; set; }

        public string Kommentar { get; set; }

        public int MedarbejderId { get; set; }
        public virtual MedarbejderDAL Medarbejder { get; set; }

        public int SagId { get; set; }
        public virtual SagDAL Sag { get; set; }

        public TidsregistreringDAL() { }

        public TidsregistreringDAL(DateTime startTid, DateTime slutTid, string kommentar, int medarbejderId, int sagId)
        {
            StartTid = startTid;
            SlutTid = slutTid;
            Kommentar = kommentar;
            MedarbejderId = medarbejderId;
            SagId = sagId;
        }
    }
}
