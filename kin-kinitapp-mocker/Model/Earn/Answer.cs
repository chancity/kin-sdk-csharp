using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Model.Earn
{
    public class Answer
    {
        [JsonProperty("id")]
        public string Id { get; private set; }
        [JsonProperty("text")]
        public string Text { get; private set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; private set; }
    }

    public static class AnswerEx
    {
        public static bool IsValid(this Answer answer)
        {
            return !string.IsNullOrEmpty(answer.Id) && 
                   (!string.IsNullOrEmpty(answer.Text) || !string.IsNullOrEmpty(answer.ImageUrl));
        }
    }
}
