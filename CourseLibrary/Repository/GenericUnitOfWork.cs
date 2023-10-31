using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    //using(GenericUnitOfWork obj = new GenericUnitOfWork()){}
    public class GenericUnitOfWork : IDisposable
    {
        DbContext context;
        public GenericUnitOfWork(DbContext context)
        {
            this.context = context;
        }
        //колекція репозиторіїв
        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        //Додавання репозиторію
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if(repositories.Keys.Contains(typeof(TEntity)) == true)
            {
                return repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
            }
            IGenericRepository<TEntity> repos = new EFGenericRepository<TEntity>(context);
            repositories.Add(typeof(TEntity), repos);
            return repos;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public void Dispose()
        {
            context.Dispose();
        }
    }
}
