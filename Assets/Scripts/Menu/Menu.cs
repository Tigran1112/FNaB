using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public bool GameOver;
    public GameObject transition;
    public void Play()
    {
        SceneManager.LoadScene("Night 1");
    }
    public void Exit()
    {
        Application.Quit();
    }
    private void Awake()
    {
        if (GameOver)
        {
            transition.SetActive(false);
            Invoke("ToMenu", 5f);
        }
    }
    void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}