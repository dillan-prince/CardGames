using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CardGames.Models
{
    public class Player : MonoBehaviour
    {
        public string Name { get; set; }
        public List<Card> Deck { get; set; }
        public bool IsPlayerOne { get; set; }

        public void Init(string name, bool isPlayerOne)
        {
            Name = name;
            IsPlayerOne = isPlayerOne;
        }

        /// <summary>
        /// Shows player's top two cards, placing them in the middle of the board.
        /// </summary>
        /// <param name="placeOnTop">If true, player places their cards in the top row of the middle of the board. Otherwise, player places their cards in the bottom row of the middle of the board</param>
        /// <returns>The two cards at the top of the player's deck.</returns>
        public List<Card> ShowTopTwoCards()
        {
            int count = Deck.Count() - 2;
            List<Card> topTwoCards = Deck.GetRange(count, 2);
            Deck.RemoveRange(count, 2);

            float x = .42f;
            float y = .55f;

            if (IsPlayerOne)
            {
                topTwoCards[0].Move(new Vector3(-x, y));
                topTwoCards[1].Move(new Vector3(x, y));
            }
            else
            {
                topTwoCards[0].Move(new Vector3(-x, -y));
                topTwoCards[1].Move(new Vector3(x, -y));
            }

            return topTwoCards;
        }

        /// <summary>
        /// Adds the cards in the middle of the board to the bottom of a player's deck.
        /// </summary>
        /// <param name="middleCards">Cards in the middle of the board.</param>
        public void TakeMiddleCards(List<Card> middleCards)
        {
            foreach (Card card in Deck)
                card.transform.localPosition += (new Vector3(1 / 750f, 1 / 750f, -1 / 750f)) * middleCards.Count();

            Deck.InsertRange(0, middleCards);

            Vector3 newPosition = IsPlayerOne ? new Vector3(-1.8f, 0) : new Vector3(1.8f, 0);
            for (int i = 0; i < middleCards.Count(); i++)
            {
                middleCards[i].ShowBack();
                middleCards[i].Move(newPosition + new Vector3(i / 750f, i / 750f, -i / 750f));
            }
        }

        /// <summary>
        /// Print contents of player's deck for debug purposes.
        /// </summary>
        public void Log()
        {
            int i = 0;
            foreach (Card card in Deck)
            {
                i++;
                string message = string.Format("{0}: {1} of {2}", i, card.Name, card.Suit);
                Debug.Log(message);
            }
        }
    }
}
