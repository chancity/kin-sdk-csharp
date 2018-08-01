using Newtonsoft.Json;

namespace kin_kinit_mocker.Network.Model.Requests
{
    internal struct TokenInfoRequest
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
    }
}