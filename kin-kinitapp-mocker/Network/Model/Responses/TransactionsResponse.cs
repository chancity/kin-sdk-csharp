using System.Collections.Generic;
using kin_kinit_mocker.Model;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Network.Model.Responses
{
    internal class TransactionsResponse : StatusResponse
    {
        [JsonProperty("txs")]
        public List<KinTransaction> Transactions { get; private set; }
    }
}