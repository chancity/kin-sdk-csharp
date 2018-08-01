using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Network.Model.Requests
{
    internal class PaymentReceiptRequest
    {
        [JsonProperty("tx_hash")]
        public string TxHash { get; set; }

        public PaymentReceiptRequest(string txHash)
        {
            TxHash = txHash;
        }
    }
}
