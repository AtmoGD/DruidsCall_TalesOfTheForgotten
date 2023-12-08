using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class TooltipComponent : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public Image Image { get; private set; } = null;
    [field: SerializeField] public TMP_Text Text { get; private set; } = null;

    [field: Header("Settings")]
    [field: SerializeField] public Sprite Sprite { get; private set; } = null;
    [field: SerializeField] public string TooltipText { get; private set; } = "";

    private Animator animator = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowTooltip(bool _show)
    {
        animator.SetBool("IsShown", _show);
        Image.sprite = Sprite;
        Text.text = TooltipText;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Niamh niamh = other.gameObject.GetComponent<Niamh>();
        if (niamh)
            ShowTooltip(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Niamh niamh = other.gameObject.GetComponent<Niamh>();
        if (niamh)
            ShowTooltip(false);
    }
}
