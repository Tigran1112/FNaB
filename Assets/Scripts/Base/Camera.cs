using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 150f;
    private float yaw = 90f;
    float rotateZone;

    private void Start()
    {
        rotateZone = Screen.width / 5f;
    }

    private void Update()
    {
        float mouseX = Input.mousePosition.x;

        if (mouseX < rotateZone && yaw > 20f)
        {
            yaw -= sensitivity * Time.deltaTime;
        }
        else if (mouseX > Screen.width - rotateZone && yaw < 160f)
        {
            yaw += sensitivity * Time.deltaTime;
        }

        transform.eulerAngles = new Vector3(0f, yaw, 0f);
    }
}
