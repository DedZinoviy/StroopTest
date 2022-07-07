using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Fruit : MonoBehaviour, IPointerDownHandler
{
    public GameController controller { get; set; }

    public Color color;

    [SerializeField]
    private List<GameObject> sprites;

    private void Start()
    {
        System.Random random = new System.Random();
        GameObject sprite = sprites[random.Next(0, sprites.Count)];
        sprite.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        controller.CatchFruit(color);
        Remove();
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
