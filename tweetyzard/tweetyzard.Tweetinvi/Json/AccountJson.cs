﻿using System;
using TweetinviControllers.Account;

namespace Tweetinvi.Json
{
    public static class AccountJson
    {
        [ThreadStatic]
        private static IAccountJsonController _accountJsonController;
        public static IAccountJsonController AccountJsonController
        {
            get
            {
                if (_accountJsonController == null)
                {
                    Initialize();
                }

                return _accountJsonController;
            }
        }
        
        static AccountJson()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _accountJsonController = TweetinviContainer.Resolve<IAccountJsonController>();
        }

        public static string GetLoggedUserSettingsJson()
        {
            return AccountJsonController.GetLoggedUserSettingsJson();
        }
    }
}