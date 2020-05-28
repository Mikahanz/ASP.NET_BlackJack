using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_BlackJack.Services
{
    public class Deck
    {
        public List<Card> Cards { get; set; }


        public Deck()
        {
            this.Cards = new List<Card>();
        }

        public void createFullDeck()
        {
            foreach (Suit cardSuit in typeof(Suit).GetEnumValues())
            {
                foreach (Value value in typeof(Value).GetEnumValues())
                {
                    this.Cards.Add(new Card(cardSuit, value));
                }
            }
        }

        public void shuffle()
        {
            // temporary list of cards
            List<Card> tmpDeck = new List<Card>();

            // Use random
            Random random = new Random();

            int randomCardIndex = 0;
            int originalSize = this.Cards.Count;

            for (int i = 0; i < originalSize; i++)
            {
                randomCardIndex = random.Next((this.Cards.Count - 1 - 0) + 1) + 0;

                Card theCard = this.Cards[randomCardIndex];

                tmpDeck.Add(theCard);

                this.Cards.Remove(theCard);
            }

            this.Cards = tmpDeck;
        }

        public override string ToString()
        {
            string stringBack = "";
            int i = 1;
            //Cards.ForEach(c => stringBack += $"{i++}-{c.Suit} - {c.Value} \n");
            Cards.ForEach(c => stringBack += $"{c.Value}-{c.Suit}\n");

            return stringBack;
        }

        public void removeCard(int i)
        {
            this.Cards.Remove(this.Cards[i]);
        }

        public Card getCard(int i)
        {
            return this.Cards[i];
        }

        public void addCard(Card card)
        {
            this.Cards.Add(card);
        }

        public void draw(Deck deckFrom)
        {
            this.Cards.Add(deckFrom.getCard(0));
            deckFrom.removeCard(0);
        }

        
        // Return total value of cards in deck
        public int cardsValue()
        {
            int totalValue = 0;

            int numOfAces = 0;

            foreach (Card theCard in Cards)
            {
                switch (theCard.Value)
                {
                    case Value.TWO:
                        totalValue += 2;
                        break;
                    case Value.THREE:
                        totalValue += 3;
                        break;
                    case Value.FOUR:
                        totalValue += 4;
                        break;
                    case Value.FIVE:
                        totalValue += 5;
                        break;
                    case Value.SIX:
                        totalValue += 6;
                        break;
                    case Value.SEVEN:
                        totalValue += 7;
                        break;
                    case Value.EIGHT:
                        totalValue += 8;
                        break;
                    case Value.NINE:
                        totalValue += 9;
                        break;
                    case Value.TEN:
                        totalValue += 10;
                        break;
                    case Value.JACK:
                        totalValue += 10;
                        break;
                    case Value.QUEEN:
                        totalValue += 10;
                        break;
                    case Value.KING:
                        totalValue += 10;
                        break;
                    case Value.ACE:
                        numOfAces += 1;
                        break;
                }
            }

            for (int i = 0; i < numOfAces; i++)
            {
                if (totalValue > 10)
                {
                    totalValue += 1;
                }
                else
                {
                    totalValue += 11;
                }
            }

            return totalValue;
        }

        public int getSize()
        {
            return this.Cards.Count;
        }

        // Move back all the playing deck to initial deck
        public void moveAllCardsToDeck(Deck moveToDeck)
        {
            int thisDeckSize = this.Cards.Count;

            // put cards into moveToDeck
            for (int i = 0; i < thisDeckSize; i++)
            {
                moveToDeck.addCard(this.getCard(i));
            }

            // remove all cards
            for (int i = 0; i < thisDeckSize; i++)
            {
                this.removeCard(0);
            }
        }
    }
}