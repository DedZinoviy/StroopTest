using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// Class for changeing color for window text.
/// </summary>
public class ChangeColor : MonoBehaviour
{
    /// <summary>
    ///  Text mesh pro object from interface.
    /// </summary>
    private TMP_Text _text;

    /// <summary>
    /// Current color in display.
    /// </summary>
    private Colors currentColor;

    /// <summary>
    /// Enum that displays colors of text.
    /// </summary>
    public enum Colors 
    {
        RED, // Обозначает красный цвет.
        YELLOW, // Обозначает синий цвет.
        GREEN, // Обозначает зелёный цвет.
        BLUE // Обозначает голубой цвет.
    }

    // Start is called before the first frame update
    void Start()
    {
        this.ChangeTextColor(); // Изменить цвет текста.
        this.ChangeText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Method for changing TextMeshPro color in window or in buttons.
    /// </summary>
    public void ChangeTextColor()
    {
        _text = GetComponent<TMP_Text>(); // Получить объект TextMeshPro.

        Colors random = (Colors)Enum.ToObject(typeof(Colors), UnityEngine.Random.Range(0, 4));// Сгенерировать случайный цвет.
        Color color = new Color();
        if (random == Colors.RED) color = Color.red;
        else if (random == Colors.YELLOW) { color = Color.yellow; }
        else if (random == Colors.GREEN) color = Color.green;
        else if (random == Colors.BLUE) color = Color.blue;

        this.currentColor = random; // Запнить текущий цвет.

        _text.color = color;// Установить цвет текста.
    }

    /// <summary>
    /// Method to set the color name in the TextMeshPro field.
    /// </summary>
    public void ChangeText()
    {
        _text = GetComponent<TMP_Text> (); // Получть объект TextMeshPro.
        int random = UnityEngine.Random.Range(0,4); // Сгененрировать случайное название.
        if (random == 0) _text.text = "Красный"; // Установить название согласно сгенерированному значению.
        else if (random == 1) _text.text = "Жёлтый";
        else if (random == 2) _text.text = "Зелёный";
        else if (random == 3) _text.text = "Синий";
    }
}
