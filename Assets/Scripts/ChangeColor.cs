using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    private TMP_Text _text;
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
        _text = GetComponent<TMP_Text>();
        int random = Random.Range(0, 3); // Сгенерировать случайный цвет.
        Color color = new Color();
        if (random == 0) color = Color.red;
        else if (random == 1) { color = Color.yellow; }
        else if (random == 2) color = Color.green;
        else if (random == 3) color = Color.blue;
        //extMeshPro text = new TextMeshPro();
        _text.color = color;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
