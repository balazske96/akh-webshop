#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class Repository<T> : IRepository<T> where T : class, IHasId
    {
        private DbContext DataContext { get; set; }

        public Repository(DbContext dataContext)
        {
            DataContext = dataContext;
        }

        public IEnumerable<T> GetItems(int limit, int offset, Func<T, bool>? filter)
        {
            if (filter == null)
            {
                return DataContext.Set<T>().Skip(offset).Take(limit).ToList();
            }

            return DataContext.Set<T>().Where(filter).Skip(offset).Take(limit).ToList();
        }

        public T GetItemById(Guid id)
        {
            return DataContext.Set<T>().Find(id);
        }

        public void SaveItem(T item)
        {
            DataContext.Set<T>().Add(item);
        }

        public void UpdateItem(T item)
        {
            T itemToEdit = DataContext.Set<T>().Find(item.Id);
            DataContext.Entry(itemToEdit).CurrentValues.SetValues(item);
            DataContext.SaveChanges();
        }

        public void DeleteItem(T item)
        {
            DataContext.Set<T>().Remove(item);
        }

        public void SaveItem(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                SaveItem(item);
            }
        }

        public void UpdateItem(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                UpdateItem(item);
            }
        }

        public void DeleteItem(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                DeleteItem(item);
            }
        }
    }
}