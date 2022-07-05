using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthCreator : MonoBehaviour
{
    [SerializeField] private GameObject Cell; //Ячейка лабиринта
    [SerializeField] private int Width = 5; //Ширина лабиринта
    [SerializeField] private int Height = 5; //Высота лабиринта
    [SerializeField] private Camera Camera;
    [SerializeField] private int CountTraps = 3;
    private float sizeX; //Размер ячейки по X
    private float sizeY; //Размер ячейки по Y


    private void Start()
    {
        sizeX = Cell.GetComponent<Transform>().localScale.x;
        sizeY = Cell.GetComponent<Transform>().localScale.y;
        CreateLabyrinth(); //Создать лабиринт
    }

    private void CreateLabyrinth()
    {
        GameObject Cells = new GameObject("Cells"); //Объект для всех ячеек
        LabyrinthGenerator generator = new LabyrinthGenerator(Width, Height); //Генератор лабиринта 
        LabyrinthCell[,] labyrinth = generator.GenerateLabyrinth(); //Генерация лабиринта
        TrapGenerator trapGenerator = new TrapGenerator();
        labyrinth = trapGenerator.GenerateTrapWalls(labyrinth, CountTraps);

        //Отображение лабиринта на сцене
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                GameObject curObject = Instantiate(Cell, new Vector2(x*sizeX, y*sizeY), Quaternion.identity); //Создать ячейку
                curObject.transform.SetParent(Cells.transform); //Добавить текущую ячейку ко всем ячейкам
                Cell cell = curObject.GetComponent<Cell>();
                LabyrinthCell curCell = labyrinth[x, y];
                cell.SetWallsActive(curCell.LeftWall, curCell.RightWall, curCell.UpperWall, curCell.BottomWall); //Изменить видимость стен в ячейке
                if (curCell.exitDirection != Direction.None)
                    cell.SetExitWall(curCell.exitDirection); //Установить выход
                if (curCell.trapDirection != Direction.None)
                    cell.SetTrapWall(curCell.trapDirection); //Установить ловушку
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
