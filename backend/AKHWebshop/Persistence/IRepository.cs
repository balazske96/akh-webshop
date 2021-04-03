#nullable enable
using System;
using System.Collections.Generic;


namespace Persistence
{
    public interface IRepository<T>
    {
        public IEnumerable<T> GetItems(int limit, int offset, Func<T, bool>? filter);

        public T GetItemById(Guid id);

        public void SaveItem(T item);

        public void UpdateItem(T item);

        public void DeleteItem(T item);

        public void SaveItem(IEnumerable<T> items);

        public void UpdateItem(IEnumerable<T> items);

        public void DeleteItem(IEnumerable<T> items);
    }
}