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
        Color = color; // ���������� ����, �� ������� �������� ������.

        string text = null; //��������� �������� ����� � ����������� �� �����.
        if (color == DisplayColor.Colors.RED) text = "�������";
        else if (color == DisplayColor.Colors.YELLOW) text = "Ƹ����";
        else if (color == DisplayColor.Colors.GREEN) text = "������";
        else if (color == DisplayColor.Colors.BLUE) text = "�����";

        TmpText.text = text;
    }
}
