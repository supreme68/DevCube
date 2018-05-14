using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCube.Data.Models;

namespace DevCube.Data.Repositories
{
    interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entities);
        void Delete(TEntity entities);
    }
}
