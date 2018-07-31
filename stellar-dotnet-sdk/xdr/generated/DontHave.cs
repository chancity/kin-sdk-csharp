// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten

namespace stellar_dotnet_sdk.xdr
{
// === xdr source ============================================================

//  struct DontHave
//  {
//      MessageType type;
//      uint256 reqHash;
//  };

//  ===========================================================================
    public class DontHave
    {
        public MessageType Type { get; set; }
        public Uint256 ReqHash { get; set; }

        public static void Encode(XdrDataOutputStream stream, DontHave encodedDontHave)
        {
            MessageType.Encode(stream, encodedDontHave.Type);
            Uint256.Encode(stream, encodedDontHave.ReqHash);
        }

        public static DontHave Decode(XdrDataInputStream stream)
        {
            var decodedDontHave = new DontHave();
            decodedDontHave.Type = MessageType.Decode(stream);
            decodedDontHave.ReqHash = Uint256.Decode(stream);
            return decodedDontHave;
        }
    }
}