using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Controller : MonoBehaviour
{
    [SerializeField] private DisplayColor EtalonColor;
    [SerializeField] private SetButtonText text;
    [SerializeField] private TMP_Text ScoreEdit;
    [SerializeField] private TMP_Text TimeEdit;
    [SerializeField] private Bar TimeBar;
    private double Seconds;
    private int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        Seconds = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (Seconds >= 0)
        {
            double prevSec = Seconds;
            Seconds -= Time.deltaTime;
            TimeEdit.text = "Время: " + Math.Round(Seconds, 1);
            double percent = Seconds / prevSec;
            TimeBar.ChangeTimeBar((float)percent);
        }

    }

    public void isCorrectAnswer(Btn button)
    {
        if (EtalonColor.currentColor == button.Color)
        {
            Score++;
        }
        else
        {
            Seconds -= 0.5;
        }
        ScoreEdit.text = "Счёт: " + Score;
        text.SetText();
    }


}
