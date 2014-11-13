using System;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.oAuth;

namespace TweetinviCredentials
{
    public class CredentialsAccessor : ICredentialsAccessor
    {

        public CredentialsAccessor()
        {
            CurrentThreadCredentials = StaticApplicationCredentials;
        }

        private static IOAuthCredentials StaticApplicationCredentials { get; set; }

        public IOAuthCredentials ApplicationCredentials
        {
            get { return StaticApplicationCredentials; } 
            set { StaticApplicationCredentials = value;  }
        }

        private IOAuthCredentials _currentThreadCredentials;
        public IOAuthCredentials CurrentThreadCredentials
        {
            get { return _currentThreadCredentials; }
            set
            {
                _currentThreadCredentials = value;

                if (!HasTheApplicationCredentialsBeenInitialized() && _currentThreadCredentials != null)
                {
                    StaticApplicationCredentials = value;
                }
            }
        }

        public T ExecuteOperationWithCredentials<T>(IOAuthCredentials credentials, Func<T> operation)
        {
            var initialCredentials = CurrentThreadCredentials;
            CurrentThreadCredentials = credentials;
            var result = operation();
            CurrentThreadCredentials = initialCredentials;
            return result;
        }

        public void ExecuteOperationWithCredentials(IOAuthCredentials credentials, Action operation)
        {
            var initialCredentials = CurrentThreadCredentials;
            CurrentThreadCredentials = credentials;
            operation();
            CurrentThreadCredentials = initialCredentials;
        }

        private bool HasTheApplicationCredentialsBeenInitialized()
        {
            return StaticApplicationCredentials != null;
        }
    }
}