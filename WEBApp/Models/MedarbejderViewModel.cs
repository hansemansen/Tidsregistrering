using System.Collections.Generic;
using Tidsregistrering.DTO.Models;

namespace WEBApp.Models
{
    public class MedarbejderViewModel
    {
        public int ValgtMedarbejderId { get; set; }
        public int AfdelingId { get; set; }
        public List<MedarbejderDTO> Medarbejdere { get; set; }
    }
}
