using System;
using kin_kinit_mocker.Model;

namespace kin_kinit_mocker.Repository
{
    public class UserRepository
    {
        private const string USER_ID_KEY = "user_id";
        private const string IS_REGISTERED_KEY = "is_registered";
        private const string IS_FRESH_INSTALL = "is_fresh_install";
        private const string FCM_TOKEN_SENT_KEY = "token_sent";
        private const string USER_CACHE_NAME = "kin.app.user";
        private const string TOS = "tos";
        private const string PHONE_VERIFICATION_ENABLED = "PHONE_VERIFICATION_ENABLED";
        private const string PHONE_VERIFIED = "PHONE_VERIFIED";
        private const string FIRST_TIME_USER = "FIRST_TIME_USER";
        private const string P2P_MAX_KIN = "P2P_MAX_KIN";
        private const string P2P_MIN_KIN = "P2P_MIN_KIN";
        private const string P2P_MIN_TASKS = "P2P_MIN_TASKS";
        private const string P2P_ENABLED = "P2P_ENABLED";

        private readonly IDataStore _userCache;
        private readonly UserInfo _userInfo;

        public bool FcmTokenSent
        {
            get => _userCache.GetValue(FCM_TOKEN_SENT_KEY, false);
            set => _userCache.PutValue(FCM_TOKEN_SENT_KEY, value);
        }

        public bool IsRegistered
        {
            get => _userCache.GetValue(IS_REGISTERED_KEY, false);
            set => _userCache.PutValue(IS_REGISTERED_KEY, value);
        }
        public bool IsPhoneVerified
        {
            get => _userCache.GetValue(PHONE_VERIFIED, false);
            set => _userCache.PutValue(PHONE_VERIFIED, value);
        }
        public bool IsFreshInstall
        {
            get => _userCache.GetValue(IS_FRESH_INSTALL, true);
            set => _userCache.PutValue(IS_FRESH_INSTALL, value);
        }
        public string Tos
        {
            get => _userCache.GetValue(TOS, "");
            set => _userCache.PutValue(TOS, value);
        }
        public bool IsPhoneVerificationEnabled
        {
            get => _userCache.GetValue(PHONE_VERIFICATION_ENABLED, false);
            set => _userCache.PutValue(PHONE_VERIFICATION_ENABLED, value);
        }
        public bool IsFirstTimeUser
        {
            get => _userCache.GetValue(FIRST_TIME_USER, false);
            set => _userCache.PutValue(FIRST_TIME_USER, value);
        }
        public int P2PMaxKin
        {
            get => _userCache.GetValue(P2P_MAX_KIN, 0);
            set => _userCache.PutValue(P2P_MAX_KIN, value);
        }
        public int P2PMinKin
        {
            get => _userCache.GetValue(P2P_MIN_KIN, 0);
            set => _userCache.PutValue(P2P_MIN_KIN, value);
        }
        public int P2PMinTasks
        {
            get => _userCache.GetValue(P2P_MIN_TASKS, 0);
            set => _userCache.PutValue(P2P_MIN_TASKS, value);
        }
        public bool IsP2PEnabled
        {
            get => _userCache.GetValue(P2P_ENABLED, false);
            set => _userCache.PutValue(P2P_ENABLED, value);
        }
        public UserRepository(IDataStoreProvider dataStoreProvider)
        {
            _userCache = dataStoreProvider.DataStore(USER_CACHE_NAME);

            string userId = _userCache.GetValue(USER_ID_KEY, "");

            if (string.IsNullOrEmpty(userId))
            {
                userId = Guid.NewGuid().ToString("N");
                _userCache.PutValue(USER_ID_KEY, userId);
            }

            _userInfo = new UserInfo(userId);
        }

        public string UserId => _userInfo.UserId;
    }
}