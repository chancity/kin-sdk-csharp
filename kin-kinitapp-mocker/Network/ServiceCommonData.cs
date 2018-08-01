using System;
using System.Collections.Generic;
using System.Text;

namespace kin_kinit_mocker.Network
{
    internal class ServiceCommonData
    {
        public const int ERROR_NO_INTERNET = 1;
        public const int ERROR_NO_PUBLIC_ADDRESS = 2;
        public const int ERROR_EMPTY_RESPONSE = 3;
        public const int ERROR_APP_SERVER_FAILED_RESPONSE = 4;
        public const int ERROR_INVALID_DATA = 5;
        public const string USER_HEADER_KEY = "X-USERID";
    }
}
