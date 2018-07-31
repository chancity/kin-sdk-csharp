// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten

namespace stellar_dotnet_sdk.xdr
{
// === xdr source ============================================================

//  struct TrustLineEntry
//  {
//      AccountID accountID; // account this trustline belongs to
//      Asset asset;         // type of asset (with issuer)
//      int64 balance;       // how much of this asset the user has.
//                           // Asset defines the unit for this;
//  
//      int64 limit;  // balance cannot be above this
//      uint32 flags; // see TrustLineFlags
//  
//      // reserved for future use
//      union switch (int v)
//      {
//      case 0:
//          void;
//      }
//      ext;
//  };

//  ===========================================================================
    public class TrustLineEntry
    {
        public AccountID AccountID { get; set; }
        public Asset Asset { get; set; }
        public Int64 Balance { get; set; }
        public Int64 Limit { get; set; }
        public Uint32 Flags { get; set; }
        public TrustLineEntryExt Ext { get; set; }

        public static void Encode(XdrDataOutputStream stream, TrustLineEntry encodedTrustLineEntry)
        {
            AccountID.Encode(stream, encodedTrustLineEntry.AccountID);
            Asset.Encode(stream, encodedTrustLineEntry.Asset);
            Int64.Encode(stream, encodedTrustLineEntry.Balance);
            Int64.Encode(stream, encodedTrustLineEntry.Limit);
            Uint32.Encode(stream, encodedTrustLineEntry.Flags);
            TrustLineEntryExt.Encode(stream, encodedTrustLineEntry.Ext);
        }

        public static TrustLineEntry Decode(XdrDataInputStream stream)
        {
            var decodedTrustLineEntry = new TrustLineEntry();
            decodedTrustLineEntry.AccountID = AccountID.Decode(stream);
            decodedTrustLineEntry.Asset = Asset.Decode(stream);
            decodedTrustLineEntry.Balance = Int64.Decode(stream);
            decodedTrustLineEntry.Limit = Int64.Decode(stream);
            decodedTrustLineEntry.Flags = Uint32.Decode(stream);
            decodedTrustLineEntry.Ext = TrustLineEntryExt.Decode(stream);
            return decodedTrustLineEntry;
        }

        public class TrustLineEntryExt
        {
            public int Discriminant { get; set; }

            public static void Encode(XdrDataOutputStream stream, TrustLineEntryExt encodedTrustLineEntryExt)
            {
                stream.WriteInt(encodedTrustLineEntryExt.Discriminant);
                switch (encodedTrustLineEntryExt.Discriminant)
                {
                    case 0:
                        break;
                }
            }

            public static TrustLineEntryExt Decode(XdrDataInputStream stream)
            {
                var decodedTrustLineEntryExt = new TrustLineEntryExt();
                var discriminant = stream.ReadInt();
                decodedTrustLineEntryExt.Discriminant = discriminant;
                switch (decodedTrustLineEntryExt.Discriminant)
                {
                    case 0:
                        break;
                }
                return decodedTrustLineEntryExt;
            }
        }
    }
}