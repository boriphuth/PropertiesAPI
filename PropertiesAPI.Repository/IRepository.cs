using PropertiesAPI.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertiesAPI.Repository
{
    public interface IRepository<T> where T : IEntity 
    {
        void CreateEntity(T entity);

        List<T> GetAll();

        void Delete(int id);

        void Update(T entity);
    }
}
