using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;
    
    private CardType[] cardTypes = new CardType[] { CardType.CHERRY, CardType.PEAR, CardType.QIWI };

    [SerializeField]
    private int pairsCount = 2;

    private void Start()
    {
        SprideCards(GenerateCardsList());
    }

    private void SprideCards(List<Card> cards)
    {
        int id = 0;

        int rowsCount;
        int collumnCount;

        if (pairsCount < 6) collumnCount = 2;
        else if (pairsCount == 6) collumnCount = 3;
        else collumnCount = 4;

        rowsCount = 2 * pairsCount / collumnCount;

        float x = (collumnCount - 1) / -2f;
        for (int i = 0; i < collumnCount; i++, x++)
        {
            float y = (rowsCount - 1) / -2f;
            for (int j = 0; j < rowsCount; j++, y++)
            {
                cards[id].gameObject.transform.position = new Vector3(x, y, 0);
                id++;
            }
        }
    }

    private List<Card> GenerateCardsList()
    {
        List<Card> cards = new List<Card>();
        int id = 0;
        for (int i = 0; i < pairsCount; i++)
        {
            for (int j = 0; j < 2; j ++)
            {
                Card newCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Card>();
                newCard.type = cardTypes[i]; //!!!! [i]
                newCard.Id = id;
                newCard.mechanic = gameObject.GetComponent<MechanicGame>();
                cards.Add(newCard);
                id++;
            }
        }

        System.Random random = new System.Random(DateTime.Now.Millisecond);
        for (int i = cards.Count - 1; i >= 1; i--)
        {
            int newIndex = random.Next(0, i + 1);
            (cards[i], cards[newIndex]) = (cards[newIndex], cards[i]);
        }

        return cards;
    }
}
