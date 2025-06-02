using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tidsregistrering.DTO.Models
{
    public class SagDTO
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public int AfdelingId { get; set; }

        public SagDTO() { }

        public SagDTO(string navn, string beskrivelse, int afdelingId)
        {
            Navn = navn;
            Beskrivelse = beskrivelse;
            AfdelingId = afdelingId;
        }
    }
}
