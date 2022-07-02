using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for describing scale bars in app.
/// </summary>
public class Bar : MonoBehaviour
{
    /// <summary>
    /// Bar's sprite.
    /// </summary>
    private SpriteRenderer sprite;

    /// <summary>
    /// Bar''s size.
    /// </summary>
    private Vector2 originSize;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>(); // Установить объект шкалы при запуске.
        originSize = sprite.sprite.rect.size; // Узнать изначальный размер шкалы.
    }

    // Update is called once per frame
    void Update()
    {
        //sprite.color = Color.Lerp(color, Color.red, Time.captureDeltaTime);
    }

    /// <summary>
    /// Method for changing bar's size
    /// </summary>
    /// <param name="percent">the proportion of the original size that the school should occupy.</param>
    public void ChangeBarSize(float percent)
    {
        var originX = originSize.x; // Получить изначальную ширину.
        var destinationX = originX * percent; // Получить требуемую ширину.
        var ratio = destinationX / originX; // Посчитать отношение требуемого размера к исходному.
        var scale = transform.localScale; // Получить текущий масштаб.
        transform.localScale = new Vector3(scale.x * ratio, scale.y, scale.z); //Изменить масштаб.
    }

    public void ChangeBarColor(float percent)
    {
        if (percent > 0.5)
        {
            percent = percent / 2;
            sprite.color = Color.Lerp(Color.green, new Color((float)0.9,1,0,1), 1 - percent);
        }
        else
        {
            percent = percent * 2;
            sprite.color = Color.Lerp(new Color ((float)0.8,1,0,1), Color.red, 1 - percent);
        }
    }
}
