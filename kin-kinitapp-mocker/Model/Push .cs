using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Model
{
    public class Push
    {
        public const string TYPE_TX_COMPLETED = "tx_completed";
        public const string TYPE_ENGAGEMENT = "engage-recent";
        public const string TYPE_AUTH_TOKEN = "auth_token";
        public const string TYPE_REGISTER = "register";
        public const string ID_DATA_KEY = "push_id";
        public const string TYPE_DATA_KEY = "push_type";
        public const string MESSAGE_DATA_KEY = "message";

        public class NotificationMessage
        {
            [JsonProperty("body")]
            public string Body { get; private set; } = null;
            [JsonProperty("title")]
            public string Title { get; private set; } = null;

        }

        public class AuthTokenMessage
        {
            [JsonProperty("user_id")]
            public string UserId { get; private set; } = null;
            [JsonProperty("type")]
            public string Type { get; private set; } = null;
            [JsonProperty("token")]
            public string AuthToken { get; private set; } = null;
        }

        public class TransactionCompleteMessage
        {
            [JsonProperty("kin")]
            public int? Kin { get; private set; } = null;
            [JsonProperty("user_id")]
            public string UserId { get; private set; } = null;
            [JsonProperty("task_id")]
            public string TaskId { get; private set; } = null;
            [JsonProperty("tx_hash")]
            public string TxHash { get; private set; } = null;
            [JsonProperty("type")]
            public string Type { get; private set; } = null;
        }
    }
}
