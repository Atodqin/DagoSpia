using Microsoft.AspNetCore.Mvc;
using ProjectWork.Models;
using ProjectWork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Controllers
{
    public class ColoreController : Controller
    {
        private readonly IService<Colore> _service;

        public ColoreController(IService<Colore> service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Elenco()
        {
            List<Colore> listaColori = _service.GetAll();

            return View(listaColori);
        }

        public IActionResult Dettagli(int id)
        {
            Colore c = _service.SearchById(id);

            if (c != null)
            {
                return View(c);
            }
            else
            {
                return Content($"Colore con id {id} non trovato");
            }
        }
        public IActionResult FormNuovoColore(string erroreInsert)
        {
            ViewBag.ErrorMessage = erroreInsert;

            return View();
        }

        public IActionResult AggiungiColore(Dictionary<string, string> dati)
        {
            foreach (var item in dati)
            {
                if (item.Value == null)
                {
                    return RedirectToAction("FormNuovoColore", new { erroreInsert = "Inserire tutti i dati" });
                }
            }
            Colore c = new Colore();
            c.FromDictionary(dati);
            _service.Add(c);

            return Redirect("/Colore/Elenco");
        }

        public IActionResult FormModificaColore(int id, string erroreInsert)
        {
            ViewBag.ErrorMessage = erroreInsert;

            Colore c = _service.SearchById(id);
            return View(c);
        }

        public IActionResult ModificaColore(Dictionary<string, string> dati, int id)
        {
            foreach (var item in dati)
            {
                if (item.Value == null)
                {
                    return RedirectToAction("FormModificaColore", new { id = id, erroreInsert = "Inserire tutti i dati" });
                }
            }
            Colore c = new Colore();
            c.FromDictionary(dati);
            _service.Update(id, c);

            return Redirect("/Colore/Elenco");
        }

        public IActionResult Elimina(int id)
        {
            _service.Delete(id);

            return Redirect("/Colore/Elenco");
        }
    }
}
