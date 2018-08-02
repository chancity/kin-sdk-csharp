using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using stellar_dotnet_sdk;

namespace kin_kinit_mocker.Model
{
    public class UserInfo
    {
        private string _addressSeed;
        public string Id { get; set; }
        public string PublicAddress => KeyPair.FromSecretSeed(AddressSeed).Address;
        [JsonIgnore]
        public KeyPair KayPair => KeyPair.FromSecretSeed(AddressSeed);

        [JsonProperty]
        private string AddressSeed {
            get
            {
                if (string.IsNullOrEmpty(_addressSeed))
                {
                    _addressSeed = KeyPair.Random().SecretSeed;
                }

                return _addressSeed;
            }
            set
            {
                if (string.IsNullOrEmpty(_addressSeed))
                {
                    _addressSeed = value;
                }
            }
        }

        [JsonConstructor]
        private UserInfo() { }

        public UserInfo(string userId)
        {
            Id = userId;
        }
    }
}
