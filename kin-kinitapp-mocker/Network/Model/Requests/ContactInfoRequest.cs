using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Network.Model.Requests
{
    public struct ContactInfoRequest
    {
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        public ContactInfoRequest(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
