using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField]
    private GameController controller;

    [SerializeField]
    private Image fill;

    [SerializeField]
    private TMP_Text colorText;

    private int[] FruitCounts;

    private int desiredFruitCount;
    private int currentFruitCount;
    private int currentCountIndex;

    private void Start()
    {
        FruitCounts = GenerateFruitCounts();

        currentCountIndex = 0;
        desiredFruitCount = FruitCounts[currentCountIndex];
        currentFruitCount = desiredFruitCount;
    }

    public void SetScoreBarFill(Color color)
    {
        fill.color = color;

        string colorName = GetColorName(color);
        colorText.text = $"Ловите <color=#{ColorUtility.ToHtmlStringRGBA(color)}>{colorName}</color>";
    }

    public void AddPoint() => ChangeScore(1);

    public void SubPoint() => ChangeScore(-1);

    private void ChangeScore(int delta)
    {
        currentFruitCount += delta;

        if (currentFruitCount == 0 && currentCountIndex < FruitCounts.Length - 1)
        {
            currentCountIndex++;
            desiredFruitCount = FruitCounts[currentCountIndex];
            currentFruitCount = desiredFruitCount;
            controller.ChangeColor();
        }
        else if (currentFruitCount <= 0)
            Debug.Log("Victory!");
        else if (currentFruitCount > desiredFruitCount)
            Debug.Log("Game Over");

        fill.transform.localScale = new Vector3(currentFruitCount / (float)desiredFruitCount, 1, 1);
    }

    private string GetColorName(Color color)
    {
        if (color == Color.red) return "Красный";
        else if (color == Color.green) return "Зеленый";
        else if (color == Color.blue) return "Синий";
        else if (color == new Color(1f, 0.92f, 0.016f, 1f)) return "Желтый";
        else return "Оранжевый";
    }

    private int[] GenerateFruitCounts()
    {
        System.Random random = new(DateTime.Now.Millisecond);
        int defaultCount = 4;
        
        int[] fruitsCount = new int[controller.complexityLevel + 1];
        Array.Fill(fruitsCount, defaultCount);
        int shuffleAmount = fruitsCount.Length / 2 * 2; 

        for (int i = 0; i < shuffleAmount; i += 2)
        {
            int delta = random.Next(-defaultCount / 2, defaultCount / 2 + 1);
            fruitsCount[i] -= delta;
            fruitsCount[i + 1] += delta;
        }

        return fruitsCount;
    }
}
