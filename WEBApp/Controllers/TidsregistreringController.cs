using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Tidsregistrering.BLL;
using Tidsregistrering.DTO.Models;
using WEBApp.Models;

public class TidsregistreringController : Controller
{
    private readonly TidsregistreringBLL tidsBLL = new TidsregistreringBLL();
    private readonly SagBLL sagBLL = new SagBLL();

    public ActionResult Registrering(int afdelingId, int medarbejderId)
    {
        var sager = sagBLL.GetSagerForAfdeling(afdelingId);
        var registreringer = tidsBLL.GetTidsregistreringerForMedarbejder(medarbejderId);

        var medarbejder = new MedarbejderBLL().GetMedarbejder(medarbejderId);

        var nu = DateTime.Now;

        var ugeStart = nu.Date.AddDays(-(int)nu.DayOfWeek + (int)DayOfWeek.Monday);
        var ugeSlut = ugeStart.AddDays(7);
        var månedStart = new DateTime(nu.Year, nu.Month, 1);
        var månedSlut = månedStart.AddMonths(1);

        var timerUge = registreringer
            .Where(t => t.StartTid >= ugeStart && t.StartTid < ugeSlut)
            .Sum(t => t.Timer);

        var timerMåned = registreringer
            .Where(t => t.StartTid >= månedStart && t.StartTid < månedSlut)
            .Sum(t => t.Timer);

        var timerTotal = registreringer.Sum(t => t.Timer);

        var model = new TidsregistreringViewModel
        {
            AfdelingId = afdelingId,
            MedarbejderId = medarbejderId,
            MedarbejderNavn = medarbejder?.Navn ?? "Ukendt",
            Sager = sager,
            Tidsregistreringer = registreringer,
            StartTid = DateTime.Now,
            SlutTid = DateTime.Now.AddHours(1),

        };
        return View(model);
    }

    [HttpPost]
    public ActionResult Registrering(TidsregistreringViewModel model)
    {
        var tidsBLL = new TidsregistreringBLL();

        // Checker for forkert tids-indtastning.
        if (!ModelState.IsValid || model.StartTid >= model.SlutTid)
        {
            model.Sager = sagBLL.GetSagerForAfdeling(model.AfdelingId);
            model.Tidsregistreringer = tidsBLL.GetTidsregistreringerForMedarbejder(model.MedarbejderId);

            var medarbejder = new MedarbejderBLL().GetMedarbejder(model.MedarbejderId);
            model.MedarbejderNavn = medarbejder?.Navn ?? "Ukendt";

            ModelState.AddModelError("", "Starttid skal være før sluttid.");
            return View(model);
        }

        // Checker for om der prøves at indtaste mere end 37t på en uge.
        if (tidsBLL.WillNewRegistrationExceed37Hours(model.MedarbejderId, model.StartTid, model.SlutTid))
        {
            model.Sager = sagBLL.GetSagerForAfdeling(model.AfdelingId);
            model.Tidsregistreringer = tidsBLL.GetTidsregistreringerForMedarbejder(model.MedarbejderId);

            var medarbejder = new MedarbejderBLL().GetMedarbejder(model.MedarbejderId);
            model.MedarbejderNavn = medarbejder?.Navn ?? "Ukendt";

            ModelState.AddModelError("", "Du kan ikke registrere mere end 37 timer på en uge.");
            return View(model);
        }

        // Checker for om den indtastede tid vil overlappe med en eksisterende tid.
        if (tidsBLL.HarTidsoverlap(model.MedarbejderId, model.StartTid, model.SlutTid))
        {
            model.Sager = sagBLL.GetSagerForAfdeling(model.AfdelingId);
            model.Tidsregistreringer = tidsBLL.GetTidsregistreringerForMedarbejder(model.MedarbejderId);

            var medarbejder = new MedarbejderBLL().GetMedarbejder(model.MedarbejderId);
            model.MedarbejderNavn = medarbejder?.Navn ?? "Ukendt";

            ModelState.AddModelError("", "Tidsregistreringen overlapper med en eksisterende registrering.");
            return View(model);
        }

        //Hvis alt er korrekt indtastet og tilladt, oprettes tiden.
        var ny = new TidsregistreringDTO(model.StartTid, model.SlutTid, model.Kommentar, model.MedarbejderId, model.ValgtSagId);
        tidsBLL.CreateTidsregistrering(ny);

        return RedirectToAction("Registrering", new { afdelingId = model.AfdelingId, medarbejderId = model.MedarbejderId });
    }
}
