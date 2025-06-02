using System.Collections.Generic;

namespace Tidsregistrering.DAL.Models
{
    public class MedarbejderDAL
    {
        public int Id { get; set; }
        public string Initialer { get; set; }
        public string CPR { get; set; }
        public string Navn { get; set; }

        public int AfdelingId { get; set; }
        public virtual AfdelingDAL Afdeling { get; set; }

        public virtual List<TidsregistreringDAL> Tidsregistreringer { get; set; }

        public MedarbejderDAL()
        {
            Tidsregistreringer = new List<TidsregistreringDAL>();
        }

        public MedarbejderDAL(string initialer, string navn, string cpr, int afdelingId)
        {
            Initialer = initialer;
            Navn = navn;
            CPR = cpr;
            AfdelingId = afdelingId;
            Tidsregistreringer = new List<TidsregistreringDAL>();
        }
    }
}
