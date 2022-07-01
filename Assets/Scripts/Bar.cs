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

    [SerializeField]private Color color;

    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>(); // Установить объект шкалы при запуске.
        originSize = sprite.sprite.rect.size; // Узнать изначальный размер шкалы.
        color = sprite.color;
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
        ChangeBarColor(ratio);
        var scale = transform.localScale; // Получить текущий масштаб.
        transform.localScale = new Vector3(scale.x * ratio, scale.y, scale.z); //Изменить масштаб.
       // ChangeBarColor(percent);
    }

    public void ChangeBarColor(float percent)
    {
        if (color.r <= 1) { color.r = 1 - percent; color.g = percent;}
        sprite.color = new Color(color.r,color.g,color.b);
    }
}
