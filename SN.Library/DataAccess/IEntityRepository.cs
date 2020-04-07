using SN.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SN.Library.DataAccess
{
    public interface IEntityRepository<TEntity> where TEntity:class, IEntity,new()
    {
        TEntity Get(Expression<Func<TEntity,bool>> filter = null);
        List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null);
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity, Expression<Func<TEntity, bool>> filter = null);
    }
}
