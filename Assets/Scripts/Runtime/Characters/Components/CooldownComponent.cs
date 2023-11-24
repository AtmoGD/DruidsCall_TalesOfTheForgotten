using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using System;

[Serializable]
public class Cooldown
{
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public float Duration { get; set; }
    [field: SerializeField] public float TimeRemaining { get; set; }
    [SerializeField] public bool IsReady { get { return TimeRemaining <= 0; } }

    public Cooldown(string _name, float _duration)
    {
        Name = _name;
        Duration = _duration;
        TimeRemaining = Duration;
    }
}

public class CooldownComponent : MonoBehaviour
{
    [field: SerializeField] public List<Cooldown> Cooldowns { get; set; } = new List<Cooldown>();

    public void AddCooldown(Cooldown _cooldown)
    {
        Cooldowns.Add(_cooldown);
    }

    public void RemoveCooldown(string _name)
    {
        Cooldown cooldown = Cooldowns.Find(x => x.Name == _name);
        if (cooldown != null)
            Cooldowns.Remove(cooldown);
    }

    public bool HasCooldown(string _name)
    {
        return Cooldowns.Find(x => x.Name == _name) != null;
    }

    public void Update()
    {
        Cooldowns.ForEach(x => x.TimeRemaining -= Time.deltaTime);
        Cooldowns.RemoveAll(x => x.TimeRemaining <= 0);
    }
}
