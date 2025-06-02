using System.Collections.Generic;

namespace Tidsregistrering.DAL.Models
{
    public class AfdelingDAL
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public int Nummer { get; set; }

        public virtual List<MedarbejderDAL> Medarbejdere { get; set; }

        public AfdelingDAL()
        {
            Medarbejdere = new List<MedarbejderDAL>();
        }

        public AfdelingDAL(string navn, int nummer)
        {
            Navn = navn;
            Nummer = nummer;
            Medarbejdere = new List<MedarbejderDAL>();
        }
    }
}
