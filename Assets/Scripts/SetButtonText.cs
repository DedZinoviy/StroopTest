using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class SetButtonText : MonoBehaviour
{
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
        var rand = new System.Random(DateTime.Now.Millisecond); // ������������� ��������� ��������.
        int random = -1;
        HashSet<DisplayColor.Colors> execlude = new HashSet<DisplayColor.Colors>();
        random = rand.Next(0, 4); // ������� ��������� ������ ��� ��������� �����.
        DisplayColor.Colors color = curColor.currentColor; // �������� ������������� � ���� ����.
        buttons[random].SetColor(color); // ��������� ���� � ������.
        execlude.Add(color); // �������� ���� � ������ �������������� ������.

        var btnRange = Enumerable.Range(0, 4).Where(k => k != random); // ������� ����� ������ ������ � ������ �����������.

        foreach (var btn in btnRange) // ��� ������ ��������� ������...
        {
            // ������� ������ ������, ������� ����� ������������.
            List<int> range = Enumerable.Range(0, 4).Where(j => !execlude.Contains((DisplayColor.Colors)j)).ToList();
            int index = rand.Next(range.Count());
            DisplayColor.Colors newColor = (DisplayColor.Colors)range[index]; // �������������� ��������� ����.
            execlude.Add(newColor); //�������� ���� � ������ ��������������.
            buttons[btn].SetColor(newColor); // ���������� ���� � ������.
        }
    }
}
