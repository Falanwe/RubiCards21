using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AynchroSample
{
    public static class CoroutineSample
    {
        private static string[] _words = { "sucette", "patate", "Noël", "couscous" };
        private static Random _rand = new Random();
        private static string RandomWord() => _words[_rand.Next(4)];

        private static IEnumerator MyCoroutine()
        {
            for (var i = 0; i < 4; i++)
            {
                var timeToWait = _rand.Next(40);
                Console.WriteLine($"Waiting {timeToWait * 100} ms.");
                for (var j = 0; j < timeToWait; j++)
                {
                    yield return null;
                }

                Console.WriteLine(RandomWord());
            }
        }

        public static void Execute()
        {
            using var engine = new CoroutineEngine();
            engine.ExecuteCoroutine(MyCoroutine());

            Console.WriteLine("Press enter to stop");
            Console.ReadLine();
            engine.Stop();
        }
    }
}
