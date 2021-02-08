using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bataille
{
    class BattleGame
    {
        public bool IsFinished
        {
            get;
            private set;
        }

        private List<CardSet> players;
        private string display;
        private List<Card> waitingCards;
        private bool equality;
        private int roundCount = 0;

        public BattleGame()
        {
            CardSet set = new CardSet(52);
            waitingCards = new List<Card>();
            display = "";
            equality = false;
            players = set.Distribute(2);
        }

        public void Update()
        {
            display += $"Round {++roundCount}\n";
            display += $"P1 => {players[0].Count} cards\nP2 => {players[1].Count} cards\n";
            Card cP1 = players[0].PopCard();
            Card cP2 = players[1].PopCard();

            display += $"P1 : {cP1} vs P2 : {cP2}";
            if(cP1 > cP2)
            {
                players[0].AddCard(cP1);
                players[0].AddCard(cP2);
                if (equality)
                {
                    equality = false;
                    players[0].AddRangeCard(waitingCards);
                    waitingCards.Clear();
                }
                display += $"\tP1 won !!!\n";
            }
            else if(cP1 < cP2)
            {
                players[1].AddCard(cP1);
                players[1].AddCard(cP2);
                if (equality)
                {
                    equality = false;
                    players[1].AddRangeCard(waitingCards);
                    waitingCards.Clear();
                }
                display += $"\tP2 won !!!\n";
            }
            else
            {
                equality = true;
                waitingCards.Add(cP1);
                waitingCards.Add(cP2);
                waitingCards.Add(players[0].PopCard());
                waitingCards.Add(players[1].PopCard());
                display += $"\tEquality !!!\n";
            }


            if (players[0].Count == 0 || players[1].Count == 0)
            {
                IsFinished = true;
                display += players[0].Count == 0 ? "==> P1 Lost !!!\n": "==> P2 Lost !!!\n";
            }
        }

        public void Display()
        {
            Console.WriteLine(display);
            display = "";
        }
    }
}
