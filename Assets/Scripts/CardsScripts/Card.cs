using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;// Не забудьте процитировать
using UnityEngine.EventSystems;

// Статус карты спереди и сзади
public enum CardState
{
    Front,
    Back
}

public enum CardType
{
    APPLE,
    BANANA,
    CHERRY,
    GRAPE,
    ORANGE,
    PEAR,
    PLUM,
    QIWI,
    STRAWBERRY,
    WATERMELLON
}

public class Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private List<GameObject> mFronts;// Передняя часть карты
    
    [SerializeField]
    private GameObject mBack;// Оборотная сторона карты
    
    [SerializeField]
    private float mTime = 0.3f;

    public MechanicGame mechanic { get; set; }

    public int Id { get; set; }

    public CardType type { get; set; }

    private CardState mCardState = CardState.Back;// Текущее состояние карты: лицевая или оборотная?
    private bool isActive = false;// истина означает, что ролловер выполняется и его нельзя прерывать
    private GameObject mFront;
    private Vector3 cardScale;

    /// <summary>
    /// Инициализируем угол карты, согласно mCardState
    /// </summary>
    public void Init()
    {
        if (mCardState == CardState.Front)
        {
            // Если вы начинаете спереди, поверните назад на 90 градусов, чтобы спина не была видна
            mFront.transform.eulerAngles = Vector3.zero;
            mBack.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        {
            // Начнем со спины, то же самое
            mFront.transform.eulerAngles = new Vector3(0, 90, 0);
            mBack.transform.eulerAngles = Vector3.zero;
        }
    }
    private void Start()
    {
        mFront = mFronts[(int)type];
        mFront.SetActive(true);
        Init();
    }

    /// <summary>
    /// Интерфейс зарезервирован для внешних вызовов
    /// </summary>
    public void StartBack()
    {
        if (isActive)
            return;
        StartCoroutine(ToBack());
    }
    /// <summary>
    /// Интерфейс зарезервирован для внешних вызовов
    /// </summary>
    public void StartFront()
    {
        if (isActive)
            return;
        StartCoroutine(ToFront());
    }

    /// <summary>
    /// перевернуть на спину
    /// </summary>
	IEnumerator ToBack()
    {
        isActive = true;
        mFront.transform.DORotate(new Vector3(0, 90, 0), mTime);
        for (float i = mTime; i >= 0; i -= Time.deltaTime)
            yield return 0;
        mBack.transform.DORotate(new Vector3(0, 0, 0), mTime);
        isActive = false;
        mCardState = CardState.Back;

    }
    /// <summary>
    /// перевернуть на передний план
    /// </summary>
    IEnumerator ToFront()
    {
        isActive = true;
        mBack.transform.DORotate(new Vector3(0, 90, 0), mTime);
        for (float i = mTime; i >= 0; i -= Time.deltaTime)
            yield return 0;
        mFront.transform.DORotate(new Vector3(0, 0, 0), mTime);
        isActive = false;
        mCardState = CardState.Front;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (mCardState == CardState.Back)
        {
            mechanic.OpenCard(this);
        }
    }
}