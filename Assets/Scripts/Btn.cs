using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Class for describing color buttons in app.
/// </summary>
public class Btn : MonoBehaviour
{
    /// <summary>
    /// The color that the button is responsible for.
    /// </summary>
    public DisplayColor.Colors Color;

    /// <summary>
    /// TextMeshPro text to related button.
    /// </summary>
    [SerializeField]
    private TMP_Text TmpText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// The method of setting the color for which the button should be responsible.
    /// </summary>
    /// <param name="color">Related color</param>
    public void SetColor(DisplayColor.Colors color)
    {
        Color = color; // Установить цвет, за который отвечает кнопка.

        string text = null; //Установть название цвета в зависимости от цвета.
        if (color == DisplayColor.Colors.RED) text = "Красный";
        else if (color == DisplayColor.Colors.YELLOW) text = "Жёлтый";
        else if (color == DisplayColor.Colors.GREEN) text = "Зелёный";
        else if (color == DisplayColor.Colors.BLUE) text = "Синий";

        TmpText.text = text; // Присвоить кнопке текст с названием.
    }
}
