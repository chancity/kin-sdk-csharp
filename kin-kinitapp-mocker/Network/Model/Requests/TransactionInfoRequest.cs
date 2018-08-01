using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Network.Model.Requests
{
    internal struct TransactionInfoRequest
    {
        [JsonProperty("tx_hash")]
        public string TxHash { get; private set; }
        [JsonProperty("destination_address")]
        public string Address { get; private set; }
        [JsonProperty("amount")]
        public int Amount { get; private set; }

        public TransactionInfoRequest(string txHash, string address, int amount)
        {
            TxHash = txHash;
            Address = address;
            Amount = amount;
        }
    }
}
