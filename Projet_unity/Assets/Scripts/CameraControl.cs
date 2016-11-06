using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public float speeduse = 0.5f;
    private float minimum, maximumY, maximumX;
    private float height;
    private float width;

    // Update is called once per frame
    void Start()
    {
        minimum = 0f;
        maximumY = 30f;
        maximumX = 53f;

        if (Camera.main != null)
        {
            Camera.main.orthographicSize = 30 * Screen.height / Screen.width * 0.5f;
            height = Camera.main.GetComponent<Camera>().orthographicSize;
            width = height * Screen.width / Screen.height;
        }
    }

    void Update()
    {
        if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            if(Camera.current != null)
            {
                float xAxisValue = Input.GetAxisRaw("Horizontal") * speeduse;
                float yAxisValue = Input.GetAxisRaw("Vertical") * speeduse;

                Camera.current.transform.position =
                    new Vector3(Mathf.Clamp(xAxisValue + Camera.current.transform.position.x, minimum + width, maximumX - width),
                    Mathf.Clamp(yAxisValue + Camera.current.transform.position.y, minimum + height, maximumY - height), -20.0f);
            }
        }
        else if(Application.platform == RuntimePlatform.Android)
        {
            if(Camera.current != null)
            {
                if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                    //transform.Translate(-touchDeltaPosition.x * speeduse, -touchDeltaPosition.y * speeduse, 0);

                    Camera.current.transform.position =
                        new Vector3(Mathf.Clamp(Camera.current.transform.position.x - touchDeltaPosition.x, minimum + width, maximumX - width),
                                    Mathf.Clamp(Camera.current.transform.position.y - touchDeltaPosition.y, minimum + height, maximumY - height), -20.0f);
                }
            }
        }
    }
}
