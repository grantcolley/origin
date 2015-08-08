using System;
using System.Threading.Tasks;

namespace DevelopmentInProgress.ExampleModule.Service
{
    public class TaskRunner
    {
        public static async Task DoAsyncStuff()
        {
            await Task.Run(async delegate
            {
                var random = new Random();
                int milliseconds = random.Next(3)*1000;
                await Task.Delay(milliseconds);
            });
        }
    }
}
