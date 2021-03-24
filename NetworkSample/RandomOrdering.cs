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

            for(byte i = 0; i<n; i++)
            {
                _order[i] = i;
            }

            for(var i = (byte)(n-1); i>0; i--)
            {
                var swapTarget = random.Next(i+1);
                var temp = _order[i];
                _order[i] = _order[swapTarget];
                _order[swapTarget] = temp;
            }
        }

        public bool IsFavorable => true;
        
        public byte LookAt(byte index)
        {
            return _order[index];
        }
    }
}
