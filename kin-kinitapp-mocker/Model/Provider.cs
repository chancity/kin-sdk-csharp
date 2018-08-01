using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Model
{
    public struct Provider
    {
        [JsonProperty("name")]
        public string Name { get; private set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; private set; }
    }

    public static class ProviderEx
    {
        public static bool IsValid(this Provider provider)
        {
            return !string.IsNullOrEmpty(provider.ImageUrl) && !string.IsNullOrEmpty(provider.ImageUrl);
        }
    }
}
