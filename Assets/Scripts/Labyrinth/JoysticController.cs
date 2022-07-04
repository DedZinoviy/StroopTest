using UnityEngine;
using UnityEngine.EventSystems;

public class JoysticController : MonoBehaviour, IPointerDownHandler
{

    private bool isTouch = false; //����, ������������ ���� �� �������
    private Vector2 firstTouchPosition; //�������������� ������� �������
    private Vector2 secondTouchPosition; //������� �����������
    [SerializeField] PlayerController playerController; //���������� �������
    [SerializeField] Transform joysticMarker; //���� ���������
    private bool isControlPanel = false; //����, ������������, ����������� �� ������� �� ������ ����������

    private void Update()
    {
        JoysticTouchPoints(); //���������� ����� �������������
    }

    private void JoysticTouchPoints()
    {
        if (Input.GetMouseButtonDown(0))
            firstTouchPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y); //���������� ��������� ����� �������
        if (Input.GetMouseButton(0))
        {
            isTouch = true;
            secondTouchPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y); //���������� ����� �����������
        }
        else
        {
            isTouch = false;
            isControlPanel = false;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer(); //����������� ������
    }

    private void MovePlayer()
    {
        if (isTouch && isControlPanel)
        {
            Vector2 deltaPosition = secondTouchPosition - firstTouchPosition; //����� ��������
            Vector2 direction = Vector2.ClampMagnitude(deltaPosition, 1.0f); //����� ����������� ����������� ������
            playerController.SetDirection(direction); //����������� ������

            joysticMarker.transform.position = new Vector2(transform.position.x + direction.x * 0.5f, transform.position.y + direction.y * 0.5f); //����������� ���� ���������
        }
        else
        {
            playerController.SetDirection(new Vector2(0, 0)); //���������� ����������� ������
            joysticMarker.transform.position = new Vector2(transform.position.x, transform.position.y); //������� ����
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        string controlPanelName = "Control Panel"; //�������� ������ ����������
        string joysticName = "Joystic"; //�������� ���������
        string joysticMarkerName = "Joystic marker"; //�������� �����
        string curObjName = eventData.pointerCurrentRaycast.gameObject.name; //��� �������, �� ������� ������
        if (curObjName == controlPanelName || curObjName == joysticName || curObjName == joysticMarkerName)
            isControlPanel = true;
        else
            isControlPanel = false;
    }
}
