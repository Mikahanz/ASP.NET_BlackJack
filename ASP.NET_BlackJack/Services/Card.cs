using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_BlackJack.Services
{
    public class Card
    {
        public Suit Suit { get; set; }
        public Value Value { get; set; }

        public Card(Suit suit, Value value)
        {
            this.Suit = suit;
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Suit.ToString() + " - " + this.Value.ToString();
        }
    }
}