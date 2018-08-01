using System;
using System.Collections.Generic;
using System.Text;

namespace kin_kinit_mocker.Model
{
    public enum TaskState
    {
        // new task
        IDLE = 0,
        // user has clicked on start or continue
        IN_PROGRESS = 8,
        // user finished the task and we submitted to server
        SUBMITTED = 7,
        // task submission was successful
        SUBMITTED_SUCCESS_WAIT_FOR_REWARD = 1,
        // there was an error when submitting the task
        // user can retry later
        SUBMIT_ERROR_RETRY = 2,
        // there was an unrecoverable submission error
        SUBMIT_ERROR_NO_RETRY = 3,
        // there was a transaction error
        TRANSACTION_ERROR = 4,
        // user received their Kin!
        TRANSACTION_COMPLETED = 6
    }
}
