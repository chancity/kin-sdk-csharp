using Newtonsoft.Json;

namespace kin_kinit_mocker.Network.Model.Requests {
    internal struct AccountInfoRequest
    {
        [JsonProperty("public_address")]
        public string PublicAddress { get; private set; }

        public AccountInfoRequest(string token)
        {
            PublicAddress = token;
        }
    }
}