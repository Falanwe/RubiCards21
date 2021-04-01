using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkSample.Server

{
    public class RandomOrdering
    {
        private readonly byte[] _order;

        public RandomOrdering(byte n)
        {
            _order = new byte[n];


            var random = new Random();

            for (byte i = 0; i < n; i++)
            {
                _order[i] = i;
            }

            for (var i = (byte)(n - 1); i > 0; i--)
            {
                var swapTarget = random.Next(i + 1);
                var temp = _order[i];
                _order[i] = _order[swapTarget];
                _order[swapTarget] = temp;
            }
        }

        public bool IsFavorable => BiggestCycleLength() <= (_order.Length + 1) / 2;
        private int BiggestCycleLength()
        {
            var result = 0;
            for (var i = 0; i < _order.Length; i++)
            {
                var cycleLength = GetCycleLength(i);
                if(cycleLength > result)
                {
                    result = cycleLength;
                }
            }
            return result;
        }

        private int GetCycleLength(int start)
        {
            var result = 0;
            var current = start;
            do
            {
                result++;
                current = _order[current];
            }
            while (current != start);
            return result;
        }

        public byte LookAt(byte index)
        {
            if (index < _order.Length)
            {
                return _order[index];
            }
            else
            {
                return 255;
            }
        }
    }
}
