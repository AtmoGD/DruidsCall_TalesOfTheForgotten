using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPositionTeleportScreen : MonoBehaviour
{
    [field: SerializeField] public RectTransform RectTransform { get; private set; } = null;

    public void AutoPosition()
    {
        Vector2 pos = Camera.main.WorldToScreenPoint(Game.Manager.Niamh.gameObject.transform.position);
        pos = new Vector2(pos.x - Screen.width / 2, pos.y - Screen.height / 2);

        RectTransform.anchoredPosition = pos;
    }
}
