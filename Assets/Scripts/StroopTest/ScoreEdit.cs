using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreEdit : MonoBehaviour
{
    private PlayerPrefs m_PlayerPrefs = new PlayerPrefs();
    private TMP_Text m_Text;

    // Start is called before the first frame update
    void Start()
    {
       int score =  m_PlayerPrefs.LoadScore();
       m_Text = GetComponent<TMP_Text>();
       m_Text.text =  "Максимальный результат: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
