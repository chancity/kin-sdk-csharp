using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;

namespace WalletActivator
{
    public static class KinWalletActivator
    {
        private static string TRUST_NO_LIMIT_VALUE = "922337203685.4775807";
        private static string MAIN_NETWORK_ISSUER = "GDF42M3IPERQCBLWFEZKQRK77JQ65SCKTU3CW36HZVCX7XX5A5QXZIVK";
        public static string NETWORK_ID_MAIN = "Public Global Kin Ecosystem Network ; June 2018";
        private static readonly Server Server;
        private static readonly Asset KinAsset;
        static KinWalletActivator()
        {
            KinAsset = Asset.CreateNonNativeAsset("KIN", KeyPair.FromAccountId(MAIN_NETWORK_ISSUER));
            Server = new Server("https://horizon-kin-ecosystem.kininfrastructure.com/");
            Network.UsePublicNetwork();
            Network.Use(new Network(NETWORK_ID_MAIN));
        }

        public static async Task<bool> Activate(this KeyPair account)
        {
            AccountResponse accountResponse = await GetAccount(account);

                if (!HasKinAsset(accountResponse))
                {
                    SubmitTransactionResponse response = await SendAllowKinTrustOperation(account, accountResponse).ConfigureAwait(false);
                    return response != null;
                }

                return true;
        }

        private static async Task<SubmitTransactionResponse> SendAllowKinTrustOperation(KeyPair account, AccountResponse accountResponse)
        {
            ChangeTrustOperation.Builder changeTrustOperationBuilder = new ChangeTrustOperation.Builder((AssetTypeCreditAlphaNum)KinAsset,
                                                                                                        TRUST_NO_LIMIT_VALUE).SetSourceAccount(account);

            ChangeTrustOperation changeTrustOperation = changeTrustOperationBuilder.Build();

            Transaction.Builder allowKinTrustTransaction =
                new Transaction.Builder(new Account(account, accountResponse.SequenceNumber)).AddOperation(changeTrustOperation);

            Transaction transaction = allowKinTrustTransaction.Build();
            transaction.Sign(account);
            return await Server.SubmitTransaction(transaction).ConfigureAwait(false);
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
            AccountResponse accountResponse = await Server.Accounts.Account(account).ConfigureAwait(false);

            if (accountResponse == null)
            {
                throw new Exception("can't retrieve data for account " + account.AccountId);
            }

            return accountResponse;
        }
    }
}
