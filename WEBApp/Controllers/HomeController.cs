using System.Web.Mvc;
using Tidsregistrering.BLL;
using WEBApp.Models;

public class HomeController : Controller
{
    private readonly AfdelingBLL afdelingBLL = new AfdelingBLL();

    public ActionResult Index()
    {
        var afdelinger = afdelingBLL.GetAllAfdelinger();
        var model = new AfdelingViewModel
        {
            Afdelinger = afdelinger
        };

        return View(model);
    }

    [HttpPost]
    public ActionResult VælgAfdeling(AfdelingViewModel model)
    {
        if (model.ValgtAfdelingId <= 0)
        {
            ModelState.AddModelError("", "Vælg venligst en gyldig afdeling.");
            model.Afdelinger = afdelingBLL.GetAllAfdelinger();
            return View("Index", model);
        }

        return RedirectToAction("VælgMedarbejder", "Medarbejder", new { afdelingId = model.ValgtAfdelingId });
    }
}
