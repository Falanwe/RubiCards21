using System;
using System.Collections.Generic;
using System.Text;

namespace AynchroSample
{
    public static class Program
    {
        private static string[] _words = { "sucette", "patate", "Noël", "couscous" };
        private static Random _rand = new Random();
        private static string RandomWord() => _words[_rand.Next(4)];

        public static void Main(string[] args)
        {
            //var delegateExemples = new DelegatesExemples();

            //delegateExemples.Prefix = "Chan";
            //delegateExemples.Suffix = "Tho";
            //var generator = delegateExemples.DecorateStringFactory(RandomWord);

            //Console.WriteLine(generator());
            //Console.ReadLine();

            //delegateExemples.Prefix = "1";
            //delegateExemples.Suffix = "2";
            //Console.WriteLine(generator());

            var tupleSample = new TupleSample();

            tupleSample.SwitchTest(Move.Paper, Move.Scissor);
            tupleSample.SwitchTest(Move.Scissor, Move.Paper);
            tupleSample.SwitchTest(Move.Rock, Move.Rock);
        }
    }
}
