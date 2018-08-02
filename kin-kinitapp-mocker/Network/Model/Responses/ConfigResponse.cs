using System;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Network.Model.Responses
{
    internal class ConfigResponse
    {
        [JsonProperty("auth_token_enabled")]
        public bool AuthTokenEnabled { get; private set; }
        [JsonProperty("p2p_enabled")]
        public bool P2PEnabled { get; private set; }
        [JsonProperty("p2p_max_kin")]
        public int P2PMaxKin { get; private set; }
        [JsonProperty("p2p_min_kin")]
        public int P2PMinKin { get; private set; }
        [JsonProperty("p2p_min_tasks")]
        public int P2PMinTasks { get; private set; }
        [JsonProperty("phone_verification_enabled")]
        public bool PhoneVerificationEnabled { get; private set; }
        [JsonProperty("tos")]
        public string Tos { get; private set; }
    }
}