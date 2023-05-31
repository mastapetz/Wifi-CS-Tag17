using Microsoft.AspNetCore.Mvc;
using WebSteuer.Models;

namespace WebSteuer.Controllers
{
    public class SteuerController : Controller
    {
        // Get: Holen der Webseite
        public IActionResult Index()
        {
            // Steuerobjekt anlegen
            SteuerModel steuer = new SteuerModel();
            // SteuerModel wird an View übergeben
            return View(steuer);
        }

        // Post: SteuerModel wird ausgefüllt zum Controller geschickt
        [HttpPost]
        public ActionResult Berechnen(SteuerModel steuer)
        {
            if (ModelState.IsValid)
            {
                steuer.Berechnen();
            }
            // Wenn Name der Methode unterschiedlich ist von View,
            // muss der Name der View mitgegeben werden
            return View("Index", steuer);
        }
    }
}
