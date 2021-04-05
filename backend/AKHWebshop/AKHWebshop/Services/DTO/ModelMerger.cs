using System;
using System.Linq;

namespace AKHWebshop.Services.DTO
{
    public class ModelMerger : IModelMerger
    {
        // Thanks to -> https://stackoverflow.com/a/8702650/11424213
        public void CopyValues<T>(T target, T source)
        {
            Type t = typeof(T);

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                if (value != null)
                    prop.SetValue(target, value, null);
            }
        }
    }
}