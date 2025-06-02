using System.Collections.Generic;

namespace Tidsregistrering.DTO.Models
{
    public class AfdelingDTO
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public int Nummer { get; set; }

        public List<MedarbejderDTO> Medarbejdere { get; set; }

        public AfdelingDTO()
        {
            Medarbejdere = new List<MedarbejderDTO>();
        }

        public AfdelingDTO(string navn, int nummer)
        {
            Navn = navn;
            Nummer = nummer;
            Medarbejdere = new List<MedarbejderDTO>();
        }
    }
}
