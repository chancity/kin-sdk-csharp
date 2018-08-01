using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Network.Model.Requests
{
    internal struct OfferInfoRequest
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public OfferInfoRequest(string id)
        {
            Id = id;
        }
    }
}
