using System;
using System.Collections.Generic;
using System.Text;
using kin_kinit_mocker.Model.Earn;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Network.Model.Responses
{
    internal class NextTasksResponse
    {
        [JsonProperty("tasks")]
        public List<EarnTask> Tasks { get; private set; }
    }
}
