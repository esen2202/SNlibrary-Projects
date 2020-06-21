using SN.Class.Helpers;
using SN.Data.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SN.Data.DataAccess.SQLiteNet
{
    public class SQLiteEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private SQLiteConnection connection;

        private ISQLiteConnectionService _connectionManager { get; set; }

        public SQLiteEntityRepositoryBase(ISQLiteConnectionService connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public virtual void Add(TEntity entity)
        {
            using (connection = _connectionManager.GetConnection())
            {
                var result = connection.Insert(entity);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            using (connection = _connectionManager.GetConnection())
            {
                var result = connection.Delete(entity);
            }
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter = null)
        {
            using (connection = _connectionManager.GetConnection())
            {
                TEntity result = new TEntity();

                var exception = ExceptionHelper.CatchException(() =>
                {
                    result = connection.Get<TEntity>(filter);
                });

                return result;
            }
        }

        public virtual IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (connection = _connectionManager.GetConnection())
            {
                IList<TEntity> result;

                if (filter == null)
                    result = connection.Table<TEntity>().ToList();
                else
                    result = connection.Table<TEntity>().Where(filter).ToList();

                return result;
            }
        }

        public virtual void Update(TEntity entity, Expression<Func<TEntity, bool>> filter = null)
        {
            using (connection = _connectionManager.GetConnection())
            {
                var result = connection.Update(entity);
            }
        }

        public void InsertAll(IList<TEntity> entities)
        {
            using (connection = _connectionManager.GetConnection())
            {
                var result = connection.InsertAll(entities);
            }
        }
    }
}
