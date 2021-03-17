using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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

        public static async Task Execute()
        {
            Console.WriteLine(await CountGooglesInGoogle());
        }
    }
}
