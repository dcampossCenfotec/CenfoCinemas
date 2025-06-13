using DataAccess.DAO;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    //Clase abstracta que servirá como base para las clases de CRUD específicas
    public abstract class CrudFactory
    {
        protected SqlDAO _sqlDao;
        public abstract void Create(BaseDTO baseDTO);
        public abstract void Update(BaseDTO baseDTO);
        public abstract void Delete(BaseDTO baseDTO);
        public abstract T Retrieve<T>();
        public abstract T RetrieveById<T>();
        public abstract List<T> RetrieveAll<T>();
    }
}
