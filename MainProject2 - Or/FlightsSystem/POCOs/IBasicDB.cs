using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public interface IBasicDB<T> where T:IPOCO
    {
        void Add(T item);
        T Get(int id);
        IList<T> GetAll();
        void Remove(T item);
        void Update(T item);
    }
}
