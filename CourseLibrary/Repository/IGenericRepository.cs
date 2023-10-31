using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        //CRUD
        // ------- Create(INSERT)
        void Add(TEntity entity);
        //--------- Read(SELECT)
        // SELECT * FROM table WHERE id = 1
        TEntity FindById(params object[] id);

        //SELECT * FROM table WHERE Name = 'TV' AND Price = 21
        // x => x.Name == "TV" && x.Price == 21
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetAll();

        // DELETE
        void Remove(TEntity entity);

        //UPDATE
        void Update(TEntity entity);


    }
    
}