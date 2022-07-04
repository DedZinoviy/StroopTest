using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissZone : MonoBehaviour
{
    [SerializeField]
    private GameController controller;

    [SerializeField]
    private Camera mainCamera;

    private void Update()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y = -mainCamera.orthographicSize - 1.8f;
        transform.position = currentPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Fruit missedFruit = collision.gameObject.GetComponent<Fruit>();
        if (missedFruit != null)
        {
            controller.MissFruit(missedFruit.color);
            missedFruit.Remove();
        }
    }
}
