using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;

namespace kin_kinit_mocker
{
    public static class KinAccountActivator
    {
        private static string TRUST_NO_LIMIT_VALUE = "922337203685.4775807";
        private static string MAIN_NETWORK_ISSUER = "GDF42M3IPERQCBLWFEZKQRK77JQ65SCKTU3CW36HZVCX7XX5A5QXZIVK";
        public static  string NETWORK_ID_MAIN = "Public Global Kin Ecosystem Network ; June 2018";
        private static Server _server;
        private static stellar_dotnet_sdk.Asset _kinAsset;
        static KinAccountActivator()
        {
            _kinAsset = Asset.CreateNonNativeAsset("KIN", KeyPair.FromAccountId(MAIN_NETWORK_ISSUER));
            _server = new Server("https://horizon-kin-ecosystem.kininfrastructure.com/");
            stellar_dotnet_sdk.Network.UsePublicNetwork();
            stellar_dotnet_sdk.Network.Use(new stellar_dotnet_sdk.Network(NETWORK_ID_MAIN));
        }

        public static async Task<bool> Activate(this KeyPair account)
        {
            AccountResponse accountResponse;
            try
            {
                accountResponse = await GetAccount(account);

                if (!HasKinAsset(accountResponse))
                {
                    SubmitTransactionResponse response = await SendAllowKinTrustOperation(account, accountResponse).ConfigureAwait(false);
                    return response != null;
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private static async Task<SubmitTransactionResponse> SendAllowKinTrustOperation(KeyPair account, AccountResponse accountResponse)
        {
            AllowTrustOperation.Builder allowTrustOperationBuilder =
                new AllowTrustOperation.Builder(account, "KIN", true).SetSourceAccount(account);
            
            ChangeTrustOperation.Builder changeTrustOperationBuilder = new ChangeTrustOperation.Builder((AssetTypeCreditAlphaNum)_kinAsset,
                                                                                                        TRUST_NO_LIMIT_VALUE).SetSourceAccount(account);

            ChangeTrustOperation changeTrustOperation = changeTrustOperationBuilder.Build();

            Transaction.Builder allowKinTrustTransaction =
                new Transaction.Builder(new Account(account, accountResponse.SequenceNumber)).AddOperation(changeTrustOperation);


            allowKinTrustTransaction.AddMemo(new MemoText("1-chancity"));

            Transaction transaction = allowKinTrustTransaction.Build();
            transaction.Sign(account);
            return await _server.SubmitTransaction(transaction).ConfigureAwait(false);
        }
        private static bool HasKinAsset(AccountResponse account)
        {
            foreach (Balance accountBalance in account.Balances)
            {
                if (accountBalance.AssetCode != null && accountBalance.AssetCode.Equals("KIN"))
                {
                    return true;
                }
            }

            return false;
        }
        private static async Task<AccountResponse> GetAccount(KeyPair account)
        {
            AccountResponse accountResponse;
            accountResponse = await _server.Accounts.Account(account).ConfigureAwait(false);
            
            if (accountResponse == null)
            {
                throw new Exception("can't retrieve data for account " + account.AccountId);
            }

            return accountResponse;
        }
    }
}
