using RelayApi.Interfaces;
using System;
using System.Threading;

namespace RelayApi
{
    public class Manager : IManager
    {
        private bool shouldRun;
        private readonly IErrorMessageHandler errorHandler;

        public Manager(IErrorMessageHandler errorHandler)
        {
            this.errorHandler = errorHandler;
        }

        public void Start()
        {
            shouldRun = true;
            new Thread(RunAcceptLoop).Start();
        }

        public void Stop()
        {
            shouldRun = false;
        }

        private void RunAcceptLoop()
        {
            while (shouldRun)
            {
                try
                {
                    Process();
                    Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    errorHandler.StoreError(ex, null);
                    Thread.Sleep(5000);
                }
            }
        }

        private static void Process()
        {
            throw new NotImplementedException("Replace this line with some process.");
        }
    }
}
