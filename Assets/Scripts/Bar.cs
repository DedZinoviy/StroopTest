using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Vector2 originSize;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originSize = sprite.sprite.rect.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTimeBar(float percent)
    {
        var originX = originSize.x; // Получить изначальную ширину.
        var destinationX = originX * percent; // Получить требуемую ширину.
        var ratio = destinationX / originX;
        var scale = transform.localScale;
        transform.localScale = new Vector3(scale.x * ratio, scale.y, scale.z);
    }
}
