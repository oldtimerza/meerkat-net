using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meerkat.Model
{
    public interface IRepository<T>
    {
        List<T> Get();
        void Create(T t);
        T Update(int id, T t);
        void Delete(int id);
    }
}
