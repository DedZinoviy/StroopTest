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
        GameObject curWall = null; //Стена
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
            curWall.name = "Exit Wall"; //Переименовать стену выхода
            curWall.GetComponent<LineRenderer>().enabled = false; //Отключить видимость
            curWall.GetComponent<EdgeCollider2D>().isTrigger = true; //Сделать триггером
            curWall.AddComponent<ExitTrigger>(); //Добавление обработчика триггера
            curWall.GetComponent<ExitTrigger>().player = GameObject.FindGameObjectWithTag("Player"); //Передача параметра обработчику
            curWall.AddComponent<ExitPointer>(); //Добавление указателя на выход
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
            curWall.GetComponent<LineRenderer>().startColor = new Color(79f/256, 31f/256, 31f/256); //Установить цвет
            curWall.GetComponent<LineRenderer>().endColor = new Color(79f / 256, 31f / 256, 31f / 256); //Установить цвет
            curWall.GetComponent<EdgeCollider2D>().isTrigger = true; //Сделать триггером
        }
    }
}