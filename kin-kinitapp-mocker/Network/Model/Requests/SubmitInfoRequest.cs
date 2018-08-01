using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using kin_kinit_mocker.Model.Earn;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Network.Model.Requests
{
    internal struct SubmitInfoRequest
    {
        [JsonProperty("id")]
        public string TaskId { get; private set; }
        [JsonProperty("results")]
        public List<ChosenAnswers> ChosenAnswersList { get; private set; }
        [JsonProperty("address")]
        public string PublicAddress { get; private set; }

        public SubmitInfoRequest(string taskId, string publicAddress, List<ChosenAnswers> chosenAnswersList)
        {
            TaskId = taskId;
            PublicAddress = publicAddress;
            ChosenAnswersList = chosenAnswersList;
        }
    }
}
