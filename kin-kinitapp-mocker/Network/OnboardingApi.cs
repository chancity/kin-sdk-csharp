using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using kin_kinit_mocker.Network.Model;
using kin_kinit_mocker.Network.Model.Requests;
using kin_kinit_mocker.Network.Model.Responses;
using Refit;

namespace kin_kinit_mocker.Network
{
    internal interface OnboardingApi
    {
        [Post("/user/register")]
        Task<StatusConfigResponse> Register([Body] RegistrationInfoRequest registrationInfoRequestBody);
        [Post("/user/app-launch")]
        Task<StatusConfigResponse> AppLaunch([Header(ServiceCommonData.USER_HEADER_KEY)] string userId, [Body] AppVersionRequest appVersionRequestBody);
        [Post("/user/update-token")]
        Task<StatusConfigResponse> UpdateToken([Header(ServiceCommonData.USER_HEADER_KEY)] string userId, [Body] TokenInfoRequest tokenInfoRequestRequestBody);
        [Post("/user/onboard")]
        Task<StatusResponse> OnBoard([Header(ServiceCommonData.USER_HEADER_KEY)] string userId, [Body] AccountInfoRequest accountInfoRequestBody);
        [Post("/user/auth/ack")]
        Task<StatusConfigResponse> AuthTokenAck([Header(ServiceCommonData.USER_HEADER_KEY)] string userId, [Body] TokenInfoRequest tokenInfoRequestBody);
    }
}
