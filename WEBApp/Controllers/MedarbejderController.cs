using System.Web.Mvc;
using Tidsregistrering.BLL;
using WEBApp.Models;

public class MedarbejderController : Controller
{
    private readonly MedarbejderBLL medarbejderBLL = new MedarbejderBLL();

    public ActionResult VælgMedarbejder(int afdelingId)
    {
        var medarbejdere = medarbejderBLL.GetMedarbejdereFraAfdeling(afdelingId);

        var model = new MedarbejderViewModel
        {
            AfdelingId = afdelingId,
            Medarbejdere = medarbejdere
        };

        return View(model);
    }

    [HttpPost]
    public ActionResult VælgMedarbejder(MedarbejderViewModel model)
    {
        if (model.ValgtMedarbejderId <= 0)
        {
            ModelState.AddModelError("", "Vælg venligst en gyldig medarbejder.");
            model.Medarbejdere = medarbejderBLL.GetMedarbejdereFraAfdeling(model.AfdelingId);
            return View(model);
        }

        return RedirectToAction("Registrering", "Tidsregistrering", new
        {
            afdelingId = model.AfdelingId,
            medarbejderId = model.ValgtMedarbejderId
        });
    }
}
