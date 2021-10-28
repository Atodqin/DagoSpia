using ProjectWork.Data;
using ProjectWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Services
{
    public class ProdottoService : IService<Prodotto>
    {
        private readonly DataContext _ctx;

        public ProdottoService(DataContext ctx)
        {
            _ctx = ctx;
        }

        public Prodotto Add(Prodotto newT)
        {
            var add = _ctx.Prodotti.Add(newT);
            _ctx.SaveChanges();

            return add.Entity;
        }

        public Prodotto Delete(int id)
        {
            var del = _ctx.Prodotti.FirstOrDefault(p => p.Id == id);
            if (del is null)
            {
                throw new Exception($"Prodcut not found {id}");
            }
            _ctx.Remove(del);
            _ctx.SaveChanges();
            return del;

        }

        public List<Prodotto> GetAll()
        {
            return _ctx.Prodotti.ToList();
        }

        public Prodotto SearchById(int id)
        {
            var del = _ctx.Prodotti.FirstOrDefault(p => p.Id == id);
            if (del is null)
            {
                throw new Exception($"Prodcut not found {id}");
            }
            return del;
        }

        public Prodotto Update(int id, Prodotto newT)
        {
            newT.Id = id;

            _ctx.Prodotti.Update(newT);

            _ctx.SaveChanges();

            return newT;
        }
    }
}
