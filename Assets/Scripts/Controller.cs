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
    [SerializeField] private SceneChanger Change;
    private PlayerPrefs save = new PlayerPrefs();
    private double Seconds;
    private double OriginalSec;
    private int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        Seconds = 20;
        OriginalSec = Seconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (Seconds > 0)
        {
            double prevSec = Seconds;
            Seconds -= Time.deltaTime;
            TimeEdit.text = "Время: " + Math.Round(Seconds, 1);
            double percent = Seconds / prevSec;
            double colorPerc = Seconds / OriginalSec;
            TimeBar.ChangeBarSize((float)percent);
            TimeBar.ChangeBarColor((float)colorPerc);
        }
        else
        {
            if (this.Score > save.LoadScore())
            {
                save.HighestScore = this.Score;
                save.SaveScore();
            }
            Change.ToMenu();
        }

    }

    public void isCorrectAnswer(Btn button)
    {
        if (EtalonColor.currentColor == button.Color)
        {
            Score++;
            Win();
        }
        else
        {
            Seconds -= 0.5;
        }
        ScoreEdit.text = "Счёт: " + Score;
        text.SetText();
    }

    private void Win()
    {
        if (this.Score >= 2)
        {
            Change.NextScene();
        }
    }


}
