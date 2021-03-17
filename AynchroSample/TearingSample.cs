using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AynchroSample
{
    public struct SomewhatBigStruct
    {
        public SomewhatBigStruct(long l)
        {
            X = l;
            Y = l;
            Z = l;
        }
        public long X { get; }
        public long Y { get; }
        public long Z { get; } 
    }
    public static class TearingSample
    {
        private static SomewhatBigStruct _field = new SomewhatBigStruct(0);

        private static bool _isRunning = true;
        private static readonly object _syncRoot = new object();

        private static void SetFieldMayTimes(long n)
        {
            var value = new SomewhatBigStruct(n); ;
            while (_isRunning)
            {
                lock (_syncRoot)
                {
                    _field = value;
                }
            }
        }

        private static long _globalCounter = 0;

        private static void IncrementFast()
        {
            var localCounter = 0L;
            while(_isRunning)
            {
                localCounter++;
                Interlocked.Increment(ref _globalCounter);                
            }

            Console.WriteLine($"local counter = {localCounter}");
        }

        public static void Execute()
        {
            Task.Run(() => SetFieldMayTimes(1));
            Task.Run(() => SetFieldMayTimes(2));
            Task.Run(() => SetFieldMayTimes(3));

            SomewhatBigStruct copy = new SomewhatBigStruct();
            while (true)
            {
                if (copy.X != copy.Y || copy.X != copy.Z)
                {
                    Console.WriteLine($"X is {copy.X}, Y is {copy.Y}, Z is {copy.Z}");
                }
                lock (_syncRoot)
                {
                    copy = _field;
                }
            }

            _isRunning = false;

            //Console.WriteLine($"X is {copy.X}, Y is {copy.Y}, Z is {copy.Z}. This should not happen!");
        }

        public static void Execute1()
        {
            Task.Run(IncrementFast);
            Task.Run(IncrementFast);

            Console.ReadLine();
            _isRunning = false;
            Console.WriteLine($"global counter = {_globalCounter}");
        }
    }
}
