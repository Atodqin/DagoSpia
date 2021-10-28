using Microsoft.AspNetCore.Mvc;
using ProjectWork.Models;
using ProjectWork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Controllers
{
    public class TagliaController : Controller
    {
        private readonly IService<Taglia> _service;

        public TagliaController(IService<Taglia> service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Elenco()
        {
            List<Taglia> listaTaglie = _service.GetAll();

            return View(listaTaglie);
        }

        public IActionResult Dettagli(int id)
        {
            Taglia t = _service.SearchById(id);

            if (t != null)
            {
                return View(t);
            }
            else
            {
                return Content($"Taglia con id {id} non trovata");
            }
        }
        public IActionResult FormNuovaTaglia(string erroreInsert)
        {
            ViewBag.ErrorMessage = erroreInsert;

            return View();
        }

        public IActionResult AggiungiTaglia(Dictionary<string, string> dati)
        {
            foreach (var item in dati)
            {
                if (item.Value == null)
                {
                    return RedirectToAction("FormNuovaTaglia", new { erroreInsert = "Inserire tutti i dati" });
                }
            }
            Taglia t = new Taglia();
            t.FromDictionary(dati);
            _service.Add(t);

            return Redirect("/Taglia/Elenco");
        }

        public IActionResult FormModificaTaglia(int id, string erroreInsert)
        {
            ViewBag.ErrorMessage = erroreInsert;

            Taglia t = _service.SearchById(id);
            return View(t);
        }

        public IActionResult ModificaTaglia(Dictionary<string, string> dati, int id)
        {
            foreach (var item in dati)
            {
                if (item.Value == null)
                {
                    return RedirectToAction("FormModificaTaglia", new { id = id, erroreInsert = "Inserire tutti i dati" });
                }
            }
            Taglia t = new Taglia();
            t.FromDictionary(dati);
            _service.Update(id, t);

            return Redirect("/Taglia/Elenco");
        }

        public IActionResult Elimina(int id)
        {
            _service.Delete(id);

            return Redirect("/Taglia/Elenco");
        }
    }
}
