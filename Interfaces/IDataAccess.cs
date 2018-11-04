using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDataAccess<T>
    {
        T Get(Guid id);

        T Get(string name);

        ICollection<T> GetAll();

        void Add(T entity);

        void Modify(T entity);

        void Delete(T entity);
    }
}
