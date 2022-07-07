using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;

    [SerializeField]
    private PlayerPrefs playerPrefs;

    private CardType[] cardTypes = new CardType[]
    {
        CardType.CHERRY, CardType.PEAR, CardType.QIWI,
        CardType.APPLE, CardType.BANANA, CardType.GRAPE,
        CardType.ORANGE, CardType.STRAWBERRY, CardType.PLUM,
        CardType.WATERMELLON
    };

    private int pairsCount;

    private void Start()
    {
        pairsCount = playerPrefs.LoadLevelComplexity() * 2;
        SprideCards(GenerateCardsList());
        gameObject.GetComponent<MechanicGame>().pairsCount = pairsCount;
    }

    private void SprideCards(List<Card> cards)
    {
        int id = 0;

        int collumnCount = CalculateCollumnCount(pairsCount);
        int rowsCount = 2 * pairsCount / collumnCount;
        float cardScale = CalculateCardScale(pairsCount);
        float deltaCoordinates = -1 / (cardScale / 2f);

        float x = (collumnCount - 1) / deltaCoordinates;
        for (int i = 0; i < collumnCount; i++, x += cardScale)
        {
            float y = (rowsCount - 1) / deltaCoordinates;
            for (int j = 0; j < rowsCount; j++, y += cardScale)
            {
                cards[id].gameObject.transform.position = new Vector3(x, y, 0);
                cards[id].gameObject.transform.localScale = new Vector3(cardScale, cardScale, 1);
                id++;
            }
        }
    }

    private int CalculateCollumnCount(int pairsAmount)
    {
        if (pairsAmount < 6) return 2;
        else if (pairsAmount == 6) return 3;
        else return 4;
    }

    private float CalculateCardScale(int pairsAmount)
    {
        if (pairsAmount < 4) return 2f;
        else if (pairsAmount >= 4 && pairsAmount <= 6 ) return 1.5f;
        else return 1.2f;
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
                newCard.type = cardTypes[i];
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
