using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthPoint : MonoBehaviour
{
    [SerializeField] private TMP_Text HealthText; //“екст дл€ количества здоровь€
    [SerializeField] private int MaxHealthCount = 3; //максимальное количество здоровь€ на уровне
    [SerializeField] private SceneChanger SceneChanger;
    [SerializeField] private Animation PlayerAnimation;
    [SerializeField] private FailPanel failPanel;
    private int healthCount; //текущее количество здоровь€

    private void Start()
    {
        healthCount = MaxHealthCount; //”становить текущее значение здоровь€
        SetHealthText(); //”становить текст здоровь€
    }

    private void SetHealthText()
    {
        HealthText.text = "x " + healthCount; //установить текст здоровь€
    }

    public void ReduceHealth()
    {
        if (healthCount > 0)
            healthCount--; //ќтн€ть количество здоровь€
        SetHealthText(); //ќбновить текст здоровь€
        PlayerAnimation.Play("PlayerReduceHp");
        IsFailLevel(); //ѕроверить не провален ли уровень
    }

    private void IsFailLevel()
    {
        if (healthCount == 0)
            failPanel.Open(); //¬ыйти в меню
    }
}
