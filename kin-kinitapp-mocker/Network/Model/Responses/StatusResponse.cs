using Newtonsoft.Json;

namespace kin_kinit_mocker.Network.Model.Responses
{
    internal class StatusResponse
    {
        [JsonProperty("status")]
        public string Status { get; private set; }
    }
    internal class StatusConfigResponse : StatusResponse
    {
        [JsonProperty("config")]
        public ConfigResponse Config { get; private set; }
    }
}