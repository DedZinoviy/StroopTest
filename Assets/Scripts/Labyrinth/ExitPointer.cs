using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExitPointer : MonoBehaviour
{
    private Transform playerTransform; //Игрок
    private Camera camera; //Камера игрока
    private Transform pointerTransform; //Указатель
    private Image image; //Изображение указателя
    private bool isPointerEnabled = true; //Флаг, показывающий, включено ли отображение указателя
    private float pointerScale; //Размер указателя
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; //Найти игрока
        camera = playerTransform.Find("Player Camera").gameObject.GetComponent<Camera>(); //Найти игровую камеру
        pointerTransform = playerTransform.Find("Pointer Canvas").Find("Pointer Icon"); //Найти указатель
        image = pointerTransform.Find("Image").GetComponent<Image>(); //Найти изображение указателя
        pointerScale = pointerTransform.localScale.x; //Найти размер указателя
    }
    private void LateUpdate()
    {
        MovePointer();
    }

    private void MovePointer()
    {
        Vector3 fromPlayerToExit = transform.position - playerTransform.position; //Вектор между игроком и выходом
        Ray ray = new Ray(playerTransform.position, fromPlayerToExit); //Луч между игроком и выходо

        float minDistance = MinRayDistance(ray, fromPlayerToExit.magnitude); //Найти минимальную дистанцию от игрока до плоскостей камеры       

        PostPointer(ray, minDistance, fromPlayerToExit); //Расположить указатель

        if (fromPlayerToExit.magnitude > minDistance)
            ShowPointer(); //Показать указатель
        else
            HidePointer(); //Скрыть указатель
    }

    private float MinRayDistance(Ray ray, float maxPossibleDistance)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera); //Плоскости камеры

        float minDistance = Mathf.Infinity; //Минимальная дистанция до плоскости камеры
        //Найти минимальную дистанцию
        for (int i = 0; i < 4; i++)
        {
            if (planes[i].Raycast(ray, out float distance))
            {
                minDistance = Mathf.Min(minDistance, distance);
            }
        }

        minDistance = Mathf.Clamp(minDistance, 0, maxPossibleDistance); //Ограничения для растояния

        return minDistance;
    }

    private void PostPointer(Ray ray, float distance, Vector3 direction)
    {
        Vector3 WorldPointerPosition = ray.GetPoint(distance); //Позиция для указателя
        pointerTransform.position = Vector3.Lerp(pointerTransform.position, WorldPointerPosition, Time.deltaTime); //Перемещение указателя

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Угол между игроком и выходом
        pointerTransform.rotation = Quaternion.Euler(0, 0, angle - 90); //Поворот указателя
    }

    private void ShowPointer()
    {
        if (!isPointerEnabled)
        {
            isPointerEnabled = true;

            StopAllCoroutines();
            StartCoroutine("ShowCoroutine");
        }
    }

    private void HidePointer()
    {
        if (isPointerEnabled)
        {
            isPointerEnabled = false;

            StopAllCoroutines();
            StartCoroutine("HideCoroutine");
        }
    }

    private IEnumerator ShowCoroutine()
    {
        image.enabled = true;
        //Увеличить указатель
        pointerTransform.localScale = Vector3.zero;
        for (float t=0; t<1f; t+=Time.deltaTime * 6f)
        {
            pointerTransform.localScale = new Vector3(pointerScale, pointerScale, pointerScale) * t;
            yield return null;
        }
        pointerTransform.localScale = new Vector3(pointerScale, pointerScale, pointerScale);
    }

    private IEnumerator HideCoroutine()
    {
        //Уменьшить указатель
        for (float t = 1f; t > 0; t -= Time.deltaTime * 6f)
        {
            pointerTransform.localScale = new Vector3(pointerScale, pointerScale, pointerScale) * t;
            yield return null;
        }
        pointerTransform.localScale = Vector3.zero;
        image.enabled = false;
    }
}
