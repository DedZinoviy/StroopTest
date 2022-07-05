using System.Collections.Generic;

using UnityEngine;

public class TrapGenerator
{
    public LabyrinthCell[,] GenerateTrapWalls(LabyrinthCell[,] labyrinthCells, int countTraps)
    {
        Vector2Int[] indexes = RandomIndexes(countTraps, labyrinthCells.GetLength(0), labyrinthCells.GetLength(1)); //Получить случайные индексы
        LabyrinthCell[,] updatedLabyrinthCells = labyrinthCells;
        for(int i =0; i< indexes.Length; i++)
        { //Для каждого выбранного элемента
            updatedLabyrinthCells[indexes[i].x, indexes[i].y].trapDirection = SelectTrapDirection(updatedLabyrinthCells[indexes[i].x, indexes[i].y]); //Выбрать направление ловушки
        }

        return updatedLabyrinthCells;
    }

    private Direction SelectTrapDirection(LabyrinthCell cell)
    {
        Direction[] possibleDirections = PossibleTrapDirections(cell); //Определить все возможные направления ловушки
        return possibleDirections[Random.Range(0, possibleDirections.Length - 1)]; //Вернуть случайное направление
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
        List<Vector2Int> indexes = new List<Vector2Int>(); //Случайные индексы
        int i = 0;
        while (i < count)
        {
            int curIndexX = Random.Range(0, sizeArrX - 1); //Текущий индекс x
            int curIndexY = Random.Range(0, sizeArrY - 1); //Текущий индекс y
            if (!indexes.Contains(new Vector2Int(curIndexX, curIndexY)))
            { //Индекса ещё не было
                indexes.Add(new Vector2Int(curIndexX, curIndexY));
                i++;
            }
        }
        return indexes.ToArray();
    }
}
