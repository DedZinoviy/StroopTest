using System.Collections.Generic;
using UnityEngine;

public enum Direction
{ //Направление стены
    None,
    Left,
    Right,
    Upper,
    Bottom
}
public class LabyrinthCell
{
    public int X; //Горизонтальная координата ячейки лабиринта
    public int Y; //Вертикальная координата ячейки лабиринта

    public bool LeftWall = true; //Флаг, показывающий существование левой стены
    public bool RightWall = true; //Флаг, показывающий существование правой стены
    public bool UpperWall = true; //Флаг, показывающий существование верхней стены
    public bool BottomWall = true; //Флаг, показывающий существование нижней стены

    public bool IsVisited = false; //Флаг, показывающий была ли посещена ячейка

    public int DystanceStart; //Расстояние до точки старта

    public Direction exitDirection = Direction.None; //Направление выхода в ячейкей

    public Direction trapDirection = Direction.None; //Направление ловушки
}
public class LabyrinthGenerator
{
    public int Width { get; private set; } //Ширина лабиринта
    public int Height { get; private set; } //Высота лабиринта
    public int XStart { get; private set; } //Горизонтальная координата стартовой точки
    public int YStart { get; private set; } //Вертикальная координата стартовой точки

    public LabyrinthGenerator(int width, int height, int xStart = 0, int yStart = 0)
    {
        Width = width;
        Height = height;
        XStart = xStart;
        YStart = yStart;
    }


    public LabyrinthCell[,] GenerateLabyrinth()
    {
        LabyrinthCell[,] labyrinthCells = new LabyrinthCell[Width, Height]; //Ячейки лабиринта

        //Для всех ячеек лабиринта
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                labyrinthCells[x, y] = new LabyrinthCell { X = x, Y = y }; //Задать соответствующие координаты
            }
        }

        LabyrinthCell startCell = labyrinthCells[XStart, YStart]; //Ячейка старта

        RemoveWallsBacktracking(labyrinthCells, startCell); //Удалить стены

        MarkExitWall(labyrinthCells); //Отметить стену выхода
        return labyrinthCells; //Вернуть ячейки лабиринта
    }

    private void RemoveWallsBacktracking(LabyrinthCell[,] labyrinth, LabyrinthCell curCell, int distanceStart = 0)
    {
        curCell.IsVisited = true; //Считать текущую вершину посещённой
        curCell.DystanceStart = distanceStart; //Установить расстояние до стартовой позиции

        List<LabyrinthCell> unvisited = GetUnvisited(labyrinth,curCell); //Список непосещённых соседних вершин

        while (unvisited.Count>0)
        { //Пока есть непосещенные соседние вершины
            LabyrinthCell nextCell = unvisited[Random.Range(0, unvisited.Count)]; //Случайняя соседняя непосещённая вершина
            RemoveWall(curCell, nextCell); //Удалить стену между текущей и следующей ячейками
            RemoveWallsBacktracking(labyrinth, nextCell, distanceStart+1); //Удалить стены
            unvisited = GetUnvisited(labyrinth, curCell); //Обновит список неосещённых соседних вершин
        }
        
    }

    private List<LabyrinthCell> GetUnvisited(LabyrinthCell[,] labyrinth, LabyrinthCell curCell)
    {

        List<LabyrinthCell> unvisited = new List<LabyrinthCell>(); //Список непосещённых соседних вершин
        int x = curCell.X; //Горизонтальная координата текущей вершины
        int y = curCell.Y; //Вертикальная координата текущей вершины

        //Добавить все непосещённые соседние вершины в соответствующий список...
        if (x > 0 && !labyrinth[x - 1, y].IsVisited)
            unvisited.Add(labyrinth[x - 1, y]); //...если не посещена ячейка слева от текущей 
        if (x < Width - 1 && !labyrinth[x + 1, y].IsVisited)
            unvisited.Add(labyrinth[x + 1, y]); //...если не посещена ячейка справа от текущей 
        if (y > 0 && !labyrinth[x, y - 1].IsVisited)
            unvisited.Add(labyrinth[x, y - 1]); //...если не посещена ячейка сверху от текущей 
        if (y < Height - 1 && !labyrinth[x, y + 1].IsVisited)
            unvisited.Add(labyrinth[x, y + 1]); //...если не посещена ячейка снизу от текущей 
        return unvisited;
    }
    private void RemoveWall(LabyrinthCell first, LabyrinthCell second)
    {
        if (first.X == second.X)
        { //Если первая и вторая ячейки на одной линии по вертикали
            if (first.Y < second.Y)
            { //Если первая ячейка ниже второй
                first.UpperWall = false; //Убрать верхнюю стену первой ячейки
                second.BottomWall = false; //Убрать нижнюю стену второй ячейки
            }
            else
            { //Иначе
                first.BottomWall = false; //Убрать нижнюю стену первой ячейки
                second.UpperWall = false; //Убрать верхнюю стену второй ячейки
            }
        }
        else if (first.Y == second.Y)
        {//ИначеЕсли первая и вторая ячейки на одной линии по горизонтали
            if (first.X < second.X)
            {//Если первая ячейка левее второй
                first.RightWall = false; //Убрать правую стену первой ячейки
                second.LeftWall = false; //Убрать левую стену второй ячейки
            }
            else
            { //Иначе
                first.LeftWall = false; //Убрать левую стену первой ячейки
                second.RightWall = false; //Убрать правую стену второй ячейки
            }
        }
    }

    private void MarkExitWall(LabyrinthCell[,] labyrinth)
    {
        LabyrinthCell farCell = labyrinth[XStart, YStart]; //самая дальняя ячейка от стартовой

        //Найти самую дальнюю ячейку по периметру от стартовой ячейки 
        for (int x = 0; x < Width; x++)
        {
            if (labyrinth[x, 0].DystanceStart > farCell.DystanceStart)
                farCell = labyrinth[x, 0];
            if (labyrinth[x, Height - 1].DystanceStart > farCell.DystanceStart)
                farCell = labyrinth[x, Height - 1];
        }

        for (int y = 0; y < Height; y++)
        {
            if (labyrinth[0, y].DystanceStart > farCell.DystanceStart)
                farCell = labyrinth[0, y];
            if (labyrinth[Width - 1, y].DystanceStart > farCell.DystanceStart)
                farCell = labyrinth[Width - 1, y];
        }

        // Отметить соответствующую стену
        if (farCell.X == 0)
            farCell.exitDirection = Direction.Left;
        else if (farCell.X == Width - 1)
            farCell.exitDirection = Direction.Right;
        else if (farCell.Y == 0)
            farCell.exitDirection = Direction.Bottom;
        else if (farCell.Y == Height - 1)
            farCell.exitDirection = Direction.Upper;
    }
}
