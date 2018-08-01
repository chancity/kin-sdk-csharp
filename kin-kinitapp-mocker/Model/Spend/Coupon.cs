using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Model.Spend
{
    public class Coupon
    {
        [JsonProperty("offer_id")]
        public string Id { get; private set; }

        [JsonProperty("title")]
        public string Title { get; private set; }

        [JsonProperty("desc")]
        public string Description { get; private set; }

        [JsonProperty("type")]
        public string Type { get; private set; }

        [JsonProperty("value")]
        public string Value { get; private set; }

        [JsonProperty("date")]
        public long? DateInSeconds { get; private set; }

        [JsonProperty("provider")]
        public Provider? Provider { get; private set; }
    }

    public static class CouponEx
    {
        public static bool IsValid(this Coupon coupon)
        {
            if (coupon.Id.IsNullOrBlank() || coupon.Title.IsNullOrBlank() ||
                coupon.Description.IsNullOrBlank() || coupon.Type.IsNullOrBlank() ||
                coupon.Value.IsNullOrBlank() || !coupon.DateInSeconds.HasValue ||
                !coupon.Provider.HasValue)
            {
                return false;
            }

            return coupon.Provider.Value.IsValid();
        }
    }
}
