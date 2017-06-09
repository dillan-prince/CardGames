using System.Collections;
using System.Collections.Generic;
using CardGames.Models;
using CardGames.Utility;
using UnityEngine;

/// <summary>
/// Utility for testing card flipping animation.
/// </summary>
public class CardTests : MonoBehaviour
{
    public Transform cardPrefab;
    private Card card;

    void Awake()
    {
        card = Instantiate(cardPrefab).GetComponent<Card>().Init(CardInformation.Names.Ace, CardInformation.Suits.Heart, 1);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 28), "Press"))
        {
            card.ShowFront();
        }
    }
}