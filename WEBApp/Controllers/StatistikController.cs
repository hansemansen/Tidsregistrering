using System;
using System.Web.Mvc;
using Tidsregistrering.BLL;
using System.Linq;

public class StatistikController : Controller
{
    private readonly TidsregistreringBLL tidsBLL = new TidsregistreringBLL();
    private readonly SagBLL sagBLL = new SagBLL();

    public ActionResult Index(int medarbejderId, int sagId, DateTime dato)
    {
        var ugeStart = dato.AddDays(-(int)dato.DayOfWeek + (int)DayOfWeek.Monday);
        var ugeSlut = ugeStart.AddDays(7);
        var månedStart = new DateTime(dato.Year, dato.Month, 1);
        var månedSlut = månedStart.AddMonths(1);
        
        var sag = sagBLL.GetSag(sagId);
        ViewBag.SagNavn = sag?.Navn ?? "Ukendt";

        var poster = tidsBLL.GetTidsregistreringerForMedarbejderOgSag(medarbejderId, sagId);
        ViewBag.AntalPoster = poster.Count;
        var timerUge = poster.Where(t => t.StartTid >= ugeStart && t.StartTid < ugeSlut).Sum(t => t.Timer);
        var timerMåned = poster.Where(t => t.StartTid >= månedStart && t.StartTid < månedSlut).Sum(t => t.Timer);
        var timerTotal = poster.Sum(t => t.Timer);

        ViewBag.MedarbejderId = medarbejderId;
        ViewBag.SagId = sagId;
        ViewBag.ValgtDato = dato.ToShortDateString();
        ViewBag.TimerUge = timerUge;
        ViewBag.TimerMåned = timerMåned;
        ViewBag.TimerTotal = timerTotal;

        return View();
    }
}
