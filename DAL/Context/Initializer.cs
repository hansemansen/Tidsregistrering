using System;
using System.Collections.Generic;
using System.Data.Entity;
using Tidsregistrering.DAL.Models;

namespace Tidsregistrering.DAL.Context
{
    public class Initializer : CreateDatabaseIfNotExists<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            // Opret og tilføj afdelinger
            var afd1 = new AfdelingDAL("IT", 101);
            var afd2 = new AfdelingDAL("HR", 102);
            context.Afdelinger.Add(afd1);
            context.Afdelinger.Add(afd2);
            context.SaveChanges(); // 🔥 Så de får Id'er

            // Opret medarbejdere med rigtige Id'er
            var mk = new MedarbejderDAL("MK", "Mikkel Hansen", "123456-7890", afd1.Id);
            var lp = new MedarbejderDAL("LP", "Lise Nørgaard", "234567-1234", afd1.Id);
            var hh = new MedarbejderDAL("HH", "Kenneth Hansen", "345678-5678", afd2.Id);

            // Tilføj medarbejdere til afdelinger
            afd1.Medarbejdere.AddRange(new[] { mk, lp });
            afd2.Medarbejdere.Add(hh);
            context.SaveChanges();

            // Opret sager
            var sag1 = new SagDAL("Sag A", "Udvikling af ny portal", afd1.Id);
            var sag2 = new SagDAL("Sag B", "HR Onboarding", afd2.Id);
            var sagAdHocIT = new SagDAL("Ad hoc", "Ikke tilknyttet projekt", afd1.Id);
            var sagAdHocHR = new SagDAL("Ad hoc", "Ikke tilknyttet projekt", afd2.Id);
            context.Sager.AddRange(new[] { sag1, sag2, sagAdHocIT, sagAdHocHR });
            context.SaveChanges();

            // Opret tidsregistreringer
            var tidsregistreringer = new List<TidsregistreringDAL>
    {
        new TidsregistreringDAL
        {
            StartTid = DateTime.Today.AddDays(-2).AddHours(8),
            SlutTid = DateTime.Today.AddDays(-2).AddHours(12),
            Kommentar = "Møde og planlægning",
            MedarbejderId = mk.Id,
            SagId = sag1.Id
        },
        new TidsregistreringDAL
        {
            StartTid = DateTime.Today.AddDays(-1).AddHours(9),
            SlutTid = DateTime.Today.AddDays(-1).AddHours(12),
            Kommentar = "Kodning",
            MedarbejderId = mk.Id,
            SagId = sag1.Id
        },
        new TidsregistreringDAL
        {
            StartTid = DateTime.Today.AddDays(-1).AddHours(13),
            SlutTid = DateTime.Today.AddDays(-1).AddHours(16),
            Kommentar = "Test og dokumentation",
            MedarbejderId = mk.Id,
            SagId = sag1.Id
        }
    };

            context.Tidsregistreringer.AddRange(tidsregistreringer);
            context.SaveChanges();
        }

    }
}
