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

        public BattleGame()
        {
            CardSet set = new CardSet(52);
            players = set.Distribute(2);
        }

        public void Update()
        {
            
        }

        public void Display()
        {

        }
    }
}
