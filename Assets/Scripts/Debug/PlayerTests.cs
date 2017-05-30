using UnityEngine;
using System.Collections;
using TwentyFour.Utility;
using TwentyFour.Models;
using System.Collections.Generic;

public class PlayerTests : MonoBehaviour
{
    public Transform deckPrefab;
    public Transform playerPrefab;
    private Deck _deck;

    private Player _playerOne;
    private Player _playerTwo;
    private static List<Card> _middleCards;

    private void Awake()
    {
        _deck = Instantiate(deckPrefab).GetComponent<Deck>();

        _playerOne = Instantiate(playerPrefab).GetComponent<Player>();
        _playerTwo = Instantiate(playerPrefab).GetComponent<Player>();

        _playerOne.Init("Dillan", true);
        _playerTwo.Init("Feeling", false);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 28), "Shuffle"))
        {
            _deck.Shuffle();
        }

        if (GUI.Button(new Rect(10, 40, 100, 28), "Deal"))
        {
            _deck.Deal(_playerOne, _playerTwo);
        }

        if (GUI.Button(new Rect(10, 70, 100, 28), "Reset"))
        {
            _deck.Reset();
        }

        if (GUI.Button(new Rect(10, 100, 100, 28), "Show Top Two"))
        {
            _middleCards = new List<Card>();

            List<Card> playerOneCards = _playerOne.ShowTopTwoCards();
            List<Card> playerTwoCards = _playerTwo.ShowTopTwoCards();

            _middleCards.AddRange(playerOneCards);
            _middleCards.AddRange(playerTwoCards);
        }

        if (GUI.Button(new Rect(10, 130, 100, 28), "Flip"))
        {
            foreach (Card card in _middleCards)
                card.ShowFront();
        }

        if (GUI.Button(new Rect(10, 160, 100, 28), "Player One Win"))
        {
            _playerTwo.TakeMiddleCards(_middleCards);
        }

        if (GUI.Button(new Rect(10, 190, 100, 28), "Player Two Win"))
        {
            _playerOne.TakeMiddleCards(_middleCards);
        }

        if (GUI.Button(new Rect(10, 220, 100, 28), "Round Draw"))
        {
            _playerOne.TakeMiddleCards(new List<Card> { _middleCards[1], _middleCards[3] });
            _playerTwo.TakeMiddleCards(new List<Card> { _middleCards[0], _middleCards[2] });
        }
    }
}
