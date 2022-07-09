using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private ScoreCounter scoreCounter;

    [SerializeField]
    private SceneChanger changer;

    [SerializeField]
    private MissZone missZone;

    [SerializeField]
    private FailPanel failPanel;

    private Color[] colors = 
    { 
        Color.red, 
        Color.green, 
        new Color(1f, 0.92f, 0.016f, 1f), 
        Color.blue, 
        new Color(1f, 0.65f, 0f, 1f) 
    };

    private Color currentColor;

    private void Start()
    {
        ChangeColor();
    }

    public void CatchFruit(Color color)
    {
        if (color == currentColor)
        {
            scoreCounter.SubPoint();
        }
        else
        {
            scoreCounter.LosePoints();
        }
    }

    public void MissFruit(Color color)
    {
        if (color == currentColor)
        {
            scoreCounter.AddPoint();
        }
    }

    public void ChangeColor()
    {
        Color[] allowedColors = Array.FindAll(colors, x => x != currentColor);

        System.Random random = new System.Random();
        currentColor = allowedColors[random.Next(0, allowedColors.Length)];

        scoreCounter.SetScoreBarFill(currentColor);
        missZone.ActivateSafeTime();
    }

    public void Win() => changer.NextScene();

    public void Lose() => failPanel.Open();
}
