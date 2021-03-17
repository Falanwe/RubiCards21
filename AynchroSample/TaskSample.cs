using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AynchroSample
{
    public static class TaskSample
    {
        private static Task WaitASecond()
        {
            return Task.Delay(TimeSpan.FromSeconds(1))
                .ContinueWith(t =>
                {
                    if (t.IsCompletedSuccessfully)
                    {
                        Console.WriteLine("I'll wait some more.");
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong");
                    }
                    return Task.Delay(TimeSpan.FromSeconds(1));
                })
                .Unwrap()
                .ContinueWith(t =>
            {
                if (t.IsCompletedSuccessfully)
                {
                    Console.WriteLine("I finished waiting");
                }
                else
                {
                    Console.WriteLine("Something went wrong");
                }
            });
        }
        public static void Execute()
        {
            Console.WriteLine("Press Enter to start waiting.");
            while (true)
            {
                Console.ReadLine();
                WaitASecond();
            }
        }
    }
}
