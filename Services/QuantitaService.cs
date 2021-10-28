using Microsoft.EntityFrameworkCore;
using ProjectWork.Data;
using ProjectWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Services
{
    public class QuantitaService : IQuantitaService<Quantita>
    {
        private readonly DataContext _ctx;

        public QuantitaService(DataContext ctx)
        {
            _ctx = ctx;
        }

        public Quantita Add(Quantita newItem)
        {
            var quantita = _ctx.Quantitas.Add(newItem);
            _ctx.SaveChanges();
            return quantita.Entity;
        }

        public Quantita Delete(int idprodotto, int idcolore, int idtaglia)
        {
            var quantita = _ctx.Quantitas.FirstOrDefault(q => q.ColoreId == idcolore && q.ProdottoId == idprodotto && q.TagliaId == idtaglia);

            _ctx.Remove(quantita);
            _ctx.SaveChanges();
            return quantita;
        }

        public List<Quantita> GetAll()
        {
            return _ctx.Quantitas.Include(p => p.Colori).Include(p => p.Taglie).Include(p => p.Prodotti).ToList();
        }

        public Quantita Update(int oldidprodotto, int oldidcolore, int oldidtaglia, int idprodotto, int idcolore, int idtaglia, Quantita item)
        {
            item.ProdottoId = idprodotto;
            item.TagliaId = idtaglia;
            item.ColoreId = idcolore;

            Delete(oldidprodotto, oldidcolore, oldidtaglia);

            Add(item);

            return item;
        }

        public Quantita SearchById(int idprodotto, int idcolore, int idtaglia)
        {
            var quantita = _ctx.Quantitas.Include(p => p.Colori).Include(p => p.Taglie).Include(p => p.Prodotti).FirstOrDefault(q => q.ColoreId == idcolore && q.ProdottoId == idprodotto && q.TagliaId == idtaglia);

            return quantita;
        }
    }
}
