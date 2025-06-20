using UnityEngine;

public class CCTV : MonoBehaviour
{
    public GameObject[] cameras;
    public GameObject mainCamera;
    public GameObject cameraUI;
    public int CameraIndex;
    
    void ChangeState()
    {
        if (mainCamera.activeSelf)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
    void Open()
    {
        mainCamera.SetActive(false);
        cameraUI.SetActive(true);
        cameras[CameraIndex].SetActive(true);
    }
    void Close()
    {
        mainCamera.SetActive(true);
        cameraUI.SetActive(false);
        cameras[CameraIndex].SetActive(false);
    }
    void ChangeCamera(int index)
    {
        cameras[CameraIndex].SetActive(false);
        CameraIndex = index;
        cameras[CameraIndex].SetActive(true);
    }
}
