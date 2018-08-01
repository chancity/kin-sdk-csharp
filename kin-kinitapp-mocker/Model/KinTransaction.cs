using Newtonsoft.Json;

namespace kin_kinit_mocker.Model
{
    public class TxInfo
    {
        [JsonProperty("memo")]
        public string Memo { get; private set; }

        [JsonProperty("task_id")]
        public string Id { get; private set; }
    }

    public class KinTransaction
    {
        [JsonProperty("tx_hash")]
        public string TxHash { get; private set; }

        [JsonProperty("title")]
        public string Title { get; private set; }

        [JsonProperty("desc")]
        public string Description { get; private set; }

        [JsonProperty("amount")]
        public int? Amount { get; private set; }

        [JsonProperty("type")]
        public string Type { get; private set; }

        [JsonProperty("client_received")]
        public bool? ClientReceived { get; private set; }

        [JsonProperty("date")]
        public long? Date { get; private set; }

        [JsonProperty("provider")]
        public Provider? Provider { get; private set; }

        [JsonProperty("tx_info")]
        public TxInfo TxInfo { get; private set; }

        public int? TxBalance { get; } = null;
    }

    public static class KinTransactionEx
    {
        public static bool IsValid(this TxInfo txInfo)
        {
            return !(txInfo.Memo.IsNullOrBlank() || txInfo.Id.IsNullOrBlank());
        }

        public static bool IsValid(this KinTransaction kinTransaction)
        {
            if (kinTransaction.TxHash.IsNullOrBlank() || !kinTransaction.Amount.HasValue ||
                kinTransaction.Type.IsNullOrBlank() || !kinTransaction.ClientReceived.HasValue || 
                !kinTransaction.Date.HasValue || !kinTransaction.Provider.HasValue || 
                kinTransaction.TxInfo == null)
            {
                return false;
            }

            return kinTransaction.Provider.Value.IsValid() && kinTransaction.TxInfo.IsValid();
        }
    }
}