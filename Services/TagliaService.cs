using ProjectWork.Data;
using ProjectWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Services
{
    public class TagliaService : IService<Taglia>
    {
        private readonly DataContext _ctx;

        public TagliaService(DataContext ctx)
        {
            _ctx = ctx;
        }

        public Taglia Add(Taglia newT)
        {
            var add = _ctx.Taglie.Add(newT);
            _ctx.SaveChanges();

            return add.Entity;
        }

        public Taglia Delete(int id)
        {
            var del = _ctx.Taglie.FirstOrDefault(p => p.Id == id);
            if (del is null)
            {
                throw new Exception($"Prodcut not found {id}");
            }
            _ctx.Remove(del);
            _ctx.SaveChanges();
            return del;

        }

        public List<Taglia> GetAll()
        {
            return _ctx.Taglie.ToList();
        }

        public Taglia SearchById(int id)
        {
            var del = _ctx.Taglie.FirstOrDefault(p => p.Id == id);
            if (del is null)
            {
                throw new Exception($"Prodcut not found {id}");
            }
            return del;
        }

        public Taglia Update(int id, Taglia newT)
        {
            newT.Id = id;
            _ctx.Update(newT);
            _ctx.SaveChanges();
            return newT;
        }

    }
}
