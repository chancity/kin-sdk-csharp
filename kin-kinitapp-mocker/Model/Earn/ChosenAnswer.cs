using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Model.Earn
{
    public class ChosenAnswer
    {
        [JsonProperty("qid)")]
        public string QuestionId { get; set; }
        [JsonProperty("aid)")]
        public List<string> AnswersIds { get; set; }

        public ChosenAnswer(string questionId, List<string> answersIds)
        {
            QuestionId = questionId;
            AnswersIds = answersIds;
        }

        public ChosenAnswer() { }
    }
}
