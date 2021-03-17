using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

        private static async Task WaitASecondWithAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            Console.WriteLine("I'll wait some more.");
            await Task.Delay(TimeSpan.FromSeconds(1));
            Console.WriteLine("I finished waiting");
        }

        public static async Task<int> CountGooglesInGoogle()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://www.google.com");
            var text = await response.Content.ReadAsStringAsync();

            return Regex.Matches(text, "google", RegexOptions.IgnoreCase).Count;
        }

        private static bool _shouldRun = true;
        private static async Task WriteAfterTaskCompleted(Task<DateTime> task)
        {
            var time = await task.ConfigureAwait(false);
            Console.WriteLine($"The task completed at {time}!");
            _shouldRun = false;
        }

        private static readonly Random _rand = new Random();

        private static async Task<int> WaitRandomly()
        {
            var delay = _rand.Next(2000);
            await Task.Delay(delay);
            return delay;
        }

        public static void Execute()
        {
            var aPressedTaskCompletionSource = new TaskCompletionSource<DateTime>();

            var _ = WriteAfterTaskCompleted(aPressedTaskCompletionSource.Task);
            _ = WriteAfterTaskCompleted(Task.Run(() =>
            {
                Thread.Sleep(200);
                return DateTime.UtcNow;
            }));

            while (_shouldRun)
            {
                if (Console.ReadKey().KeyChar == 'a')
                {
                    aPressedTaskCompletionSource.SetResult(DateTime.UtcNow);
                }
            }
            Console.WriteLine("End of Program");
            Console.ReadLine();
        }

        public static async IAsyncEnumerable<int> WaitRandomlyManyTimes(int n)
        {
            for (var i = 0; i < n; i++)
            {
                yield return await WaitRandomly();
            }
        }

        public static async Task Execute1()
        {
            await foreach (var delay in WaitRandomlyManyTimes(10))
            {
                Console.WriteLine($"I waited {delay} ms");
            }
        }
    }
}
