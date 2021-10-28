using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Services
{
    public interface IService<T>
    {
        List<T> GetAll();
        T Add(T newT);
        T SearchById(int id);
        T Update(int id, T newT);
        T Delete(int id);
    }
}
