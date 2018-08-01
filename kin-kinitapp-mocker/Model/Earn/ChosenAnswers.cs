using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Model.Earn
{
    public class ChosenAnswers
    {
        [JsonProperty("qid)")]
        public string QuestionId { get; set; }
        [JsonProperty("aid)")]
        public List<string> AnswersIds { get; set; }


    }
}
