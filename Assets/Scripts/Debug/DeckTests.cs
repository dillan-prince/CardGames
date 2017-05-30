using UnityEngine;
using UnityEditor;
using TwentyFour.Utility;
using TwentyFour.Models;

public class DeckTests : MonoBehaviour
{
    public Transform deckPrefab;
    public Transform playerPrefab;

    private Deck _deck;
    private Player _playerOne;
    private Player _playerTwo;

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

        if (GUI.Button(new Rect(10, 100, 100, 28), "Log"))
        {
            _deck.Log();
        }
    }
}