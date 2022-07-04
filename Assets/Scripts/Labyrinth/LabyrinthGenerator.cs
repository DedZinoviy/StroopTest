using System.Collections.Generic;
using UnityEngine;

public enum ExitDirection
{ //����������� ������
    None,
    Left,
    Right,
    Upper,
    Bottom
}
public class LabyrinthCell
{
    public int X; //�������������� ���������� ������ ���������
    public int Y; //������������ ���������� ������ ���������

    public bool LeftWall = true; //����, ������������ ������������� ����� �����
    public bool RightWall = true; //����, ������������ ������������� ������ �����
    public bool UpperWall = true; //����, ������������ ������������� ������� �����
    public bool BottomWall = true; //����, ������������ ������������� ������ �����

    public bool IsVisited = false; //����, ������������ ���� �� �������� ������

    public int DystanceStart; //���������� �� ����� ������

    public ExitDirection exitDirection = ExitDirection.None; //����������� ������ � �������
}
public class LabyrinthGenerator
{
    public int Width { get; private set; } //������ ���������
    public int Height { get; private set; } //������ ���������
    public int XStart { get; private set; } //�������������� ���������� ��������� �����
    public int YStart { get; private set; } //������������ ���������� ��������� �����

    public LabyrinthGenerator(int width, int height, int xStart = 0, int yStart = 0)
    {
        Width = width;
        Height = height;
        XStart = xStart;
        YStart = yStart;
    }


    public LabyrinthCell[,] GenerateLabyrinth()
    {
        LabyrinthCell[,] labyrinthCells = new LabyrinthCell[Width, Height]; //������ ���������

        //��� ���� ����� ���������
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                labyrinthCells[x, y] = new LabyrinthCell { X = x, Y = y }; //������ ��������������� ����������
            }
        }

        LabyrinthCell startCell = labyrinthCells[XStart, YStart]; //������ ������

        RemoveWallsBacktracking(labyrinthCells, startCell); //������� �����

        MarkExitWall(labyrinthCells); //�������� ����� ������
        return labyrinthCells; //������� ������ ���������
    }

    private void RemoveWallsBacktracking(LabyrinthCell[,] labyrinth, LabyrinthCell curCell, int distanceStart = 0)
    {
        curCell.IsVisited = true; //������� ������� ������� ����������
        curCell.DystanceStart = distanceStart; //���������� ���������� �� ��������� �������

        List<LabyrinthCell> unvisited = GetUnvisited(labyrinth,curCell); //������ ������������ �������� ������

        while (unvisited.Count>0)
        { //���� ���� ������������ �������� �������
            LabyrinthCell nextCell = unvisited[Random.Range(0, unvisited.Count)]; //��������� �������� ������������ �������
            RemoveWall(curCell, nextCell); //������� ����� ����� ������� � ��������� ��������
            RemoveWallsBacktracking(labyrinth, nextCell, distanceStart+1); //������� �����
            unvisited = GetUnvisited(labyrinth, curCell); //������� ������ ����������� �������� ������
        }
        
    }

    private List<LabyrinthCell> GetUnvisited(LabyrinthCell[,] labyrinth, LabyrinthCell curCell)
    {

        List<LabyrinthCell> unvisited = new List<LabyrinthCell>(); //������ ������������ �������� ������
        int x = curCell.X; //�������������� ���������� ������� �������
        int y = curCell.Y; //������������ ���������� ������� �������

        //�������� ��� ������������ �������� ������� � ��������������� ������...
        if (x > 0 && !labyrinth[x - 1, y].IsVisited)
            unvisited.Add(labyrinth[x - 1, y]); //...���� �� �������� ������ ����� �� ������� 
        if (x < Width - 1 && !labyrinth[x + 1, y].IsVisited)
            unvisited.Add(labyrinth[x + 1, y]); //...���� �� �������� ������ ������ �� ������� 
        if (y > 0 && !labyrinth[x, y - 1].IsVisited)
            unvisited.Add(labyrinth[x, y - 1]); //...���� �� �������� ������ ������ �� ������� 
        if (y < Height - 1 && !labyrinth[x, y + 1].IsVisited)
            unvisited.Add(labyrinth[x, y + 1]); //...���� �� �������� ������ ����� �� ������� 
        return unvisited;
    }
    private void RemoveWall(LabyrinthCell first, LabyrinthCell second)
    {
        if (first.X == second.X)
        { //���� ������ � ������ ������ �� ����� ����� �� ���������
            if (first.Y < second.Y)
            { //���� ������ ������ ���� ������
                first.UpperWall = false; //������ ������� ����� ������ ������
                second.BottomWall = false; //������ ������ ����� ������ ������
            }
            else
            { //�����
                first.BottomWall = false; //������ ������ ����� ������ ������
                second.UpperWall = false; //������ ������� ����� ������ ������
            }
        }
        else if (first.Y == second.Y)
        {//��������� ������ � ������ ������ �� ����� ����� �� �����������
            if (first.X < second.X)
            {//���� ������ ������ ����� ������
                first.RightWall = false; //������ ������ ����� ������ ������
                second.LeftWall = false; //������ ����� ����� ������ ������
            }
            else
            { //�����
                first.LeftWall = false; //������ ����� ����� ������ ������
                second.RightWall = false; //������ ������ ����� ������ ������
            }
        }
    }

    private void MarkExitWall(LabyrinthCell[,] labyrinth)
    {
        LabyrinthCell farCell = labyrinth[XStart, YStart]; //����� ������� ������ �� ���������

        //����� ����� ������� ������ �� ��������� �� ��������� ������ 
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

        // �������� ��������������� �����
        if (farCell.X == 0)
            farCell.exitDirection = ExitDirection.Left;
        else if (farCell.X == Width - 1)
            farCell.exitDirection = ExitDirection.Right;
        else if (farCell.Y == 0)
            farCell.exitDirection = ExitDirection.Bottom;
        else if (farCell.Y == Height - 1)
            farCell.exitDirection = ExitDirection.Upper;
    }
}
