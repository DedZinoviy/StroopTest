using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
            TriggerAction();
    }

    private void TriggerAction()
    {
        Exit();
    }

    private void ShowPanel()
    {
        GameObject messagePanel = GameObject.Find("Message Panel Creator").transform.Find("Message Panel").gameObject;
        if (messagePanel != null)
        {
            messagePanel.SetActive(true);
        }
    }

    private void Exit()
    {
        SceneChanger changer = FindObjectOfType<SceneChanger>();
        changer.NextScene();
    }
}
