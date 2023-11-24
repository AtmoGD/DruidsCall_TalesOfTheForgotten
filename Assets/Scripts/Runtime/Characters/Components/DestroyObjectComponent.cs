using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectComponent : MonoBehaviour
{
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
