using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgePan : MonoBehaviour
{
    public float speed, zoomScale, middleDragSpeed;
    public int screenWidth, screenHeight;
    public bool isCamMoving;

    public Vector2 lastMousePosition;

    private void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    private void Update()
    {
        MoveCam();
        ZoomCam();
    }

    void MoveCam()
    {
        Vector3 camPos = transform.position;
        Vector2 currentPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (currentPos.x > screenWidth - 30)
        {
            isCamMoving = true;
            camPos.x += speed;
        }
        else if (currentPos.x < 30)
        {
            isCamMoving = true;
            camPos.x -= speed;
        }

        if (currentPos.y > screenHeight - 30)
        {
            isCamMoving = true;
            camPos.z += speed;
        }
        else if (currentPos.y < 30)
        {
            isCamMoving = true;
            camPos.z -= speed;
        }
        else
        {
            isCamMoving = false;
        }
        if (Input.GetMouseButton(2))
        {
            camPos.x -= (currentPos.x - lastMousePosition.x) * middleDragSpeed;
            camPos.z -= (currentPos.y - lastMousePosition.y) * middleDragSpeed;
        }
        transform.position = Vector3.Lerp(transform.position, camPos, Time.deltaTime);
        lastMousePosition = currentPos;
    }
    void ZoomCam()
    {
        this.GetComponent<Camera>().orthographicSize -= Input.mouseScrollDelta.y * zoomScale;
    }

}
