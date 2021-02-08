using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Hello");
                Battle battle = new Battle();

                Console.WriteLine("QUEUE A : ");
                for (int i = 0; i < battle.queueA.Count; i++)
                    Console.WriteLine(battle.queueA.ElementAt(i).ToString());

                Console.WriteLine("QUEUE B : ");
                for (int i = 0; i < battle.queueB.Count; i++)
                    Console.WriteLine(battle.queueB.ElementAt(i).ToString());

                int forceA = 0;
                for (int i = 0; i < battle.queueA.Count; i++)
                    forceA += battle.queueA.ElementAt(i).indice;

                int forceB = 0;
                for (int i = 0; i < battle.queueB.Count; i++)
                    forceB += battle.queueB.ElementAt(i).indice;

                while (!battle.isFinish)
                {
                    battle.Affrontement();
                }

                Console.WriteLine("PARTIE FINIE EN " + battle.tours.ToString());
                Console.WriteLine("PREDICTION : A -> " + forceA + " || B -> " + forceB);

                string v = Console.ReadLine();
            }
        }
    }

    public class Battle
    {
        public Queue<Card> queueA;
        public Queue<Card> queueB;
        public List<Card> stockA;
        public List<Card> stockB;
        public List<Card> egalite;

        public bool isFinish = false;
        public int tours = 0;

        public Battle()
        {
            GenerateCards();
        }

        void GenerateCards()
        {
            List<Card> cards = new List<Card>();

            for (int color = 0; color <= 3; color++)
                for (int indice = 2; indice <= 14; indice++)
                    cards.Add(new Card(indice, (Color)color));

            queueA = new Queue<Card>(); // PILE DU JOUEUR A
            queueB = new Queue<Card>();
            stockA = new List<Card>(); // PILE DES CARTES GAGNEES
            stockB = new List<Card>();
            egalite = new List<Card>();

            bool p = true;
            int cardsCount = cards.Count;
            for (int i = 0; i < cardsCount; i++)
            {
                Random r = new Random();
                int rdm = r.Next(0, cards.Count);
                Card card_rdm = cards[rdm];
                cards.RemoveAt(rdm);

                if (p)
                    queueA.Enqueue(card_rdm);
                else
                    queueB.Enqueue(card_rdm);

                p = !p;
            }
        }

        public Card Affrontement()
        {
            Console.WriteLine("TOURS : " + tours.ToString());
            tours++;

            if (queueA.Count == 0)
            {
                MixCardsAndEnqueue(ref stockA, ref queueA);
            }

            if (queueB.Count == 0)
            {
                MixCardsAndEnqueue(ref stockB, ref queueB);
            }

            Card cA = queueA.Dequeue();
            Card cB = queueB.Dequeue();
            Card winner = GetWinnerBetween(cA, cB);
            Console.WriteLine(string.Format(
                "BATTLE : {0} VS {1} => {2} wins !",
                cA.ToString(),
                cB.ToString(),
                winner.ToString()
                ));

            ThereIsAWinner();

            return winner;
        }

        Card GetWinnerBetween(Card cardA, Card cardB)
        {
            if (cardA.indice > cardB.indice)
            {
                Test(ref stockA, cardA, cardB);
                return cardA;
            }
            else if (cardA.indice < cardB.indice)
            {
                Test(ref stockB, cardA, cardB);
                return cardB;
            }
            else if (cardA.indice == cardB.indice)
            {
                if (queueA.Count == 0) MixCardsAndEnqueue(ref stockA, ref queueA);
                if (queueB.Count == 0) MixCardsAndEnqueue(ref stockB, ref queueB);

                egalite.Add(cardA);
                egalite.Add(cardB);

                if (queueA.Count == 0)
                    return cardA;
                if (queueB.Count == 0)
                    return cardB;

                egalite.Add(queueA.Dequeue());
                egalite.Add(queueB.Dequeue());

                return Affrontement();
            }

            return null;
        }

        public void Test(ref List<Card> stock, Card a, Card b)
        {
            stock.Add(a);
            stock.Add(b);

            if (egalite.Count > 0)
            {
                for (int i = 0; i < egalite.Count; i++)
                    stock.Add(egalite[i]);
                egalite = new List<Card>();
            }
        }

        public bool ThereIsAWinner()
        {
            Console.WriteLine("queue A : " + queueA.Count.ToString() + " queue B : " + queueB.Count.ToString());
            Console.WriteLine("stock A : " + stockA.Count.ToString() + " stock B : " + stockB.Count.ToString());
            return isFinish = (queueA.Count == 0 && stockA.Count == 0) || (queueB.Count == 0 && stockB.Count == 0);
        }

        public void MixCardsAndEnqueue(ref List<Card> stock, ref Queue<Card> toEnqueue)
        {
            MyExtensions.Shuffle(stock);

            for (int i = 0; i < stock.Count; i++)
                toEnqueue.Enqueue(stock[i]);

            stock = new List<Card>();
        }
    }

    public class Card
    {
        public int indice;
        public Color color;

        public Card(int _indice, Color _color)
        {
            indice = _indice;
            color = _color;
        }

        public override string ToString()
        {
            string i = indice.ToString();
            if (indice == 11)
                i = "J";
            else if (indice == 12)
                i = "Q";
            else if (indice == 13)
                i = "K";
            else if (indice == 14)
                i = "A";

            return string.Format(
                "{0} de {1}",
                i,
                color.ToString()
                );
        }
    }

    public enum Color
    {
        PIQUE, CARREAU, COEUR, TREFLE
    }

    public static class ThreadSafeRandom
    {
        [ThreadStatic] private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }

    static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
