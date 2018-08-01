using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Network.Model.Responses
{
    internal class ContactResponse : StatusResponse
    {
        [JsonProperty("reason")]
        public string Reason { get; private set; }
        [JsonProperty("address")]
       public string Address { get; private set; }
    }
}
