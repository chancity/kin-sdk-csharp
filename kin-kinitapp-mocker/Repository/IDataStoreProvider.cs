using System;
using System.Collections.Generic;
using System.Text;

namespace kin_kinit_mocker.Repository
{
    public interface IDataStoreProvider
    {
        IDataStore DataStore(string storage);
    }
}
