using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthPoint : MonoBehaviour
{
    [SerializeField] private TMP_Text HealthText; //����� ��� ���������� ��������
    [SerializeField] private int MaxHealthCount = 3; //������������ ���������� �������� �� ������
    [SerializeField] private SceneChanger SceneChanger;
    [SerializeField] private Animation PlayerAnimation;
    [SerializeField] private FailPanel failPanel;
    private int healthCount; //������� ���������� ��������

    private void Start()
    {
        healthCount = MaxHealthCount; //���������� ������� �������� ��������
        SetHealthText(); //���������� ����� ��������
    }

    private void SetHealthText()
    {
        HealthText.text = "x " + healthCount; //���������� ����� ��������
    }

    public void ReduceHealth()
    {
        if (healthCount > 0)
            healthCount--; //������ ���������� ��������
        SetHealthText(); //�������� ����� ��������
        PlayerAnimation.Play("PlayerReduceHp");
        IsFailLevel(); //��������� �� �������� �� �������
    }

    private void IsFailLevel()
    {
        if (healthCount == 0)
            failPanel.Open(); //����� � ����
    }
}
