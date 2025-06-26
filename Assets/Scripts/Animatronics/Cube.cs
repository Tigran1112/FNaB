using System.Collections;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cube : MonoBehaviour
{
    public int AI;
    private float MinSpeed;
    private float MaxSpeed;
    private int Chance;
    public GameObject[] Poses;
    private int Pos;
    public GameObject IsOpen;

    private float TimeToFail;
    private float TimeToRun;
    private bool isAttacking;

    public Animator Jumpscare;

    public GameObject[] transition;

    private void Awake()
    {
        if (AI == 0) return;
        float t  = (AI - 1f) / 19f;
        float s  = Mathf.SmoothStep(0f, 1f, t);
        MinSpeed = Mathf.Lerp(15f, 1f, s);
        MaxSpeed = Mathf.Lerp(25f, 7f, s);
        Chance   = Mathf.RoundToInt(Mathf.Lerp(30f, 100f, s));
        Pos      = Poses.Length -1;

        Invoke("Walk", Random.Range(MinSpeed, MaxSpeed));
        Debug.Log("Куб просыпается...");
    }

    void Walk()
    {
        if (Random.Range(0, 101) <= Chance && Pos != 0)
        {
            StartCoroutine(Transition());
            Poses[Pos].SetActive(false);
            Pos--;
            Poses[Pos].SetActive(true);
        }

        if (Pos == 0)
        {
            Attack();
        }
        else
        {
            Invoke("Walk", Random.Range(MinSpeed, MaxSpeed));
        }
        Debug.Log("Куб пошел... или не пошел");
    }

    IEnumerator Transition()
    {
        var img = transition[Pos].GetComponent<CanvasGroup>();
        if (img == null)
            yield break;

        img.alpha = 1f;
        transition[Pos].SetActive(true);

        float duration = 0.2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            img.alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            yield return null;
        }
        transition[Pos].SetActive(false);
    }


    void Attack()
    {
        isAttacking = true;
        Debug.Log("Куб рядом");
    }

    void Update()
    {
        if (!isAttacking) return;

        if (IsOpen.activeSelf)
        {
            TimeToFail += Time.deltaTime;
            if (TimeToFail >= 5f)
            {
                JumpScare();
            }
        }
        else
        {
            TimeToRun += Time.deltaTime;
            if (TimeToRun >= 2f)
            {
                isAttacking = false;
                TimeToFail = 0;
                TimeToRun = 0;

                Poses[Pos].SetActive(false);
                Pos = Poses.Length - 1;
                Poses[Pos].SetActive(true);

                Invoke("Walk", Random.Range(MinSpeed, MaxSpeed));
            }
        }
    }


    void JumpScare()
    {
        if (!Jumpscare.GetCurrentAnimatorStateInfo(0).IsName("Over"))
        {
            isAttacking = false;
            Poses[0].SetActive(false);
            Jumpscare.SetTrigger("Over");
            Invoke("End", 0.5f);
        }
    }



    void End()
    {
        transition[0].SetActive(true);
        SceneManager.LoadScene("Game Over");
    }
}
