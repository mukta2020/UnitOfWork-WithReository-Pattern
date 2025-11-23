using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessEF
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly PeopleContext context;
        public GenericRepository(PeopleContext context)
        {
            this.context = context;
        }
        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().Where(expression);
        }
        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }
        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }
        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
        }
        public virtual Task Update(T entity)
        {
            this.context.Set<T>().Attach(entity);
            this.context.Entry(entity).State = EntityState.Modified;

            return Task.CompletedTask;
        }

        public virtual Task UpdateRange(IEnumerable<T> entities)
        {
            this.context.Set<T>().AttachRange(entities);
            this.context.Entry(entities).State = EntityState.Modified;

            return Task.CompletedTask;
        }
        public virtual Task Detache(T entity)
        {
            this.context.Set<T>().Attach(entity);
            this.context.Entry(entity).State = EntityState.Detached;

            return Task.CompletedTask;
        }

        public virtual Task DetacheRange(IEnumerable<T> entities)
        {
            this.context.Set<T>().AttachRange(entities);
            this.context.Entry(entities).State = EntityState.Detached;

            return Task.CompletedTask;
        }
    }
}
