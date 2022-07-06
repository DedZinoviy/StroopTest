using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MechanicGame : MonoBehaviour
{
    private List<Card> Cards = new List<Card>();
    private bool isActive = false;

    public void OpenCard(Card card)
    {
        if (Cards.Count < 2 && !Cards.Exists(x => x.Id == card.Id))
        {
            card.StartFront();
            Cards.Add(card);
        }

        if (Cards.Count == 2 && !isActive)
        {
            StartCoroutine(CloseCards());
        }
    }

    IEnumerator CloseCards()
    {
        isActive = true;
        yield return new WaitForSeconds(1);

        Card firstCard = Cards[0];
        Card secondCard = Cards[1];
        Cards.Clear();
        isActive = false;

        if (firstCard.type == secondCard.type)
        {
            Destroy(firstCard.gameObject);
            Destroy(secondCard.gameObject);
        }
        else
        {
            firstCard.StartBack();
            secondCard.StartBack();
        }
    }
    
}