using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class SetButtonText : MonoBehaviour
{
    private bool SetNeededBtn = false;
    private HashSet<ChangeColor.Colors> execlude = new HashSet<ChangeColor.Colors>();
    [SerializeField] private List<TMP_Text> buttons; 


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
        buttons.Clear();
        int random = -1;
        buttons.Add(GameObject.Find("Text").GetComponent<TMP_Text>());
        buttons.Add(GameObject.Find("Text2").GetComponent<TMP_Text>());
        buttons.Add(GameObject.Find("Text3").GetComponent<TMP_Text>());
        buttons.Add(GameObject.Find("Text4").GetComponent<TMP_Text>());

        if (SetNeededBtn == false) // Если кнопка с цветом дисплея пока не установлена...
        {
            ChangeColor.Colors color = ChangeColor.currentColor;
            string text = null;
            if (color == ChangeColor.Colors.RED) text = "Красный";
            else if (color == ChangeColor.Colors.YELLOW) text = "Жёлтый";
            else if (color == ChangeColor.Colors.GREEN) text = "Зелёный";
            else if (color == ChangeColor.Colors.BLUE) text = "Синий";

            random = UnityEngine.Random.Range(0, 3);
            buttons[random].text = text;
            execlude.Add(color);
            
            SetNeededBtn = true;
        }

        var btnRange = Enumerable.Range(0, 4).Where(k => k != random);

        foreach (var btn in btnRange)
        {
            List<int> range = Enumerable.Range(0, 4).Where(j => !execlude.Contains((ChangeColor.Colors)Enum.ToObject(typeof(ChangeColor.Colors), j))).ToList();
            var rand = new System.Random();
            int index = rand.Next(range.Count());
            ChangeColor.Colors newColor = (ChangeColor.Colors)range[index];
            string text = null;
            if (newColor == ChangeColor.Colors.RED) text = "Красный";
            else if (newColor == ChangeColor.Colors.YELLOW) text = "Жёлтый";
            else if (newColor == ChangeColor.Colors.GREEN) { text = "Зелёный"; }
            else if (newColor == ChangeColor.Colors.BLUE) { text = "Синий"; }
            execlude.Add(newColor);
            buttons[btn].text = text;
        }
    }
}
