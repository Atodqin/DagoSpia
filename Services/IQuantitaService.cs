using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Services
{
    public interface IQuantitaService<T>
    {
        List<T> GetAll();

        T Add(T newItem);

        T Update(int oldidprodotto, int oldidcolore, int oldidtaglia, int idprodotto, int idcolore, int idtaglia, T item);

        T Delete(int idprodotto, int idcolore, int idtaglia);

        T SearchById(int idprodotto, int idcolore, int idtaglia);
    }
}
