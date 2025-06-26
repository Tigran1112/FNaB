using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Render : MonoBehaviour
{
    [Header("UI Elements")]
    public Scrollbar loading;
    public TMP_Text text;

    [Header("Render Logic")]
    public bool IsRendering = false;
    [HideInInspector]public float percent = 0f;
    public int boost = 1;

    void Start()
    {
        loading.interactable = false;
        UpdateUI();
    }

    void OnMouseDown()
    {
        if (percent < 100f)
            IsRendering = true;
    }

    void OnMouseUp()
    {
        IsRendering = false;
    }


    void Update()
    {
        if (IsRendering && percent < 100f)
        {
            percent += Time.deltaTime * boost;
            percent = Mathf.Clamp(percent, 0f, 100f);
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        loading.size = percent / 100f;
        text.text = percent < 100f ? Mathf.RoundToInt(percent) + "%" : "Render Complete";

        if (loading.handleRect != null)
        {
            var img = loading.handleRect.GetComponent<Image>();
            img.color = (percent >= 100f) ? Color.green : Color.cyan;
        }
    }
}
