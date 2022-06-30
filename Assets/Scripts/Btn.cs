using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Btn : MonoBehaviour
{
    public DisplayColor.Colors Color;

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

    public void SetColor(DisplayColor.Colors color)
    {
        Color = color; // Установить цвет, за который отвечает кнопка.

        string text = null; //Установть название цвета в зависимости от цвета.
        if (color == DisplayColor.Colors.RED) text = "Красный";
        else if (color == DisplayColor.Colors.YELLOW) text = "Жёлтый";
        else if (color == DisplayColor.Colors.GREEN) text = "Зелёный";
        else if (color == DisplayColor.Colors.BLUE) text = "Синий";

        TmpText.text = text;
    }
}
