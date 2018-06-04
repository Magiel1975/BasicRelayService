using System;
using System.Configuration;
using System.Threading;

namespace RelayApi.ConsoleApp
{
    public static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var starter = new Starter())
                {
                    starter.Start();
                    string webApiUrl = ConfigurationManager.AppSettings.Get("WebApiUrl");
                    System.Diagnostics.Process.Start($@"{webApiUrl}/swagger/ui/index");
                    Console.WriteLine($@"Process Started and hosted at {webApiUrl}");
                    Console.WriteLine("Press Enter key to exit and stop the Process.");
                    Console.ReadLine();
                    Console.WriteLine("Stopping the Process...");
                    starter.Stop();
                    Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during startup {ex}");
            }
        }
    }
}
