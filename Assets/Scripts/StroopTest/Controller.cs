using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Linq;

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
    [SerializeField]private double Seconds;
    private double OriginalSec;
    [SerializeField]private double QuestionTime;
    private double OriginalQuestionTime;
    private int Score = 0;
    [SerializeField]private float safeTime;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        TimeEdit.text = "Начало через: " + Math.Round(safeTime, 1);
        TimeEdit.color = Color.red;
        OriginalSec = Seconds;
        OriginalQuestionTime =  QuestionTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (safeTime <= 0)
        {
            List<Button> buttons = GameObject.FindObjectsOfType<Button>().ToList();
            buttons.ForEach(button => button.enabled = true);
            TimeEdit.color = Color.white;
            if (Seconds > 0) // Если время не вышло...
            {

                Seconds -= Time.deltaTime; // Уменьшить оставшееся время.
                TimeEdit.text = "Время: " + Math.Round(Seconds, 1); // Вывести оставшееся время на экран.
                this.TimePerQuestion(); // Изменить время одного вопроса.    
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
        else
        {
            this.SafeTime();
            TimeEdit.text = "Начало через: " + Math.Round(safeTime, 1);
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
        ScoreEdit.text = "Счёт: " + Score + "/" + QuestionCount; // Отобразить счёт на экране.
        text.SetText(); // Перемешать кнопки.
        this.SetOriginalQuestionTime();
        TimeBar.ReturnToOriginScale();
        TimeBar.ChangeBarColor(1);
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

    private void TimePerQuestion()
    {
        if (QuestionTime > 0) // Если время на вопрос не вышло...
        {
            double prevSec = QuestionTime;
            QuestionTime -= Time.deltaTime; // Уменьшить время на вопрос.
            double percent = QuestionTime / prevSec;
            double colorPerc = QuestionTime / OriginalQuestionTime;
            TimeBar.ChangeBarSize((float)percent);
            TimeBar.ChangeBarColor((float)colorPerc);
        }
        else
        {
            text.SetText();
            EtalonColor.ChangeText();
            EtalonColor.ChangeTextColor();
            QuestionTime = OriginalQuestionTime;
            TimeBar.ReturnToOriginScale();
            TimeBar.ChangeBarColor(1);
        }
    }

    private void SetOriginalQuestionTime()
    {
        QuestionTime = OriginalQuestionTime;
    }

    private void SafeTime()
    {
        safeTime -= Time.deltaTime;
    }
}
