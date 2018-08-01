using Newtonsoft.Json;

namespace kin_kinit_mocker.Network.Model.Requests
{
    internal struct AppVersionRequest
    {
        [JsonProperty("app_ver")]
        public string Version { get; private set; }

        public AppVersionRequest(string ver)
        {
            Version = ver;
        }
    }
}