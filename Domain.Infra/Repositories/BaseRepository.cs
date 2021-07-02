using Domain.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;

namespace Domain.Infra.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity :  class
    {
        protected readonly ProductCatalogContext _context;

        public BaseRepository(ProductCatalogContext context) 
        {
            _context = context;

        }

        protected void GenericSave(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception($"Fail to execute GenericSave. Fail Stack ===> {e}");
            }
        }

        protected void GenericUpdate(TEntity entity)
        {
            try
            {
                _context.Entry<TEntity>(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception($"Fail to execute GenericUpdate. Fail Stack ===> {e}");
            }
        }

        protected void GenericDelete(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception($"Fail to execute GenericDelete. Fail Stack ===> {e}");
            }
        }
    }
}
