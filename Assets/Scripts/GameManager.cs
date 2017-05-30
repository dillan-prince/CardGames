using System.Collections;
using System.Collections.Generic;
using TwentyFour.Models;
using TwentyFour.Utility;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform deckPrefab;

    private Deck _deck;

    #region Unity Methods
    private void Awake()
    {
        _deck = Instantiate(deckPrefab).GetComponent<Deck>();
    }
    #endregion
}
