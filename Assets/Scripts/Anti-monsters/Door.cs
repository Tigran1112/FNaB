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
        Debug.Log("Закрытие");
        anim.SetTrigger("Close");
        IsOpen.SetActive(false);
    }
    private void OnMouseUp()
    {
        Debug.Log("Открытие");
        anim.SetTrigger("Open");
        IsOpen.SetActive(true);
    }
}