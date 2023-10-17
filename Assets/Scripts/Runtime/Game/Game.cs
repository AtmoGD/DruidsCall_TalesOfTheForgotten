using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Manager { get; private set; }
    public bool SetDontDestroyOnLoad { get; private set; } = true;

    [field: Header("Game References")]
    [field: SerializeField] public Player Player { get; private set; } = null;
    [field: SerializeField] public MainCharacter MainCharacter { get; private set; } = null;
    [field: SerializeField] public Wolf Wolf { get; private set; } = null;
    [field: SerializeField] public UIController UIController { get; private set; } = null;

    private void Awake()
    {
        if (Manager) { Destroy(gameObject); return; }

        Manager = this;

        if (SetDontDestroyOnLoad) DontDestroyOnLoad(gameObject);
    }
}
