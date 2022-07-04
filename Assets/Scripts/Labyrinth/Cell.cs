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

    public void SetExitWall(ExitDirection exitDirection)
    {
        GameObject curWall = null; //Стена
        switch (exitDirection)
        {
            case ExitDirection.None:
                break;
            case ExitDirection.Left:
                curWall = LeftWall;
                break;
            case ExitDirection.Right:
                curWall = RightWall;
                break;
            case ExitDirection.Upper:
                curWall = UpperWall;
                break;
            case ExitDirection.Bottom:
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
}