using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 140f; //Скорость перемещения
    private Vector2 Direction; //Направление перемещения
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>(); //Получение компонента rigidbody
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }
    private void FixedUpdate()
    {
        MovePlayer(Direction);
    }
    private void MovePlayer (Vector2 direction)
    {
        rigidBody.velocity = transform.TransformDirection(direction * speed * Time.fixedDeltaTime); //Перемещение игрока
    }
}
