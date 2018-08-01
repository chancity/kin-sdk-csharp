using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace kin_kinit_mocker.Network.Model.Responses
{
    internal class TrueXResponse : StatusResponse
    {
        [JsonProperty("activity")]
        public JObject Activity { get; private set; }
    }
}
