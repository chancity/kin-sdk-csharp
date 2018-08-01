using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Network.Model.Responses
{
    internal class TaskSubmitResponse
    {
        [JsonProperty("tx_id")]
        public string TxId { get; private set; }
    }
}
