using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExitPointer : MonoBehaviour
{
    private Transform playerTransform; //�����
    private Camera camera; //������ ������
    private Transform pointerTransform; //���������
    private Image image; //����������� ���������
    private bool isPointerEnabled = true; //����, ������������, �������� �� ����������� ���������
    private float pointerScale; //������ ���������
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; //����� ������
        camera = playerTransform.Find("Player Camera").gameObject.GetComponent<Camera>(); //����� ������� ������
        pointerTransform = playerTransform.Find("Pointer Canvas").Find("Pointer Icon"); //����� ���������
        image = pointerTransform.Find("Image").GetComponent<Image>(); //����� ����������� ���������
        pointerScale = pointerTransform.localScale.x; //����� ������ ���������
    }
    private void LateUpdate()
    {
        MovePointer();
    }

    private void MovePointer()
    {
        Vector3 fromPlayerToExit = transform.position - playerTransform.position; //������ ����� ������� � �������
        Ray ray = new Ray(playerTransform.position, fromPlayerToExit); //��� ����� ������� � ������

        float minDistance = MinRayDistance(ray, fromPlayerToExit.magnitude); //����� ����������� ��������� �� ������ �� ���������� ������       

        PostPointer(ray, minDistance, fromPlayerToExit); //����������� ���������

        if (fromPlayerToExit.magnitude > minDistance)
            ShowPointer(); //�������� ���������
        else
            HidePointer(); //������ ���������
    }

    private float MinRayDistance(Ray ray, float maxPossibleDistance)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera); //��������� ������

        float minDistance = Mathf.Infinity; //����������� ��������� �� ��������� ������
        //����� ����������� ���������
        for (int i = 0; i < 4; i++)
        {
            if (planes[i].Raycast(ray, out float distance))
            {
                minDistance = Mathf.Min(minDistance, distance);
            }
        }

        minDistance = Mathf.Clamp(minDistance, 0, maxPossibleDistance); //����������� ��� ���������

        return minDistance;
    }

    private void PostPointer(Ray ray, float distance, Vector3 direction)
    {
        Vector3 WorldPointerPosition = ray.GetPoint(distance); //������� ��� ���������
        pointerTransform.position = Vector3.Lerp(pointerTransform.position, WorldPointerPosition, Time.deltaTime); //����������� ���������

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //���� ����� ������� � �������
        pointerTransform.rotation = Quaternion.Euler(0, 0, angle - 90); //������� ���������
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
        //��������� ���������
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
        //��������� ���������
        for (float t = 1f; t > 0; t -= Time.deltaTime * 6f)
        {
            pointerTransform.localScale = new Vector3(pointerScale, pointerScale, pointerScale) * t;
            yield return null;
        }
        pointerTransform.localScale = Vector3.zero;
        image.enabled = false;
    }
}
