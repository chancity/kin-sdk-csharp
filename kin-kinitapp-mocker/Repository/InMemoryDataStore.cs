using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace kin_kinit_mocker.Repository
{
    internal class InMemoryDataStore : IDataStore
    {
        [JsonProperty]
        private ConcurrentDictionary<string, object> Store { get; set; }
       // [JsonProperty]
      //  public string InstanceId { get; private set; }

        public InMemoryDataStore()
        {
          //  InstanceId = instanceId;
            Store = new ConcurrentDictionary<string, object>();
        }

        public T GetValue<T>(string key) where T : class 
        {
            Store.TryGetValue(key, out var value);
            return (T) value;
        }

        public T GetValue<T>(string key, T value)
        {
            var ret = Store.GetOrAdd(key, value);

            if (ret is JObject jobj)
            {
                ret = jobj.ToObject<T>();
            }
            return (T) ret;
        }

        public void PutValue<T>(string key, T value)
        {
            Store.AddOrUpdate(key, value, (s, o) => value);
        }

        public void Clear(string key)
        {
            Store.TryRemove(key, out var value);
        }

        public void ClearAll()
        {
           Store.Clear();
        }

        public ImmutableDictionary<string, object> GetAll()
        {
            return Store.ToImmutableDictionary();
        }
    }
}
