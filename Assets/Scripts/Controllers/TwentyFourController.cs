using System.Collections;
using System.Collections.Generic;
using CardGames.Models;
using CardGames.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TwentyFourController : MonoBehaviour
{
    public Transform deckPrefab;
    public Transform playerPrefab;
    public Button playerOneWinButton;
    public Button playerTwoWinButton;
    public Button drawButton;
    public Button dealButton;
    public Button showTopTwoButton;
    public Button flipButton;
    public Button resetButton;
    public Button resetYesButton;
    public Button resetNoButton;
    public Button mainMenuButton;
    public Button mainMenuYesButton;
    public Button mainMenuNoButton;
    public GameObject winnerPanel;

    private Deck _deck;
    private Player _playerOne;
    private Player _playerTwo;
    private List<Card> _middleCards;
    private bool _isGameOver;

    #region Unity Methods
    private void Awake()
    {
        _deck = Instantiate(deckPrefab).GetComponent<Deck>();

        _playerOne = Instantiate(playerPrefab).GetComponent<Player>();
        _playerTwo = Instantiate(playerPrefab).GetComponent<Player>();

        // TODO: Ask for Player names, init Players with input names.
        _playerOne.Init("Dillan", true);
        _playerTwo.Init("Shan", false);

        InitButtons();
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Shuffles and deals the game deck to both players. 
    /// </summary>
    public void DealDeck()
    {
        _isGameOver = false;

        _deck.Shuffle();
        _deck.Deal(_playerOne, _playerTwo);

        showTopTwoButton.interactable = true;
        playerOneWinButton.gameObject.SetActive(true);
        playerTwoWinButton.gameObject.SetActive(true);
        dealButton.interactable = false;
    }

    /// <summary>
    /// Shows the top two cards from the decks of both players, face down.
    /// </summary>
    public void ShowTopTwoCards()
    {
        _middleCards = new List<Card>();

        _middleCards.AddRange(_playerOne.ShowTopTwoCards());
        _middleCards.AddRange(_playerTwo.ShowTopTwoCards());

        showTopTwoButton.interactable = false;
        flipButton.interactable = true;
    }

    /// <summary>
    /// Flips the cards in the middle of the board to be face up.
    /// </summary>
    public void FlipCards()
    {
        foreach (Card card in _middleCards)
            card.ShowFront();

        flipButton.interactable = false;
        playerOneWinButton.interactable = true;
        playerTwoWinButton.interactable = true;
        drawButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Called when the Reset button is pressed. Enables Reset confirmation buttons.
    /// </summary>
    public void ConfirmReset()
    {
        if (_isGameOver)
        {
            ResetDeck();
            return;
        }

        resetYesButton.gameObject.SetActive(true);
        resetNoButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Called when user responds with No to the Reset confirmation. Hides the confirmation buttons.
    /// </summary>
    public void DenyReset()
    {
        resetYesButton.gameObject.SetActive(false);
        resetNoButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Resets the game deck to the middle of the board and resets the buttons for a new game.
    /// </summary>
    public void ResetDeck()
    {
        InitButtons();
        _deck.Reset();
        _playerOne.Deck = null;
        _playerTwo.Deck = null;
    }

    /// <summary>
    /// Gives two cards from the middle of the board to both players. The two cards selected will consist of one card that this player played, and one card that the other player played.
    /// </summary>
    public void RoundDraw()
    {
        _playerOne.TakeMiddleCards(new List<Card> { _middleCards[1], _middleCards[3] });
        _playerTwo.TakeMiddleCards(new List<Card> { _middleCards[0], _middleCards[2] });
        EnableButtonsForNewHand();
    }

    /// <summary>
    /// Gives all four middle cards to player two.
    /// </summary>
    public void PlayerOneWin()
    {
        _playerTwo.TakeMiddleCards(_middleCards);

        if (_playerOne.Deck.Count == 0)
        {
            DisplayWinner(_playerOne);
        }
        else
        {
            EnableButtonsForNewHand();
        }
    }

    /// <summary>
    /// Gives all four middle cards to player one.
    /// </summary>
    public void PlayerTwoWin()
    {
        _playerOne.TakeMiddleCards(_middleCards);

        if (_playerTwo.Deck.Count == 0)
        {
            DisplayWinner(_playerTwo);
        }
        else
        {
            EnableButtonsForNewHand();
        }
    }

    /// <summary>
    /// Displays confirmation buttons for returning to the main menu.
    /// </summary>
    public void ConfirmBackToMainMenu()
    {
        if (_isGameOver)
        {
            BackToMainMenu();
            return;
        }

        mainMenuYesButton.gameObject.SetActive(true);
        mainMenuNoButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides confirmation buttons for returning to the main menu.
    /// </summary>
    public void DenyBackToMainMenu()
    {
        mainMenuYesButton.gameObject.SetActive(false);
        mainMenuNoButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Returns to the main menu.
    /// </summary>
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Disables all buttons except Deal and Reset, and also hides the appropriate buttons, resetting the buttons for a new game.
    /// </summary>
    private void InitButtons()
    {
        _isGameOver = true;

        drawButton.gameObject.SetActive(false);
        playerOneWinButton.gameObject.SetActive(false);
        playerOneWinButton.interactable = false;
        playerTwoWinButton.gameObject.SetActive(false);
        playerTwoWinButton.interactable = false;

        dealButton.interactable = true;
        showTopTwoButton.interactable = false;
        flipButton.interactable = false;

        resetYesButton.gameObject.SetActive(false);
        resetNoButton.gameObject.SetActive(false);
        mainMenuNoButton.gameObject.SetActive(false);
        mainMenuYesButton.gameObject.SetActive(false);

        winnerPanel.SetActive(false);
    }

    /// <summary>
    /// Resets the buttons for a new hand.
    /// </summary>
    private void EnableButtonsForNewHand()
    {
        playerOneWinButton.interactable = false;
        playerTwoWinButton.interactable = false;

        drawButton.gameObject.SetActive(false);
        showTopTwoButton.interactable = true;
        _middleCards = null;
    }

    /// <summary>
    /// Displays the winner of the game and disables all buttons except Reset.
    /// </summary>
    /// <param name="player"></param>
    private void DisplayWinner(Player player)
    {
        _isGameOver = true;

        playerOneWinButton.gameObject.SetActive(false);
        playerTwoWinButton.gameObject.SetActive(false);
        drawButton.gameObject.SetActive(false);

        winnerPanel.GetComponentInChildren<Text>().text = string.Format("{0} is the winner!", player.Name);
        winnerPanel.SetActive(true);
    }
    #endregion
}
