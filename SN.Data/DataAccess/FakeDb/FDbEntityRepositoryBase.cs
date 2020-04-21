using SN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SN.Data.DataAccess.FakeDb
{
    public class FakeDbEntityRepositoryBase<TEntity, TList> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TList : class, IEnumerable<TEntity>
    {
        private List<TEntity> _context { get; set; }

        public FakeDbEntityRepositoryBase(List<TEntity> list)
        {
            _context = list;
        }
        public void Add(TEntity entity)
        {
            _context.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return _context.Where(filter.Compile()).FirstOrDefault();
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? _context.ToList() : _context.Where(filter.Compile()).ToList(); ;
        }

        public void Update(TEntity entity, Expression<Func<TEntity, bool>> filter)
        {
            var record = Get(filter);
            if (record != null)
                SN.Class.Helpers.ClassHelper.CopyObjectPropertiesValue(entity, record);

        }
    }
}
