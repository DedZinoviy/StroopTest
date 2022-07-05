using UnityEngine;
using UnityEditor;
public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject LeftWall;
    [SerializeField] private GameObject RightWall;
    [SerializeField] private GameObject UpperWall;
    [SerializeField] private GameObject BottomWall;

    public void SetWallsActive(bool LeftActive, bool RightActive, bool UpperActive, bool BottomActive)
    {
        LeftWall.SetActive(LeftActive);
        RightWall.SetActive(RightActive);
        UpperWall.SetActive(UpperActive);
        BottomWall.SetActive(BottomActive);
    }

    public void SetExitWall(Direction exitDirection)
    {
        GameObject curWall = null; //�����
        switch (exitDirection)
        {
            case Direction.None:
                break;
            case Direction.Left:
                curWall = LeftWall;
                break;
            case Direction.Right:
                curWall = RightWall;
                break;
            case Direction.Upper:
                curWall = UpperWall;
                break;
            case Direction.Bottom:
                curWall = BottomWall;
                break;
        }

        if (curWall != null)
        {
            curWall.name = "Exit Wall"; //������������� ����� ������
            curWall.GetComponent<LineRenderer>().enabled = false; //��������� ���������
            curWall.GetComponent<EdgeCollider2D>().isTrigger = true; //������� ���������
            curWall.AddComponent<ExitTrigger>(); //���������� ����������� ��������
            curWall.GetComponent<ExitTrigger>().player = GameObject.FindGameObjectWithTag("Player"); //�������� ��������� �����������
            curWall.AddComponent<ExitPointer>(); //���������� ��������� �� �����
        }
        else
            return;
    }

    public void SetTrapWall(Direction trapDirection)
    {
        GameObject curWall = null;
        switch (trapDirection)
        {
            case Direction.None:
                break;
            case Direction.Left:
                curWall = LeftWall;
                break;
            case Direction.Right:
                curWall = RightWall;
                break;
            case Direction.Upper:
                curWall = UpperWall;
                break;
            case Direction.Bottom:
                curWall = BottomWall;
                break;
        }

        if (curWall != null)
        {
            curWall.tag = "trap";
            curWall.SetActive(true);
            curWall.GetComponent<LineRenderer>().startColor = new Color(79f/256, 31f/256, 31f/256); //���������� ����
            curWall.GetComponent<LineRenderer>().endColor = new Color(79f / 256, 31f / 256, 31f / 256); //���������� ����
            curWall.GetComponent<EdgeCollider2D>().isTrigger = true; //������� ���������
        }
    }
}