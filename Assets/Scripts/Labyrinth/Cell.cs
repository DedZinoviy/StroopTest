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
        GameObject curWall = null; //�����
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
}