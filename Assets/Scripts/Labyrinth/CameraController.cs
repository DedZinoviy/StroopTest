using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float LeftBound;
    private float RightBound;
    private float UpperBound;
    private float BottomBound;

    public void SetCameraBounds(float leftBound, float rightBound, float upperBound, float bottomBound)
    {
        float vertExtent = gameObject.GetComponent<Camera>().orthographicSize - 0.05f; //¬ертикальные границы камеры
        float horExtent = gameObject.GetComponent<Camera>().aspect * vertExtent - 0.05f; //√оризонтальные границы камеры
        
        if (Mathf.Abs(leftBound - rightBound) < 2 * horExtent)
        { // амера не помещаетс€ в вертикальные границы
            horExtent = Mathf.Abs(leftBound - rightBound) / 2;
        }
        if (Mathf.Abs(upperBound - bottomBound) < 2 * vertExtent)
        {// амера не помещаетс€ в горизонтальные границы
            vertExtent = Mathf.Abs(upperBound - bottomBound) / 2;
        }

        LeftBound = leftBound + horExtent;
        RightBound = rightBound - horExtent;
        UpperBound = upperBound - vertExtent;
        BottomBound = bottomBound + vertExtent;
    }
    private void Update()
    {
        CameraClamp();
    }

    private void FixedUpdate()
    {
        if (transform.localPosition != new Vector3(0, 0, transform.localPosition.z))
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, transform.localPosition.z), Time.fixedDeltaTime); ;
    }


    private void CameraClamp()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, LeftBound, RightBound), Mathf.Clamp(transform.position.y, BottomBound, UpperBound),
            transform.position.z);
    }

}