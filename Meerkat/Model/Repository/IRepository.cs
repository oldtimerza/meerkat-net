using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meerkat.Model
{
    public interface IRepository<T>
    {
        List<T> get();
        void create(T t);
    }
}
