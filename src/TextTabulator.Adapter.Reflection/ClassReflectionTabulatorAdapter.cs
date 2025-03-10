using System;
using System.Collections.Generic;
using System.Reflection;

namespace TextTabulator.Adapter.Reflection
{
    public interface IClassReflectionTabulatorAdapter : IReflectionTabulatorAdapter
    {
    }

    public class ClassReflectionTabulatorAdapter<TClass> : IClassReflectionTabulatorAdapter where TClass : class
    {
        private readonly IEnumerable<TClass> _items;

        private PropertyInfo[]? _propertyInfos;

        public ClassReflectionTabulatorAdapter(IEnumerable<TClass> items)
        {
            _items = items;
        }

        private void GetProperties()
        {
            if (_propertyInfos == null)
            {
                var type = typeof(TClass);
                _propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            }
        }

        public IEnumerable<string>? GetHeaderStrings()
        {
            GetProperties();

            if (_propertyInfos == null
                || _propertyInfos.Length == 0)
            {
                return null;
            }

            var headers = new List<string>();

            for (var i = 0; i < _propertyInfos!.Length; i++)
            {
                headers.Add(_propertyInfos[i].Name);
            }

            return headers;
        }

        public IEnumerable<IEnumerable<string>> GetValueStrings()
        {
            GetProperties();

            if (_propertyInfos == null
                || _propertyInfos.Length == 0)
            {
                return Array.Empty<IEnumerable<string>>();
            }

            var values = new List<string[]>();

            foreach (var item in _items)
            {
                var row = new string[_propertyInfos!.Length];

                for (var i = 0; i < row.Length; i++)
                {
                    row[i] = _propertyInfos[i].GetValue(item).ToString();
                }

                values.Add(row);
            }

            return values;
        }
    }
}
