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
    [SerializeField] private int QuestionCount;
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
        if (Seconds > 0) // Если время не вышло...
        {
            double prevSec = Seconds;
            Seconds -= Time.deltaTime; // Уменьшить оставшееся время.
            TimeEdit.text = "Время: " + Math.Round(Seconds, 1); // Вывести оставшееся время на экран.
            
            // Изменить длину и цвет временной шкалы, согласно времени.
            double percent = Seconds / prevSec;
            double colorPerc = Seconds / OriginalSec;
            TimeBar.ChangeBarSize((float)percent);
            TimeBar.ChangeBarColor((float)colorPerc);
        }
        else // Иначе...
        {
            if (this.Score > save.LoadScore()) // Сохранить результат, если он является новым рекордом.
            {
                save.HighestScore = this.Score;
                save.SaveScore();
            }
            Change.ToMenu(); // Выйти в меню.
        }

    }

    /// <summary>
    /// Method for checking answers correctness.
    /// </summary>
    /// <param name="button">Clicked button.</param>
    public void isCorrectAnswer(Btn button)
    {
        if (EtalonColor.currentColor == button.Color) // Если цвет кнопки совпадает с цветом на дисплее...
        {
            Score++; // Увеличить счёт.
            Win(); // Проеврить выигрышную ситуацию.
        }
        else // Иначе...
        {
            Seconds -= 0.5; // Вычесть время в качестве штрафа.
        }
        ScoreEdit.text = "Счёт: " + Score; // Отобразить счёт на экране.
        text.SetText(); // Перемешать кнопки.
    }

    /// <summary>
    /// Method for checking winning situation.
    /// </summary>
    private void Win()
    {
        if (this.Score >= QuestionCount) // Перейти к другой головоломке, если счёт совпадает с требуемым.
        {
            Change.NextScene();
        }
    }


}
