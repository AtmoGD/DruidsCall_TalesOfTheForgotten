using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineBlendManager : MonoBehaviour
{
    private static CinemachineBlendDefinition? nextBlend;

    static CinemachineBlendManager()
    {
        CinemachineCore.GetBlendOverride = GetBlendOverrideDelegate;
    }

    public static void ClearNextBlend()
    {
        nextBlend = null;
    }

    public static void SetNextBlend(CinemachineBlendDefinition blend)
    {
        nextBlend = blend;
    }

    public static CinemachineBlendDefinition GetBlendOverrideDelegate(ICinemachineCamera fromVcam, ICinemachineCamera toVcam, CinemachineBlendDefinition defaultBlend, MonoBehaviour owner)
    {
        if (nextBlend.HasValue)
        {
            var blend = nextBlend.Value;
            nextBlend = null;
            return blend;
        }
        return defaultBlend;
    }

    /*
    HIER NACHSCHAUEN WENN DER CHARACTER INNERHALB EINES lEVELS TELEPORTIERT WERDEN SOLL
    DA KEIN WECHSEL ZWISCHEN VIRTUAL CAMERAS ERFOLGT, GIBT ES KEINEN BLEND DEN MAN ÜBERSCHREIBEN KANN
    https://ericlathrop.com/2021/04/programmatically-changing-cinemachine-blends/

    public void Warp(Vector3 destination)
    {
        CinemachineBlendManager.SetNextBlend(new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0));

        transform.position = destination;

        foreach (var vcam in allVirtualCameras)
        {
            vcam.PreviousStateIsValid = false;
        }
        StartCoroutine(ResetBlendNextFrame());
    }

    private IEnumerator ResetBlendNextFrame()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForFixedUpdate();
        CinemachineBlendManager.ClearNextBlend();
    }
    */
}
