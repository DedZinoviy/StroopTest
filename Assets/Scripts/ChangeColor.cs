using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    /// Enum that displays colors of text.
    /// </summary>
    private enum Colors 
    {
        RED, // Обозначает красный цвет.
        YELLOW, // Обозначает синий цвет.
        GREEN, // Обозначает зелёный цвет.
        BLUE // Обозначает голубой цвет.
    }

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>(); // Получить объект TextMeshPro.

        // Сгенерировать случайный цвет.
        int random = Random.Range(0, 3);
        Color color = new Color();
        if (random == 0) color = Color.red;
        else if (random == 1) { color = Color.yellow; }
        else if (random == 2) color = Color.green;
        else if (random == 3) color = Color.blue;

        // Установить цвет текста.
        _text.color = color;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
