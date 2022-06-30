using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    [SerializeField] private DisplayColor EtalonColor;
    [SerializeField] private SetButtonText text;
    [SerializeField] private TMP_Text ScoreEdit;
    //[SerializeField] private List<Btn> buttons;
    private int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void isCorrectAnswer(Btn button)
    {
        if (EtalonColor.currentColor == button.Color)
        {
            Score++;
        }
        ScoreEdit.text = "Ñ÷¸ò: " + Score;
        text.SetText();
    }
}
