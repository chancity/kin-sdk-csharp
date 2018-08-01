using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Network.Model.Responses
{
    internal class BookOfferResponse : StatusResponse
    {
        [JsonProperty("reason")]
        public string Reason { get; private set; }
        [JsonProperty("order_id")]
        public string OrderId { get; private set; }
}
}
