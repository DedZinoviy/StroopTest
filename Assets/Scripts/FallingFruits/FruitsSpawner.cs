using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class FruitsSpawner : MonoBehaviour
{
    [SerializeField]
    private GameController controller;

    [SerializeField]
    private GameObject[] circles;

    private int[] circleIndexes = new int[5];
    private int currentIndex = 5;

    private float time = 0.5f;

    private float[] xPosotions = { -3.5f, 3.5f };
    private int[] yPositionRange = { -1, 2 };
    private Vector2 force = new Vector2(0, 4);

    private System.Random random;

    void Start()
    {
        random = new System.Random(DateTime.Now.Millisecond);
        InvokeRepeating(nameof(SpawnFruit), time, time);
    }

    private void SpawnFruit()
    {
        Vector2 currentPosition = new Vector2();
        currentPosition.x = xPosotions[random.Next(0, 2)];
        currentPosition.y = random.Next(yPositionRange[0], yPositionRange[1]);
        force.x = currentPosition.x < 0 ? 2 : -2;
        GameObject currentCurcle = GetCurrentCircle();

        GameObject newCircle = Instantiate(currentCurcle, currentPosition, Quaternion.identity);
        newCircle.GetComponent<Fruit>().controller = controller;
        newCircle.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }

    private GameObject GetCurrentCircle()
    {
        if (currentIndex == circleIndexes.Length)
        {
            circleIndexes = Enumerable.Range(0, 5).ToArray();

            for(int i = circleIndexes.Length - 1; i >= 1; i--)
            {
                int newIndex = random.Next(0, i + 1);
                (circleIndexes[i], circleIndexes[newIndex]) = (circleIndexes[newIndex], circleIndexes[i]);
                currentIndex = 0;
            }
        }
        
        GameObject currentCircle = circles[circleIndexes[currentIndex]];
        currentIndex++;
        return currentCircle;
    }
}
