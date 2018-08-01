using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using kin_kinit_mocker.Network.Model.Requests;
using kin_kinit_mocker.Network.Model.Responses;
using Refit;

namespace kin_kinit_mocker.Network
{
    interface PhoneAuthenticationApi
    {
        [Post("/user/firebase/update-id-token")]
        Task<StatusConfigResponse> IpdatePhoneAuthToken([Header(ServiceCommonData.USER_HEADER_KEY)] string userId, [Body] TokenInfoRequest tokenInfoRequestBody);
    }
}
