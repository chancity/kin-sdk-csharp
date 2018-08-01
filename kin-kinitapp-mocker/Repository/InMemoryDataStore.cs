using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace kin_kinit_mocker.Repository
{
    public class InMemoryDataStore : IDataStore
    {
        private readonly ConcurrentDictionary<string, object> _store;
        public string InstanceId { get; private set; }

        public InMemoryDataStore(string instanceId)
        {
            InstanceId = instanceId;
            _store = new ConcurrentDictionary<string, object>();
        }

        public T GetValue<T>(string key) where T : class 
        {
            _store.TryGetValue(key, out var value);
            return (T) value;
        }

        public T GetValue<T>(string key, T value)
        {
            return (T)_store.GetOrAdd(key, value);
        }

        public void PutValue<T>(string key, T value)
        {
            _store.AddOrUpdate(key, value, (s, o) => !value.Equals((T)o));
        }

        public void Clear(string key)
        {
            _store.TryRemove(key, out var value);
        }

        public void ClearAll()
        {
           _store.Clear();
        }

        public ImmutableDictionary<string, object> GetAll()
        {
            return _store.ToImmutableDictionary();
        }
    }
}
