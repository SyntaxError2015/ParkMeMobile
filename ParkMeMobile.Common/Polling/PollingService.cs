using System;
using System.Threading.Tasks;
using ParkMeMobile.Common.Constants;
using ParkMeMobile.Common.Http;

namespace ParkMeMobile.Common.Polling
{
    public class PollingService<T>
    {
        private static readonly TimeSpan mTimerInterval = TimeSpan.FromSeconds(1);

        private readonly Timer mTimer;
        private readonly Action<T> mCallback;

        public PollingService(Action<T> callback)
        {
            mTimer = new Timer(mTimerInterval, PollData);
            mCallback = callback;
        }

        public bool IsTimerBusy => mTimer.IsBusy;

        public void StartTimer()
        {
            mTimer.Start();
        }

        public void StopTimer()
        {
            mTimer.Stop();
        }

        public void ResetTimer()
        {
            if (!mTimer.IsBusy)
            {
                mTimer.Reset();
            }
        }

        #region Private methods

        private async Task PollData()
        {
            try
            {
                var data = await HttpClient.Get<T>(HttpConstants.ENDPOINT_GET_ALL_PARKS);

                mCallback.Invoke(data);
            }
            catch (Exception e)
            {
                
            }
        }

#endregion
    }
}
