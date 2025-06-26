using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public Render render;

    public float gameSpeed = 60f;

    [Tooltip("Добавить часы к таймеру для отладки (сбросится после применения)")]
    public int debugAddHours = 0;

    private float elapsed = 0f;
    private bool triggered = false;

    public Animator Jumpscare;

    void Update()
    {
        if (debugAddHours > 0)
        {
            elapsed += debugAddHours * 60f;
            debugAddHours = 0;
            UpdateTimerUI();
        }

        elapsed += Time.deltaTime * gameSpeed;
        UpdateTimerUI();

        if (!triggered && GetHours() >= 6)
        {
            triggered = true;
            int renProgress = Mathf.CeilToInt(render.percent);
            if (renProgress < 100)
                JumpScare();
            else
                SceneManager.LoadScene("6 AM");
        }
    }

    private void UpdateTimerUI()
    {
        int gameMinutes = Mathf.FloorToInt(elapsed % 60f);
        int gameHours = Mathf.FloorToInt(elapsed / 60f);
        timerText.text = $"{gameHours:D2}:{gameMinutes:D2}";
    }

    private int GetHours()
    {
        return Mathf.FloorToInt(elapsed / 60f);
    }

    void JumpScare()
    {
        if (!Jumpscare.GetCurrentAnimatorStateInfo(0).IsName("Over"))
        {
            Jumpscare.SetTrigger("Over");
            Invoke("End", 0.5f);
        }
    }



    void End()
    {
        SceneManager.LoadScene("Game Over");
    }
}
