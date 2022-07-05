using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwipePanelController : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private Sprite[] Arrows = new Sprite[4]; //Стрелки
    ArrowDirection arrowDirection; //Направление стрелки
    private bool isShown = false;
    private Image timeBar; //Временная шкала
    [SerializeField] private float time = 1.0f;
    private float timeLeft;
    private enum ArrowDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    private void Start()
    {
        timeBar = transform.Find("Panel").Find("TimeBar").gameObject.GetComponent<Image>(); //Получить изображение временной шкалы
    }

    private void Update()
    {
        TimeBarChange();
    }
    public void InstantiateSwipePanel()
    {
        if (!isShown)
        {
            gameObject.SetActive(true); //Сделать панель активной
            Image image = transform.Find("Arrow").GetComponent<Image>(); //Изображение
            arrowDirection = (ArrowDirection)Random.Range(0, 4); //Направление
            image.sprite = SelectPicture(arrowDirection); //Изменить изображение
            isShown = true;
            ResetTime(); //Восстановить время
        }
    }

    private Sprite SelectPicture(ArrowDirection direction)
    {
        Sprite arrow = null;
        if (direction == ArrowDirection.Up)
            arrow = FindPicture("Up");
        if (direction == ArrowDirection.Down)
            arrow = FindPicture("Down");
        if (direction == ArrowDirection.Left)
            arrow = FindPicture("Left");
        if (direction == ArrowDirection.Right)
            arrow = FindPicture("Right");
        return arrow;
    }
    private Sprite FindPicture(string partName)
    {
        Sprite arrow = null;
        bool isFound = false;
        int i = 0;
        while (i < Arrows.Length && !isFound)
        {
            if (Arrows[i].name.Contains(partName))
            {
                isFound = true;
                arrow = Arrows[i];
            }
            else
                i++;
        }
        return arrow;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ArrowDirection swipeDirection; //Направление свайпа
        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        { //Горизонтальное движение
            if (eventData.delta.x > 0)
                swipeDirection = ArrowDirection.Right; //Вправо
            else
                swipeDirection = ArrowDirection.Left; //Влево
        }
        else
        { //Вертикальное движение
            if (eventData.delta.y > 0)
                swipeDirection = ArrowDirection.Up; //Вверх
            else
                swipeDirection = ArrowDirection.Down; //Вниз
        }

        SwipeConfirm(swipeDirection); //Проверить правильность свайпа
    }

    private void SwipeConfirm(ArrowDirection direction)
    {
        if (direction == arrowDirection)
            SuccessConfirm(); //Действие в случае правильного свайпа
        else
            FailConfirm(); //Действие в случае неправильного свайпа
    }
    private void FailConfirm()
    {
        Debug.Log("Fail");
        HideSwipePanel(); //Убрать панель
    }

    private void SuccessConfirm()
    {
        Debug.Log("Success");
        HideSwipePanel(); //Убрать панель
    }

    private void HideSwipePanel()
    {
        gameObject.SetActive(false); //Отключить панель
        isShown = false;
    }

    private void TimeBarChange()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timeBar.fillAmount = timeLeft / time;
        }
        else
            FailConfirm();
    }

    private void ResetTime()
    {
        timeLeft = time;
    }
}
