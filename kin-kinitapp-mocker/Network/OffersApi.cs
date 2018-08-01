using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using kin_kinit_mocker.Network.Model.Requests;
using kin_kinit_mocker.Network.Model.Responses;
using Refit;

namespace kin_kinit_mocker.Network
{
    interface OffersApi
    {
        [Get("/user/offers")]
        Task<OffersResponse> Offers([Header(ServiceCommonData.USER_HEADER_KEY)] string userId);
        [Post("/offer/book")]
        Task<BookOfferResponse> BookOffer([Header(ServiceCommonData.USER_HEADER_KEY)] string userId, [Body] OfferInfoRequest offerInfoRequest);
        [Post("/user/contact")]
        Task<ContactResponse> SendContact([Header(ServiceCommonData.USER_HEADER_KEY)] string userId, [Body] ContactInfoRequest contactInfoRequest);
        [Post("/user/transaction/p2p")]
        Task<StatusResponse> SendTransactionInfo([Header(ServiceCommonData.USER_HEADER_KEY)] string userId, [Body] TransactionInfoRequest transactionInfoRequest);
        [Post("/user/transaction/p2p")]
        Task<RedeemResponse> RedeemOffer([Header(ServiceCommonData.USER_HEADER_KEY)] string userId, [Body] PaymentReceiptRequest paymentReceiptRequest);
    }
}
