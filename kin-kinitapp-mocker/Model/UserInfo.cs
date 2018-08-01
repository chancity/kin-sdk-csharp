using System;
using System.Collections.Generic;
using System.Text;

namespace kin_kinit_mocker.Model
{
    public class UserInfo
    {
        public string UserId { get; set; }
        public string PublicAddress { get; set; }

        public UserInfo(string userId)
        {
            UserId = userId;
        }
    }
}
