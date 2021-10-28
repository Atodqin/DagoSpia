using ProjectWork.Data;
using ProjectWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Services
{
    public class ColoreService : IService<Colore>
    {
        private readonly DataContext _ctx;

        public ColoreService(DataContext ctx)
        {
            _ctx = ctx;
        }

        public Colore Add(Colore newT)
        {
            var add = _ctx.Colori.Add(newT);
            _ctx.SaveChanges();

            return add.Entity;
        }

        public Colore Delete(int id)
        {
            var del = _ctx.Colori.FirstOrDefault(p => p.Id == id);
            if (del is null)
            {
                throw new Exception($"Prodcut not found {id}");
            }
            _ctx.Remove(del);
            _ctx.SaveChanges();
            return del;

        }

        public List<Colore> GetAll()
        {
            return _ctx.Colori.ToList();
        }

        public Colore SearchById(int id)
        {
            var del = _ctx.Colori.FirstOrDefault(p => p.Id == id);
            if (del is null)
            {
                throw new Exception($"Prodcut not found {id}");
            }
            return del;
        }

        public Colore Update(int id, Colore newT)
        {
            newT.Id = id;
            _ctx.Update(newT);
            _ctx.SaveChanges();
            return newT;
        }





    }
}
