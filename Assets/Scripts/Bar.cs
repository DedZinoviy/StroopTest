using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for describing scale bars in app.
/// </summary>
public class Bar : MonoBehaviour
{
    /// <summary>
    /// Bar's sprite.
    /// </summary>
    private Image sprite;

    /// <summary>
    /// Bar''s size.
    /// </summary>
    private Vector2 originSize;

    private Vector3 originScale;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<Image>(); // ���������� ������ ����� ��� �������.
        originSize = sprite.sprite.rect.size; // ������ ����������� ������ �����.
        originScale = transform.localScale;
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
        var originX = originSize.x; // �������� ����������� ������.
        var destinationX = originX * percent; // �������� ��������� ������.
        var ratio = destinationX / originX; // ��������� ��������� ���������� ������� � ���������.
        var scale = transform.localScale; // �������� ������� �������.
        transform.localScale = new Vector3(scale.x * ratio, scale.y, scale.z); //�������� �������.
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

    public void ReturnToOriginScale()
    {
        transform.localScale = originScale;
    }
}
