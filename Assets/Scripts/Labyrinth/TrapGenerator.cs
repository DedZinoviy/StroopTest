using System.Collections.Generic;

using UnityEngine;

public class TrapGenerator
{
    public LabyrinthCell[,] GenerateTrapWalls(LabyrinthCell[,] labyrinthCells, int countTraps)
    {
        Vector2Int[] indexes = RandomIndexes(countTraps, labyrinthCells.GetLength(0), labyrinthCells.GetLength(1)); //�������� ��������� �������
        LabyrinthCell[,] updatedLabyrinthCells = labyrinthCells;
        for(int i =0; i< indexes.Length; i++)
        { //��� ������� ���������� ��������
            updatedLabyrinthCells[indexes[i].x, indexes[i].y].trapDirection = SelectTrapDirection(updatedLabyrinthCells[indexes[i].x, indexes[i].y]); //������� ����������� �������
        }

        return updatedLabyrinthCells;
    }

    private Direction SelectTrapDirection(LabyrinthCell cell)
    {
        Direction[] possibleDirections = PossibleTrapDirections(cell); //���������� ��� ��������� ����������� �������
        return possibleDirections[Random.Range(0, possibleDirections.Length - 1)]; //������� ��������� �����������
    }

    private Direction[] PossibleTrapDirections(LabyrinthCell cell)
    {
        List<Direction> directions = new List<Direction>();
        if (!cell.LeftWall)
            directions.Add(Direction.Left);
        if (!cell.RightWall)
            directions.Add(Direction.Right);
        if (!cell.UpperWall)
            directions.Add(Direction.Upper);
        if (!cell.BottomWall)
            directions.Add(Direction.Bottom);
        return directions.ToArray();
    }

    private Vector2Int[] RandomIndexes(int count, int sizeArrX, int sizeArrY)
    {
        List<Vector2Int> indexes = new List<Vector2Int>(); //��������� �������
        int i = 0;
        while (i < count)
        {
            int curIndexX = Random.Range(0, sizeArrX - 1); //������� ������ x
            int curIndexY = Random.Range(0, sizeArrY - 1); //������� ������ y
            if (!indexes.Contains(new Vector2Int(curIndexX, curIndexY)))
            { //������� ��� �� ����
                indexes.Add(new Vector2Int(curIndexX, curIndexY));
                i++;
            }
        }
        return indexes.ToArray();
    }
}
