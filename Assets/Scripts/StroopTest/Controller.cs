using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// Class providing general Stroop test scene management.
/// </summary>
public class Controller : MonoBehaviour
{
    [SerializeField] private DisplayColor EtalonColor;
    [SerializeField] private SetButtonText text;
    [SerializeField] private TMP_Text ScoreEdit;
    [SerializeField] private TMP_Text TimeEdit;
    [SerializeField] private Bar TimeBar;
    [SerializeField] private SceneChanger Change;
    [SerializeField] private float safeTime;
    private PlayerPrefs save = new PlayerPrefs();
    private int QuestionCount;
    private double QuestionTime;
    private double Seconds;
    private double OriginalSec;
    private double OriginalQuestionTime;
    private int Score = 0;
    private int LevelComlexity;
    

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        TimeEdit.text = "Начало через: " + Math.Round(safeTime, 1);
        TimeEdit.color = Color.red;
        this.LevelComlexity = save.LoadLevelComplexity();
        this.SetLevelComlexity(LevelComlexity);
        this.ScoreEdit.text = "Счёт: 0/" + this.QuestionCount;
        OriginalSec = Seconds;
        OriginalQuestionTime =  QuestionTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (safeTime <= 0) // Если закончилось безопасное время.
        {
            // Активировать кнопки, отвечающие за цвета.
            List<Button> buttons = GameObject.FindObjectsOfType<Button>().ToList();
            buttons.ForEach(button => button.enabled = true);

            TimeEdit.color = Color.white; // Вернуть цвет в исходное состояние.
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
        else // Иначе...
        {
            this.SafeTime(); // Уменьшить безопасное время.
            TimeEdit.text = "Начало через: " + Math.Round(safeTime, 1); // Отобразить оставшееся безопасное время на экране.
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
        this.SetOriginalQuestionTime(); // Вернуть время на вопрос к исходному значению.
        TimeBar.ReturnToOriginScale(); // Востановить шкалу в размере.
        TimeBar.ChangeBarColor(1); // Вернуть шкале исходный цвет.
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

    /// <summary>
    /// Provding management with save time.
    /// </summary>
    private void TimePerQuestion()
    {
        if (QuestionTime > 0) // Если время на вопрос не вышло...
        {
            double prevSec = QuestionTime;
            QuestionTime -= Time.deltaTime; // Уменьшить время на вопрос.
            double percent = QuestionTime / prevSec;
            double colorPerc = QuestionTime / OriginalQuestionTime;
            TimeBar.ChangeBarSize((float)percent); // Изменить резмер шкалы.
            TimeBar.ChangeBarColor((float)colorPerc); // Изменить цвет шкалы.
        }
        else // Иначе...
        {
            text.SetText(); // Изменить положение кнопок.
            EtalonColor.ChangeText(); // Изменить текст на дисплее.
            EtalonColor.ChangeTextColor(); // Изменить цвет текста на дисплее.
            QuestionTime = OriginalQuestionTime; // Вернуть время на вопрос к исходному значению.
            TimeBar.ReturnToOriginScale(); // Востановить шкалу в размере.
            TimeBar.ChangeBarColor(1); // Вернуть шкале исходный цвет.
        }
    }

    /// <summary>
    /// Returns Qusetion time to default value.
    /// </summary>
    private void SetOriginalQuestionTime()
    {
        QuestionTime = OriginalQuestionTime;
    }

    /// <summary>
    /// Decreases save time.
    /// </summary>
    private void SafeTime()
    {
        safeTime -= Time.deltaTime;
    }

    /// <summary>
    /// Changes the difficulty level depending on the received value.
    /// </summary>
    /// <param name="levelEval">Value of Complexity - [1; 5]; if the value is outside the range, the difficulty will be placed on the range boundaries.</param>
    private void SetLevelComlexity(int levelEval)
    {
        if (levelEval <= 1)
        {
            this.Seconds = 10;
            this.QuestionCount = 2;
            this.QuestionTime = 5;
        }
        else if (levelEval == 2)
        {
            this.Seconds = 12;
            this.QuestionCount = 4;
            this.QuestionTime = 4.6;
        }
        else if (levelEval == 3)
        {
            this.Seconds = 14;
            this.QuestionCount = 6;
            this.QuestionTime = 4.2;
        }
        else if (levelEval == 4)
        {
            this.Seconds = 16;
            this.QuestionCount = 8;
            this.QuestionTime = 3.8;
        }
        else if (levelEval >= 5)
        {
            this.Seconds = 20;
            this.QuestionCount = 10;
            this.QuestionTime = 3;
        }
    }
}
