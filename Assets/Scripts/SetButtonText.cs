using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class SetButtonText : MonoBehaviour
{
    private HashSet<DisplayColor.Colors> execlude = new HashSet<DisplayColor.Colors>();
    [SerializeField] private DisplayColor curColor;
    [SerializeField] private List<Btn> buttons;


    // Start is called before the first frame update
    void Start()
    {
        SetText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetText()
    {
        int random = -1;

        random = UnityEngine.Random.Range(0, 3); // Выбрать случайную кнопку для установки цвета.
        DisplayColor.Colors color = curColor.currentColor; // Получить установленный в окне цвет.
        buttons[random].SetColor(color); // Установть цвет в кнопку.
        execlude.Add(color); // Добавить цвет в список использованных цветов.

        var btnRange = Enumerable.Range(0, 4).Where(k => k != random); // Создать новый массив кнопок с учётом добавленной.

        foreach (var btn in btnRange) // Для каждой свободной кнопки...
        {
            // Создать список цветов, которые можно использовать.
            List<int> range = Enumerable.Range(0, 4).Where(j => !execlude.Contains((DisplayColor.Colors)j)).ToList();
            var rand = new System.Random(); // Сгенерировать случайное значение.
            int index = rand.Next(range.Count());
            DisplayColor.Colors newColor = (DisplayColor.Colors)range[index]; // Сгененрировать случайный цвет.
            execlude.Add(newColor); //Добавить цвет в список использованных.
            buttons[btn].SetColor(newColor); // Установить цвет в кнопку.
        }
    }
}
