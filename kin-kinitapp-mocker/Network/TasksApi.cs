using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using kin_kinit_mocker.Network.Model.Requests;
using kin_kinit_mocker.Network.Model.Responses;
using Refit;

namespace kin_kinit_mocker.Network
{
    interface TasksApi
    {
        [Post("/user/task/results")]
        Task <TaskSubmitResponse> SubmitTaskResults([Header(ServiceCommonData.USER_HEADER_KEY)] string userId, [Body] SubmitInfoRequest submitInfoRequest);
        [Get("/user/tasks")]
        Task<NextTasksResponse> NextTasks([Header(ServiceCommonData.USER_HEADER_KEY)] string userId, [Body] SubmitInfoRequest submitInfoRequest);
        [Get("/truex/activity")]
        Task<TrueXResponse> TruexActivity([Header(ServiceCommonData.USER_HEADER_KEY)] string userId, [Query("user-agent")] string agent);
    }
}
