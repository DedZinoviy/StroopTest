using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissZone : MonoBehaviour
{
    [SerializeField]
    private GameController controller;

    [SerializeField]
    private Camera mainCamera;

    private float safeTime = 0;

    private void Update()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y = -mainCamera.orthographicSize - 1.8f;
        transform.position = currentPosition;

        if (safeTime > 0)
        {
            safeTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Fruit missedFruit = collision.gameObject.GetComponent<Fruit>();
        if (missedFruit != null)
        {
            if (safeTime <= 0)
                controller.MissFruit(missedFruit.color);
            
            missedFruit.Remove();
        }
    }

    public void ActivateSafeTime()
    {
        safeTime = 0.5f;
    }
}
