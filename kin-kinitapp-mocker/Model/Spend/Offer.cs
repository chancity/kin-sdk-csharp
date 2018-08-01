using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Model.Spend
{
    public struct Offer
    {
        public const string TYPE_P2P = "p2p";

        [JsonProperty("id")]
        public string Id { get; private set; }
        [JsonProperty("type")]
        public string Type { get; private set; }
        [JsonProperty("domain")]
        public string Domain { get; private set; }
        [JsonProperty("title")]
        public string Title { get; private set; }
        [JsonProperty("description")]
        public string Description { get; private set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; private set; }
        [JsonProperty("type_image_url")]
        public string ImageTypeUrl { get; private set; }
        [JsonProperty("price")]
        public int? Price { get; private set; }
        [JsonProperty("address")]
        public string Address { get; private set; }
        [JsonProperty("provider")]
        public Provider? Provider { get; private set; }


    }

    public static class OfferEx
    {
        public static bool IsValid(this Offer offer)
        {
            if (string.IsNullOrEmpty(offer.Id) || string.IsNullOrEmpty(offer.Title) ||
                string.IsNullOrEmpty(offer.Description) || string.IsNullOrEmpty(offer.Type) ||
                string.IsNullOrEmpty(offer.Address) || string.IsNullOrEmpty(offer.ImageUrl) ||
                string.IsNullOrEmpty(offer.ImageTypeUrl) || string.IsNullOrEmpty(offer.Domain) ||
                offer.Price == null || offer.Provider == null)
            {
                return false;
            }

            return offer.Provider.Value.IsValid();
        }

        public static bool IsP2P(this Offer offer)
        {
            return offer.Type.Equals(Offer.TYPE_P2P);
        }
    }
}
