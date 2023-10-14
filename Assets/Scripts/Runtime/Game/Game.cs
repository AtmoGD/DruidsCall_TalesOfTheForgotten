using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Manager { get; private set; }

    public bool SetDontDestroyOnLoad { get; private set; } = true;

    private void Awake()
    {
        if (Manager) { Destroy(gameObject); return; }

        Manager = this;

        if (SetDontDestroyOnLoad) DontDestroyOnLoad(gameObject);
    }
}
