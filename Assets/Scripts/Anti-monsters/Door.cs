using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject IsOpen;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        IsOpen.SetActive(true);
    }
    private void OnMouseDown()
    {
        Debug.Log("��������");
        anim.SetTrigger("Close");
        IsOpen.SetActive(false);
    }
    private void OnMouseUp()
    {
        Debug.Log("��������");
        anim.SetTrigger("Open");
        IsOpen.SetActive(true);
    }
}