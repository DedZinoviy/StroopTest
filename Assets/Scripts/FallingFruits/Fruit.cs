using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Fruit : MonoBehaviour, IPointerDownHandler
{
    public GameController controller { get; set; }

    public Color color;
    
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
