using Newtonsoft.Json;
using System;
using System.Globalization;

namespace kin_kinit_mocker.Network.Model.Requests
{
    internal class RegistrationInfoRequest
    {
        [JsonProperty("user_id")]
        public string userId { get; set; }
        [JsonProperty("os")]
        public string os { get; set; } = "android";
        [JsonProperty("device_model")]
        public string deviceModel { get; set; } = "Samsung9";
        [JsonProperty("time_zone")]
        public string timeZone { get; set; }
        [JsonProperty("device_id")]
        public string deviceId { get; set; } = Guid.NewGuid().ToString().ToUpper();
        [JsonProperty("app_ver")]
        public string appVersion { get; set; } = "1.0.9";
        [JsonProperty("screen_w")]
        public int screenWidth { get; set; }
        [JsonProperty("screen_h")]
        public int screenHeight { get; set; }
        [JsonProperty("density")]
        public int density { get; set; }

        public RegistrationInfoRequest(string userId)
        {
            this.userId = userId;
            var timeDiff = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
            var sign = timeDiff.Hours > 0 ? "+" : "-";
            timeZone = sign + timeDiff.ToString("hh\\:mm");
        }
    }
}