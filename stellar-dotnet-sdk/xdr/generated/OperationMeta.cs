// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten

namespace stellar_dotnet_sdk.xdr
{
// === xdr source ============================================================

//  struct OperationMeta
//  {
//      LedgerEntryChanges changes;
//  };

//  ===========================================================================
    public class OperationMeta
    {
        public LedgerEntryChanges Changes { get; set; }

        public static void Encode(XdrDataOutputStream stream, OperationMeta encodedOperationMeta)
        {
            LedgerEntryChanges.Encode(stream, encodedOperationMeta.Changes);
        }

        public static OperationMeta Decode(XdrDataInputStream stream)
        {
            var decodedOperationMeta = new OperationMeta();
            decodedOperationMeta.Changes = LedgerEntryChanges.Decode(stream);
            return decodedOperationMeta;
        }
    }
}