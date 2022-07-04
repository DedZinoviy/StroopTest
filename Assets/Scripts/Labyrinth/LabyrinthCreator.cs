using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthCreator : MonoBehaviour
{
    [SerializeField] private GameObject Cell; //������ ���������
    [SerializeField] private int Width = 5; //������ ���������
    [SerializeField] private int Height = 5; //������ ���������
    [SerializeField] private Camera Camera;
    private float sizeX; //������ ������ �� X
    private float sizeY; //������ ������ �� Y


    private void Start()
    {
        sizeX = Cell.GetComponent<Transform>().localScale.x;
        sizeY = Cell.GetComponent<Transform>().localScale.y;
        CreateLabyrinth(); //������� ��������
    }

    private void CreateLabyrinth()
    {
        GameObject Cells = new GameObject("Cells"); //������ ��� ���� �����
        LabyrinthGenerator generator = new LabyrinthGenerator(Width, Height); //��������� ��������� 
        LabyrinthCell[,] labyrinth = generator.GenerateLabyrinth(); //��������� ���������

        //����������� ��������� �� �����
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                GameObject curObject = Instantiate(Cell, new Vector2(x*sizeX, y*sizeY), Quaternion.identity); //������� ������
                curObject.transform.SetParent(Cells.transform); //�������� ������� ������ �� ���� �������
                Cell cell = curObject.GetComponent<Cell>();
                LabyrinthCell curCell = labyrinth[x, y];
                cell.SetWallsActive(curCell.LeftWall, curCell.RightWall, curCell.UpperWall, curCell.BottomWall); //�������� ��������� ���� � ������
                if (curCell.exitDirection != ExitDirection.None)
                    cell.SetExitWall(curCell.exitDirection);
            }
        }

        ChangeCameraBounds();
    }

    private void ChangeCameraBounds()
    {
        float left = 0;
        float right = Width * sizeX;
        float up = Height * sizeY;
        float bottom = 0;
        Camera.GetComponent<CameraController>().SetCameraBounds(left, right, up, bottom);
    }
}
