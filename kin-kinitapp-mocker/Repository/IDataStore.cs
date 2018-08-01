using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace kin_kinit_mocker.Repository
{
    public interface IDataStore
    {
        T GetValue<T>(string key) where T : class;
        T GetValue<T>(string key, T value);
        void PutValue<T>(string key, T value);
        void Clear(string key);
        void ClearAll();
        ImmutableDictionary<string, object> GetAll();
    }
}
