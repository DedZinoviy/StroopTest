using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwipePanelController : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private Sprite[] Arrows = new Sprite[4]; //�������
    ArrowDirection arrowDirection; //����������� �������
    private bool isShown = false;
    private enum ArrowDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    public void InstantiateSwipePanel()
    {
        if (!isShown)
        {
            gameObject.SetActive(true); //������� ������ ��������
            Image image = transform.Find("Arrow").GetComponent<Image>(); //�����������
            arrowDirection = (ArrowDirection)Random.Range(0, 4); //�����������
            image.sprite = SelectPicture(arrowDirection); //�������� �����������
            isShown = true;
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
        ArrowDirection swipeDirection; //����������� ������
        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        { //�������������� ��������
            if (eventData.delta.x > 0)
                swipeDirection = ArrowDirection.Right; //������
            else
                swipeDirection = ArrowDirection.Left; //�����
        }
        else
        { //������������ ��������
            if (eventData.delta.y > 0)
                swipeDirection = ArrowDirection.Up; //�����
            else
                swipeDirection = ArrowDirection.Down; //����
        }

        SwipeConfirm(swipeDirection); //��������� ������������ ������
    }

    private void SwipeConfirm(ArrowDirection direction)
    {
        if (direction == arrowDirection)
            SuccessConfirm(); //�������� � ������ ����������� ������
        else
            FailConfirm(); //�������� � ������ ������������� ������
        HideSwipePanel(); //������ ������
    }
    private void FailConfirm()
    {
        Debug.Log("Fail");
    }

    private void SuccessConfirm()
    {
        Debug.Log("Success");
    }

    private void HideSwipePanel()
    {
        gameObject.SetActive(false); //��������� ������
        isShown = false;
    }
}