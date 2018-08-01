using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Network.Model.Responses
{
    internal class RedeemResponse
    {
        [JsonProperty("goods")]
        public List<GoodResponse> Goods { get; private set; }
        [JsonProperty("status")]
        public string Status { get; private set; }
    }
}
