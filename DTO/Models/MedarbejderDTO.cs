using System.Collections.Generic;

namespace Tidsregistrering.DTO.Models
{
    public class MedarbejderDTO
    {
        public int Id { get; set; }
        public string Initialer { get; set; }
        public string CPR { get; set; }
        public string Navn { get; set; }

        public int AfdelingId { get; set; }

        public List<TidsregistreringDTO> Tidsregistreringer { get; set; }

        public MedarbejderDTO()
        {
            Tidsregistreringer = new List<TidsregistreringDTO>();
        }

        public MedarbejderDTO(string initialer, string navn, string cpr, int afdelingId)
        {
            Initialer = initialer;
            Navn = navn;
            CPR = cpr;
            AfdelingId = afdelingId;
            Tidsregistreringer = new List<TidsregistreringDTO>();
        }
    }
}
