using System;
using System.Collections.Generic;
using System.Text;
using kin_kinit_mocker.Repository;

namespace kin_kinit_mocker.Modules
{
    public class DataStoreProviderModule : IDataStoreProvider
    {
        private readonly IDataStoreProvider _dataStoreProvider;
        public DataStoreProviderModule(IDataStoreProvider dataStoreProvider)
        {
            _dataStoreProvider = dataStoreProvider;
        }

        public IDataStore DataStore(string storage)
        {
            return _dataStoreProvider.DataStore(storage);
        }
    }
}
