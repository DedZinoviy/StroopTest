using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthCreator : MonoBehaviour
{
    [SerializeField] private GameObject Cell; //������ ���������
    private int Width; //������ ���������
    private int Height; //������ ���������
    [SerializeField] private Camera Camera;
    private int CountTraps; //���������� �������
    [SerializeField] private PlayerPrefs playerPrefs;
    private float sizeX; //������ ������ �� X
    private float sizeY; //������ ������ �� Y


    private void Start()
    {
        sizeX = Cell.GetComponent<Transform>().localScale.x;
        sizeY = Cell.GetComponent<Transform>().localScale.y;
        Set�omplexityParams();
        CreateLabyrinth(); //������� ��������
    }

    private void CreateLabyrinth()
    {
        GameObject Cells = new GameObject("Cells"); //������ ��� ���� �����
        LabyrinthGenerator generator = new LabyrinthGenerator(Width, Height); //��������� ��������� 
        LabyrinthCell[,] labyrinth = generator.GenerateLabyrinth(); //��������� ���������
        TrapGenerator trapGenerator = new TrapGenerator();
        labyrinth = trapGenerator.GenerateTrapWalls(labyrinth, CountTraps);

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
                if (curCell.exitDirection != Direction.None)
                    cell.SetExitWall(curCell.exitDirection); //���������� �����
                if (curCell.trapDirection != Direction.None)
                    cell.SetTrapWall(curCell.trapDirection); //���������� �������
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

    private void Set�omplexityParams()
    {
        int levelComplexity = playerPrefs.LoadLevelComplexity(); //������� ���������
        Vector3Int[] param = new Vector3Int[5]; //��������� ��������������� ������ ���������
        param[0] = new Vector3Int(5, 5, 3);
        param[1] = new Vector3Int(8, 8, 7);
        param[2] = new Vector3Int(10, 10, 9);
        param[3] = new Vector3Int(12, 12, 11);
        param[4] = new Vector3Int(15, 15, 14);

        Width = param[levelComplexity - 1].x; //������
        Height = param[levelComplexity - 1].y; //������
        CountTraps = param[levelComplexity - 1].z; //���������� �������
    }
}
