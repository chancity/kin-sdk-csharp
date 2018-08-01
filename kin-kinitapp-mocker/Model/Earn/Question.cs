using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Model.Earn
{
    public class Question
    {
        public const string TYPE_TEXT = "text";
        public const string TEXT_IMAGE = "textimage";
        public const string TEXT_DUAL_IMAGE = "dual_image";
        public const string TEXT_EMOJI = "textemoji";
        public const string TEXT_MULTIPLE = "textmultiple";

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty("results")]
        public List<Answer> Answers { get; set; }

        public Question()
        {
            Id = null;
            Text = null;
            ImageUrl = null;
            Answers = null;
        }
    }

    public static class QuestionEx
    {
        public static bool IsValid(this Question question)
        {
            if (question.Id.IsNullOrBlank() && question.Text.IsNullOrBlank() ||
                question.Type.IsNullOrBlank() || question.Answers?.Count == 0)
            {
                return false;
            }

            return question.Answers != null && question.Answers.TrueForAll(a => a.IsValid());
        }

        public static bool IsTypeDualImage (this Question question) => 
            question.Type == Question.TEXT_DUAL_IMAGE;

        public static bool HasImages(this Question question) =>
            question.Type == Question.TEXT_IMAGE || question.Type == Question.TEXT_DUAL_IMAGE;

        public static ImmutableList<string> GetImagesUrls(this Question question)
        {
            ImmutableList<string> urls = question.Answers?
                .FindAll(it => !it.ImageUrl.IsNullOrBlank())
                .Select(it => it.ImageUrl)
                .ToImmutableList();

            if (!question.ImageUrl.IsNullOrBlank())
            {
                urls?.Add(question.ImageUrl);
            }
            
            return urls;
        }
       
    }
}
