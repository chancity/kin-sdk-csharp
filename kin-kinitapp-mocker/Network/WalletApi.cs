using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using kin_kinit_mocker.Network.Model.Requests;
using kin_kinit_mocker.Network.Model.Responses;
using Refit;

namespace kin_kinit_mocker.Network
{
    internal interface WalletApi
    {
        [Get("/user/redeemed")]
        Task<CouponsResponse> GetCoupons([Header(ServiceCommonData.USER_HEADER_KEY)] string userId);
        [Get("/user/transactions")]
        Task<TransactionsResponse> GetTransactions([Header(ServiceCommonData.USER_HEADER_KEY)] string userId);
    }
}
