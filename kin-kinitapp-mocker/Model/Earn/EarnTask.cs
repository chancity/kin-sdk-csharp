using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Model.Earn
{
    public class EarnTask
    {
        public const string TASK_TYPE_QUESTIONNAIRE = "questionnaire";
        public const string TASK_TYPE_TRUEX = "truex";

        [JsonProperty("id")]
        public string Id { get; private set; }
        [JsonProperty("memo")]
        public string Memo { get; private set; }
        [JsonProperty("title")]
        public string Title { get; private set; }
        [JsonProperty("desc")]
        public string Description { get; private set; }
        [JsonProperty("price")]
        public int? KinReward { get; private set; }
        [JsonProperty("start_date")]
        public long? StartDateInSeconds { get; private set; }
        [JsonProperty("min_client_version_android")]
        public string MinClientVersionAndroid { get; set; }
        [JsonProperty("min_client_version_ios")]
        public string MinClientVersionIos { get; set; }
        [JsonProperty("min_to_complete")]
        public float? MinToComplete { get; private set; }
        [JsonProperty("tags")]
        public List<string> Tags { get; private set; }
        [JsonProperty("provider")]
        public Provider? Provider { get; private set; }
        [JsonProperty("type")]
        public string Type { get; private set; }
        [JsonProperty("updated_at")]
        public long? LastUpdatedAt { get; private set; }
        [JsonProperty("items")]
        public List<Question> Questions { get; private set; }


        [JsonConstructor]
        public EarnTask()
        {
            Questions = null;
            LastUpdatedAt = null;
            Type = null;
            Provider = null;
            Tags = null;
            MinToComplete = null;
            StartDateInSeconds = 0;
            KinReward = null;
            Description = null;
            Title = null;
            Memo = null;
            Id = null;
        }

    }

    public static class EarnTaskEx
    {
        public static bool IsValid(this EarnTask task)
        {
            if (task.Id.IsNullOrBlank() || task.Title.IsNullOrBlank() ||
                task.Description.IsNullOrBlank() || task.Type.IsNullOrBlank() || 
                task.Memo.IsNullOrBlank() || 
                !task.KinReward.HasValue || !task.MinToComplete.HasValue || 
                !task.Provider.HasValue || task.Questions == null || 
                !task.StartDateInSeconds.HasValue || !task.LastUpdatedAt.HasValue)
            {
                return false;
            }
            return task.Questions.TrueForAll(question => question.IsValid()) 
                   && task.Provider.Value.IsValid();
        }

        public static bool IsQuestionnaire(this EarnTask task) =>
            task.Type == EarnTask.TASK_TYPE_QUESTIONNAIRE;

        public static bool IsTaskWebView(this EarnTask task) =>
            task.Type == EarnTask.TASK_TYPE_TRUEX;

        public static string Tags(this EarnTask task) =>
            string.Join(",", task.Tags);

        public static long? StartDateInMillis(this EarnTask task) => task.StartDateInSeconds * 1000;
    }
}
