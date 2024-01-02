using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiamhAnimationHelper : MonoBehaviour
{
    [field: SerializeField] public Niamh Niamh { get; private set; } = null;

    private void Start()
    {
        Niamh = GetComponentInParent<Niamh>();
    }

    public void PlayFootstepFeedbacks()
    {
        Niamh.FootstepFeedbacks?.PlayFeedbacks();
    }
}
