using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CardGames.Models;
using UnityEngine;

namespace CardGames.Utility
{
    /// <summary>
    /// Class representing a deck of cards.
    /// </summary>
    public class Deck : MonoBehaviour
    {
        /// <summary>
        /// Prefab used to instantiate 52 cards.
        /// </summary>
        public Transform cardPrefab;

        private List<Card> _deck;

        #region Unity Methods
        private void Awake()
        {
            InitDeck();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Shuffles the deck using a time-seeded random number generator.
        /// </summary>
        public void Shuffle()
        {
            System.Random random = new System.Random();
            _deck = _deck.OrderBy(number => random.Next()).ToList();
        }

        /// <summary>
        /// Deals the deck, one card at a time, to two players.
        /// </summary>
        /// <param name="playerOne">The first of two players to be dealt to.</param>
        /// <param name="playerTwo">The second of two players to be dealt to.</param>
        public void Deal(Player playerOne, Player playerTwo)
        {
            playerOne.Deck = new List<Card>();
            playerTwo.Deck = new List<Card>();

            float finalX = 1.8f;
            float displace = 750f;

            for (int i = 0; i < _deck.Count() / 2; i++)
            {
                Card card;

                card = _deck[2 * i];
                playerOne.Deck.Add(card);
                card.Move(new Vector3(-finalX + i / displace, i / displace, -i / displace));

                card = _deck[2 * i + 1];
                playerTwo.Deck.Add(card);
                card.Move(new Vector3(finalX + i / displace, i / displace, -i / displace));
            }
        }

        /// <summary>
        /// Reset the deck to the middle of the board.
        /// </summary>
        public void Reset()
        {
            float displace = 750f;
            for (int i = 0; i < _deck.Count(); i++)
            {
                _deck[i].ShowBack();
                _deck[i].Move(new Vector3(i / displace, i / displace, -i / displace));
            }

            _deck = _deck.OrderBy(card => card.Index).ToList();
        }

        /// <summary>
        /// Print contents of Deck for debug purposes.
        /// </summary>
        public void Log()
        {
            int i = 0;
            foreach (Card card in _deck)
            {
                i++;
                string message = string.Format("{0}: {1} of {2}", i, card.Name, card.Suit);
                Debug.Log(message);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initialize the deck with 52 cards, and stack the cards to give a 3D appearance.
        /// </summary>
        /// <param name="cardPrefab"></param>
        /// <returns></returns>
        private void InitDeck()
        {
            _deck = new List<Card>
            {
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Ace, CardInformation.Suits.Club, 0),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Ace, CardInformation.Suits.Heart, 1),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Ace, CardInformation.Suits.Spade, 2),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Ace, CardInformation.Suits.Diamond, 3),

                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Two, CardInformation.Suits.Club, 4),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Two, CardInformation.Suits.Heart, 5),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Two, CardInformation.Suits.Spade, 6),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Two, CardInformation.Suits.Diamond, 7),

                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Three, CardInformation.Suits.Club, 8),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Three, CardInformation.Suits.Heart, 9),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Three, CardInformation.Suits.Spade, 10),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Three, CardInformation.Suits.Diamond, 11),

                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Four, CardInformation.Suits.Club, 12),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Four, CardInformation.Suits.Heart, 13),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Four, CardInformation.Suits.Spade, 14),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Four, CardInformation.Suits.Diamond, 15),

                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Five, CardInformation.Suits.Club, 16),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Five, CardInformation.Suits.Heart, 17),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Five, CardInformation.Suits.Spade, 18),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Five, CardInformation.Suits.Diamond, 19),

                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Six, CardInformation.Suits.Club, 20),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Six, CardInformation.Suits.Heart, 21),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Six, CardInformation.Suits.Spade, 22),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Six, CardInformation.Suits.Diamond, 23),

                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Seven, CardInformation.Suits.Club, 24),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Seven, CardInformation.Suits.Heart, 25),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Seven, CardInformation.Suits.Spade, 26),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Seven, CardInformation.Suits.Diamond, 27),

                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Eight, CardInformation.Suits.Club, 28),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Eight, CardInformation.Suits.Heart, 29),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Eight, CardInformation.Suits.Spade, 30),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Eight, CardInformation.Suits.Diamond, 31),

                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Nine, CardInformation.Suits.Club, 32),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Nine, CardInformation.Suits.Heart, 33),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Nine, CardInformation.Suits.Spade, 34),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Nine, CardInformation.Suits.Diamond, 35),

                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Ten, CardInformation.Suits.Club, 36),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Ten, CardInformation.Suits.Heart, 37),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Ten, CardInformation.Suits.Spade, 38),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Ten, CardInformation.Suits.Diamond, 39),

                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Jack, CardInformation.Suits.Club, 40),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Jack, CardInformation.Suits.Heart, 41),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Jack, CardInformation.Suits.Spade, 42),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Jack, CardInformation.Suits.Diamond, 43),

                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Queen, CardInformation.Suits.Club, 44),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Queen, CardInformation.Suits.Heart, 45),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Queen, CardInformation.Suits.Spade, 46),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Queen, CardInformation.Suits.Diamond, 47),

                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.King, CardInformation.Suits.Club, 48),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.King, CardInformation.Suits.Heart, 49),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.King, CardInformation.Suits.Spade, 50),
                Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.King, CardInformation.Suits.Diamond, 51)
            };

            StackDeck(_deck, new Vector3(0, 0));
        }

        private void StackDeck(List<Card> deck, Vector3 startPosition)
        {
            float displace = 750f;

            for (int i = 0; i < deck.Count(); i++)
            {
                Vector3 newPosition = new Vector3(i / displace, i / displace, -i / displace);
                deck[i].transform.localPosition = startPosition + newPosition;
            }
        }
        #endregion
    }
}
