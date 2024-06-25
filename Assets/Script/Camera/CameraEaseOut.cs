using UnityEngine;

public class CameraEaseOut : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public float duration = 1.0f;

    private Camera currentCamera;
    private Camera targetCamera;
    private float timeElapsed;
    private bool isTransitioning;

    void Start()
    {
        // Set initial camera
        currentCamera = camera1;
        targetCamera = camera2;
        camera1.enabled = true;
        camera2.enabled = false;
    }

    void Update()
    {

        if (isTransitioning)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;
            t = t * t * (3f - 2f * t);

            currentCamera.transform.position = Vector3.Lerp(currentCamera.transform.position, targetCamera.transform.position, t);
            currentCamera.transform.rotation = Quaternion.Slerp(currentCamera.transform.rotation, targetCamera.transform.rotation, t);

            if (timeElapsed >= duration)
            {
                EndTransition();
            }
        }
    }

    public void StartTransition()
    {
        isTransitioning = true;
        timeElapsed = 0;

        Camera temp = currentCamera;
        currentCamera = targetCamera;
        targetCamera = temp;

        currentCamera.enabled = true;
        targetCamera.enabled = true;
    }

    public void EndTransition()
    {
        isTransitioning = false;
        timeElapsed = 0;
        targetCamera.enabled = false;
    }
    public void SwitchCamera()
    {
        StartTransition();
    }
}
