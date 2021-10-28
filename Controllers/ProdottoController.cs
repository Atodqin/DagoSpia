using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWork.Models;
using ProjectWork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Controllers
{

    public class ProdottoController : Controller
    {
        private readonly IService<Prodotto> _service;

        public ProdottoController(IService<Prodotto> service)
        {
            _service = service;
        }

        // GET: ProdottoController
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Elenco()
        {
            List<Prodotto> listaProdotti = _service.GetAll();

            return View(listaProdotti);
        }
        //eheeh
        public IActionResult Dettagli(int id)
        {
            Prodotto p = _service.SearchById(id);

            if (p != null)
            {
                return View(p);
            }
            else
            {
                return Content($"Prodotto con id {id} non trovato");
            }
        }
        public IActionResult FormNuovoProdotto(string erroreInsert)
        {
            ViewBag.ErrorMessage = erroreInsert;

            return View();
        }

        public IActionResult AggiungiProdotto(Dictionary<string, string> dati)
        {
            Prodotto p = new Prodotto();

            foreach (var item in dati)
            {
                if (item.Value == null)
                {
                    return RedirectToAction("FormNuovoProdotto", new { erroreInsert = "Inserire tutti i dati" });
                }
            }

            p.FromDictionary(dati);
            _service.Add(p);

            return Redirect("/Prodotto/Elenco");
        }

        public IActionResult FormModificaProdotto(int id, string erroreInsert)
        {
            ViewBag.ErrorMessage = erroreInsert;

            Prodotto p = _service.SearchById(id);
            return View(p);
        }

        public IActionResult ModificaProdotto(Dictionary<string, string> dati, int id)
        {
            Prodotto p = new Prodotto();

            foreach (var item in dati)
            {
                if (item.Value == null)
                {
                    return RedirectToAction("FormModificaProdotto", new { id = id, erroreInsert = "Inserire tutti i dati" });
                }
            }

            p.FromDictionary(dati);
            _service.Update(id, p);

            return Redirect("/Prodotto/Elenco");
        }

        public IActionResult Elimina(int id)
        {
            _service.Delete(id);

            return Redirect("/Prodotto/Elenco");
        }

    }

}