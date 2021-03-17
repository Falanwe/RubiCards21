using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace AynchroSample
{
    class AsyncSample
    {
        private static bool _isRunning = true;

        private static IEnumerable<BigInteger> Fibo()
        {
            BigInteger previous = 1;
            BigInteger current = 1;
            yield return previous;
            yield return current;
            while (true)
            {
                var next = previous + current;
                yield return next;
                previous = current;
                current = next;
            }
        }

        private static void EnumerateFibo(object _)
        {
            foreach (var f in Fibo())
            {
                if (!_isRunning)
                {
                    return;
                }
                Console.WriteLine(f);
                Thread.Sleep(100);
            }
        }

        private static event Action<char> _onKeyPressed;
        public static void Execute()
        {
            bool aPressed = false;
            bool bPressed = false;
            bool cPressed = false;
            bool lost = false;

            void OnAPressed(char key)
            {
                if (key == 'a')
                {
                    Console.WriteLine();
                    Console.WriteLine("You pressed A!");
                    aPressed = true;

                    Console.WriteLine("You can now only press 10 keys");
                    _onKeyPressed -= OnAPressed;

                    StartCountDown(10, YouLose);
                }
            }

            void YouLose()
            {
                lost = true;
            }

            void StartCountDown(int keyStrokes, Action continuation)
            {
                _onKeyPressed += _ =>
                {
                    keyStrokes--;
                    if(keyStrokes == 0)
                    {
                        continuation();
                    }
                };
            }

            void OnBPressed(char key)
            {
                if (key == 'b')
                {
                    Console.WriteLine();
                    Console.WriteLine("You pressed B!");
                    bPressed = true;

                    _onKeyPressed -= OnBPressed;
                }
            }
            void OnCPressed(char key)
            {
                if (key == 'c')
                {
                    Console.WriteLine();
                    Console.WriteLine("You pressed C!");
                    cPressed = true;

                    _onKeyPressed -= OnCPressed;
                }
            }

            _onKeyPressed += OnAPressed;
            _onKeyPressed += OnBPressed;
            _onKeyPressed += OnCPressed;
            

            while (true)
            {
                var key = Console.ReadKey();
                _onKeyPressed?.Invoke(key.KeyChar);

                if (aPressed && bPressed && cPressed)
                {
                    _isRunning = false;
                    Console.WriteLine("A winner is you");
                    Console.WriteLine("Press enter to exit");
                    Console.ReadLine();
                    return;
                }
                if (lost)
                {
                    _isRunning = false;
                    Console.WriteLine();
                    Console.WriteLine("you lost");
                    Console.WriteLine("Press enter to exit");
                    Console.ReadLine();
                    return;
                }
            }
        }

    }
}
