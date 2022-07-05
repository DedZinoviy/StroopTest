using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public GameObject player;
    private SwipePanelController SwipePanel;

    private void Start()
    {
        SwipePanel = GameObject.Find("SwipePanel").transform.Find("SwipePanel").gameObject.GetComponent<SwipePanelController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
            TriggerAction();
    }

    private void TriggerAction()
    {
        SwipePanel.InstantiateSwipePanel(); //Показать панель со свайпом
        gameObject.SetActive(false); //Отключить ловушку
    }
}
