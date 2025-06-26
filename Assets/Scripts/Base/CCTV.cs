using System.Collections;
using UnityEngine;

public class CCTV : MonoBehaviour
{
    public GameObject[] cameras;
    public GameObject mainCamera;
    public GameObject cameraUI;
    public int CameraIndex;

    private Camera mainCamComponent;

    public GameObject transition;
    void Start()
    {
        mainCamComponent = mainCamera.GetComponent<Camera>();

        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].SetActive(false);
        }

        mainCamera.SetActive(true);
        mainCamComponent.enabled = true;
        cameraUI.SetActive(false);
        transition.SetActive(false);
    }

    public void ChangeState()
    {
        if (mainCamComponent.enabled)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    void Open()
    {
        mainCamComponent.enabled = false;
        cameraUI.SetActive(true);
        cameras[CameraIndex].SetActive(true);
    }

    void Close()
    {
        mainCamComponent.enabled = true;
        cameraUI.SetActive(false);
        cameras[CameraIndex].SetActive(false);
    }

    public void ChangeCamera(int index)
    {
        transition.SetActive(true);
        cameras[CameraIndex].SetActive(false);
        CameraIndex = index;
        cameras[CameraIndex].SetActive(true);
        Invoke("Transition", 0.2f);
    }
    void Transition()
    {
        transition.SetActive(false);
    }
}
