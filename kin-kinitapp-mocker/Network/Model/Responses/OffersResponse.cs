using System;
using System.Collections.Generic;
using System.Text;
using kin_kinit_mocker.Model.Spend;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Network.Model.Responses
{
    internal class OffersResponse
    {
        [JsonProperty("offers")]
        public List<Offer> OfferList { get; private set; }
    }
}
