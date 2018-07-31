// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten

namespace stellar_dotnet_sdk.xdr
{
// === xdr source ============================================================

//  union BucketEntry switch (BucketEntryType type)
//  {
//  case LIVEENTRY:
//      LedgerEntry liveEntry;
//  
//  case DEADENTRY:
//      LedgerKey deadEntry;
//  };

//  ===========================================================================
    public class BucketEntry
    {
        public BucketEntryType Discriminant { get; set; } = new BucketEntryType();

        public LedgerEntry LiveEntry { get; set; }
        public LedgerKey DeadEntry { get; set; }

        public static void Encode(XdrDataOutputStream stream, BucketEntry encodedBucketEntry)
        {
            stream.WriteInt((int) encodedBucketEntry.Discriminant.InnerValue);
            switch (encodedBucketEntry.Discriminant.InnerValue)
            {
                case BucketEntryType.BucketEntryTypeEnum.LIVEENTRY:
                    LedgerEntry.Encode(stream, encodedBucketEntry.LiveEntry);
                    break;
                case BucketEntryType.BucketEntryTypeEnum.DEADENTRY:
                    LedgerKey.Encode(stream, encodedBucketEntry.DeadEntry);
                    break;
            }
        }

        public static BucketEntry Decode(XdrDataInputStream stream)
        {
            var decodedBucketEntry = new BucketEntry();
            var discriminant = BucketEntryType.Decode(stream);
            decodedBucketEntry.Discriminant = discriminant;
            switch (decodedBucketEntry.Discriminant.InnerValue)
            {
                case BucketEntryType.BucketEntryTypeEnum.LIVEENTRY:
                    decodedBucketEntry.LiveEntry = LedgerEntry.Decode(stream);
                    break;
                case BucketEntryType.BucketEntryTypeEnum.DEADENTRY:
                    decodedBucketEntry.DeadEntry = LedgerKey.Decode(stream);
                    break;
            }
            return decodedBucketEntry;
        }
    }
}