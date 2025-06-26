using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CEO : MonoBehaviour
{
    public int AI;
    public GameObject transition;
    public GameObject transUI;
    public GameObject Screen;
    public Animator Jumpscare;

    private float MinSpeed;
    private float MaxSpeed;
    private int Chance;

    private float TimeToFail;
    private bool isAttacking;

    private void Awake()
    {
        if (AI == 0) return;

        float t  = (AI - 1f) / 19f;
        float s  = Mathf.SmoothStep(0f, 1f, t);
        MinSpeed = Mathf.Lerp(15f, 1f, s);
        MaxSpeed = Mathf.Lerp(25f, 7f, s);
        Chance   = Mathf.RoundToInt(Mathf.Lerp(30f, 100f, s));

        ScheduleNextAttack();
        Debug.Log("Начальник просыпается...");
    }

    void ScheduleNextAttack()
    {
        Invoke(nameof(Walk), Random.Range(MinSpeed, MaxSpeed));
    }

    void Walk()
    {
        if (Random.Range(0, 101) <= Chance)
        {
            StartCoroutine(Transition());
            Screen.SetActive(true);
            isAttacking = true;
        }
        else
        {
            ScheduleNextAttack();
        }

        Debug.Log("Начальник туки туки");
    }

    IEnumerator Transition()
    {
        transition.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        transition.SetActive(false);
    }

    private void Update()
    {
        if (!isAttacking) return;

        TimeToFail += Time.deltaTime;

        if (TimeToFail >= 8f)
        {
            JumpScare();
        }
        else if (Input.GetMouseButton(0))
        {
            isAttacking = false;
            TimeToFail  = 0f;
            Screen.SetActive(false);
            ScheduleNextAttack();
        }
        Debug.Log($"isAttacking = {isAttacking}, TimeToFail = {TimeToFail}, MouseDown = {Input.GetMouseButton(0)}");
    }

    void JumpScare()
    {
        if (!Jumpscare.GetCurrentAnimatorStateInfo(0).IsName("Over"))
        {
            isAttacking = false;
            Screen.SetActive(false);
            Jumpscare.SetTrigger("Over");
            Invoke(nameof(End), 0.5f);
        }
    }

    void End()
    {
        transUI.SetActive(true);
        SceneManager.LoadScene("Game Over");
    }
}
