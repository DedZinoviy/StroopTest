using UnityEngine;
using UnityEngine.EventSystems;

public class JoysticController : MonoBehaviour, IPointerDownHandler
{

    private bool isTouch = false; //Флаг, показывающий есть ли нажатие
    private Vector2 firstTouchPosition; //Первоначальная позиция нажатия
    private Vector2 secondTouchPosition; //Позиция перемещения
    [SerializeField] PlayerController playerController; //Контроллер игроком
    [SerializeField] Transform joysticMarker; //Стик джойстика
    private bool isControlPanel = false; //Флаг, показывающий, произведено ли нажатие по панели управления

    private void Update()
    {
        JoysticTouchPoints(); //Установить точки приуосновений
    }

    private void JoysticTouchPoints()
    {
        if (Input.GetMouseButtonDown(0))
            firstTouchPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y); //Установить начальную точку касания
        if (Input.GetMouseButton(0))
        {
            isTouch = true;
            secondTouchPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y); //Установить точку перемещения
        }
        else
        {
            isTouch = false;
            isControlPanel = false;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer(); //Переместить игрока
    }

    private void MovePlayer()
    {
        if (isTouch && isControlPanel)
        {
            Vector2 deltaPosition = secondTouchPosition - firstTouchPosition; //Найти смещение
            Vector2 direction = Vector2.ClampMagnitude(deltaPosition, 1.0f); //Найти направление перемещения игрока
            playerController.SetDirection(direction); //Переместить игрока

            joysticMarker.transform.position = new Vector2(transform.position.x + direction.x * 0.5f, transform.position.y + direction.y * 0.5f); //Переместить стик джойстика
        }
        else
        {
            playerController.SetDirection(new Vector2(0, 0)); //Остановить перемещение игрока
            joysticMarker.transform.position = new Vector2(transform.position.x, transform.position.y); //Вернуть стик
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        string controlPanelName = "Control Panel"; //Название панели управления
        string joysticName = "Joystic"; //Название джойстика
        string joysticMarkerName = "Joystic marker"; //Название стика
        string curObjName = eventData.pointerCurrentRaycast.gameObject.name; //Имя объекта, на который нажали
        if (curObjName == controlPanelName || curObjName == joysticName || curObjName == joysticMarkerName)
            isControlPanel = true;
        else
            isControlPanel = false;
    }
}
