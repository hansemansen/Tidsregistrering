using System.Collections.Generic;

namespace Tidsregistrering.DAL.Models
{
    public class SagDAL
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }

        public int AfdelingId { get; set; }
        public virtual AfdelingDAL Afdeling { get; set; }

        public virtual List<TidsregistreringDAL> Tidsregistreringer { get; set; }

        public SagDAL()
        {
            Tidsregistreringer = new List<TidsregistreringDAL>();
        }

        public SagDAL(string navn, string beskrivelse, int afdelingId)
        {
            Navn = navn;
            Beskrivelse = beskrivelse;
            AfdelingId = afdelingId;
            Tidsregistreringer = new List<TidsregistreringDAL>();
        }
    }
}
