﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bataille
{
    class CardSet
    {
        private List<Card> cards;
        private Random rnd; 

        public int Count
        {
            get => cards.Count;
        }

        #region Constructors
        public CardSet()
        {
            cards = new List<Card>();
            rnd = new Random(DateTime.Now.Second);
        }

        public CardSet(int nbCard)
        {
            cards = new List<Card>(nbCard);
            rnd = new Random(DateTime.Now.Second);
            FillSet();
        }
        #endregion

        public List<CardSet> Distribute(int nbPlayer)
        {
            List<CardSet> res = new List<CardSet>(nbPlayer);
            Shuffle();
            for (int idxPlayer = 0; idxPlayer < nbPlayer; idxPlayer++)
            {
                res.Add(new CardSet());                
            }
            int player = 0;
            int size = cards.Count;
            for (int idx = 0; idx < size; idx++)
            {
                int rndIdx = rnd.Next(0, cards.Count);
                res[player].AddCard(cards[rndIdx]);
                cards.RemoveAt(rndIdx);
                player++;
                if (player >= nbPlayer) player = 0;
            }

            return res;
        }

        public Card PopCard()
        {
            if(cards.Count == 0) { return null; }
            Card res = cards[0];
            cards.RemoveAt(0);
            return res;
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public void AddRangeCard(List<Card> cards)
        {
            this.cards.AddRange(cards);
        }

        #region public override
        public override string ToString()
        {
            string res = "";
            foreach (Card card in cards)
            {
                res += card + "\n";
            }
            return res;
        }
        #endregion
        #region private methods
        private void Shuffle()
        {
            //for (int i = 0; i < iterations; i++)
            //{
                List<Card> shuffledSet = new List<Card>(cards.Count);
                int size = cards.Count;
                for (int idx = 0; idx < size; idx++)
                {
                    int rndIdx = rnd.Next(0, cards.Count);
                    shuffledSet.Add(cards[rndIdx]);
                    cards.RemoveAt(rndIdx);
                }
                cards = shuffledSet;
            //}            
        }

        private void FillSet()
        {
            foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
            {
                foreach(CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    cards.Add(new Card(value, color));
                }
            }
        }        
        #endregion
    }
}
