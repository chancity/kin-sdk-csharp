using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Network.Model.Responses
{
    internal class GoodResponse
    {
        [JsonProperty("type")]
        public string Type { get; private set; }
        [JsonProperty("value")]
        public string Value { get; private set; }
    }
}
