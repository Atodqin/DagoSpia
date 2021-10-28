using Microsoft.AspNetCore.Mvc;
using ProjectWork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Models
{
    public class QuantitaController : Controller
    {
        private readonly IQuantitaService<Quantita> _service;

        private readonly IService<Prodotto> _service2;
        private readonly IService<Colore> _service3;
        private readonly IService<Taglia> _service4;

        public QuantitaController(IQuantitaService<Quantita> service, IService<Prodotto> service2, IService<Colore> service3, IService<Taglia> service4)
        {
            _service = service;
            _service2 = service2;
            _service3 = service3;
            _service4 = service4;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Elenco(string nomeQuantita, string prodotto, string colore, string taglia)
        {
            List<Prodotto> listaProdotti = _service2.GetAll();
            ViewBag.listaProdotti = listaProdotti;
            List<Colore> listaColori = _service3.GetAll();
            ViewBag.listaColori = listaColori;
            List<Taglia> listaTaglie = _service4.GetAll();
            ViewBag.listaTaglie = listaTaglie;

            nomeQuantita ??= "";
            prodotto ??= "";
            colore ??= "";
            taglia ??= "";

            List<Quantita> listaQuantita = _service.GetAll();

            return View(listaQuantita.Where(q => q.Prodotti.Nome.ToLower().Contains(nomeQuantita.ToLower()) && q.Prodotti.Nome.ToLower().Contains(prodotto.ToLower()) && q.Colori.Nome.ToLower().Contains(colore.ToLower()) && q.Taglie.Nome.ToLower().Contains(taglia.ToLower())).OrderBy(q => q.Quantitativo).ToList());
        }

        [Route("/quantita/dettagli/{idprodotto}&{idcolore}&{idtaglia}")]
        public IActionResult Dettagli(int idprodotto, int idcolore, int idtaglia)
        {
            Quantita q = _service.SearchById(idprodotto, idcolore, idtaglia);

            if (q != null)
            {
                return View(q);
            }
            else
            {
                return Content($"Prodotto non trovato");
            }
        }
        public IActionResult FormNuovaQuantita(string erroreInsert, string erroreDB)
        {
            List<Prodotto> listaProdotti = _service2.GetAll();
            ViewBag.listaProdotti = listaProdotti;
            List<Colore> listaColori = _service3.GetAll();
            ViewBag.listaColori = listaColori;
            List<Taglia> listaTaglie = _service4.GetAll();
            ViewBag.listaTaglie = listaTaglie;

            ViewBag.ErrorMessage1 = erroreInsert;
            ViewBag.ErrorMessage2 = erroreDB;

            return View();
        }

        public IActionResult AggiungiQuantita(Dictionary<string, string> dati)
        {
            Quantita q = new Quantita();

            if (dati["quantitativo"] == null || int.Parse(dati["quantitativo"]) < 0)
            {
                return RedirectToAction("FormNuovaQuantita", new { erroreInsert = "Inserire una quantità maggiore di 0" });
            }

            q.FromDictionary(dati);

            if (_service.GetAll().Any(x => x.ColoreId == q.ColoreId && x.TagliaId == q.TagliaId && x.ProdottoId == q.ProdottoId))
            {
                return RedirectToAction("FormNuovaQuantita", new { erroreDB = "Prodotto già presente" });
            }
            else
            {
                _service.Add(q);
                return Redirect("/Quantita/Elenco");
            }
        }

        [Route("/quantita/FormModificaQuantita/{idprodotto}&{idcolore}&{idtaglia}")]
        public IActionResult FormModificaQuantita(int idprodotto, int idcolore, int idtaglia, string erroreInsert)
        {
            List<Prodotto> listaProdotti = _service2.GetAll();
            ViewBag.listaProdotti = listaProdotti;
            List<Colore> listaColori = _service3.GetAll();
            ViewBag.listaColori = listaColori;
            List<Taglia> listaTaglie = _service4.GetAll();
            ViewBag.listaTaglie = listaTaglie;

            ViewBag.ErrorMessage = erroreInsert;

            Quantita q = _service.SearchById(idprodotto, idcolore, idtaglia);
            return View(q);
        }

        public IActionResult ModificaQuantita(Dictionary<string, string> dati, int prodottoid, int coloreid, int tagliaid, int oldprodottoid, int oldcoloreid, int oldtagliaid)
        {
            Quantita q = new Quantita();

            if (dati["quantitativo"] == null || int.Parse(dati["quantitativo"]) < 0)
            {
                return RedirectToAction("FormModificaQuantita", new { idprodotto = prodottoid, idcolore = coloreid, idtaglia = tagliaid, erroreInsert = "Inserire una quantità maggiore di 0" });
            }
            else
            {
                q.FromDictionary(dati);
                _service.Update(oldprodottoid, oldcoloreid, oldtagliaid, prodottoid, coloreid, tagliaid, q);
                return Redirect("/Quantita/Elenco");
            }

        }

        [Route("/quantita/elimina/{idprodotto}&{idcolore}&{idtaglia}")]
        public IActionResult Elimina(int idprodotto, int idcolore, int idtaglia)
        {
            _service.Delete(idprodotto, idcolore, idtaglia);

            return Redirect("/Quantita/Elenco");
        }
    }
}
