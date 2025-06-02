using System.Collections.Generic;
using Tidsregistrering.DTO.Models;

namespace WEBApp.Models
{
    public class AfdelingViewModel
    {
        public int ValgtAfdelingId { get; set; }
        public List<AfdelingDTO> Afdelinger { get; set; }
    }
}
