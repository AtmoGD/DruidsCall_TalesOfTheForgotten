using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [field: SerializeField] public Animator TeleportAnimator { get; private set; } = null;
    [field: SerializeField] public Animator DieAnimator { get; private set; } = null;

    public void PlayTeleportAnimation()
    {
        TeleportAnimator.SetTrigger("Teleport");
    }

    public void PlayDieAnimation(bool _isDead)
    {
        DieAnimator.SetBool("IsDead", _isDead);
    }
}
