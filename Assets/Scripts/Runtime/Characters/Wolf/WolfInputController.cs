using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum WolfGoal
{
    Idle,
    Wait,
    FollowHero,
    AttackHeroTarget,
    AttackOwnTarget,
}

[Serializable]
public class WolfInput
{
    public Vector2 Move = Vector2.zero;
    public int LastMoveDirection = 1;
    public bool Jump = false;
    public bool Attack = false;
}

public class WolfInputController : MonoBehaviour
{
    public WolfInput WolfInput { get; protected set; } = new WolfInput();
    public WolfGoal CurrentGoal { get; protected set; } = WolfGoal.Idle;

    [field: Header("Debugging")]
    [field: SerializeField] public TMPro.TMP_Text GoalText { get; private set; } = null;

    private void Update()
    {
        DoGoalChecks();

        if (GoalText != null)
            GoalText.text = CurrentGoal.ToString();
    }

    private void DoGoalChecks()
    {
        switch (CurrentGoal)
        {
            case WolfGoal.Idle:
                CheckIdleGoal();
                break;
            case WolfGoal.Wait:
                CheckWaitGoal();
                break;
            case WolfGoal.FollowHero:
                CheckFollowHeroGoal();
                break;
            case WolfGoal.AttackHeroTarget:
                CheckAttackHeroTargetGoal();
                break;
            case WolfGoal.AttackOwnTarget:
                CheckAttackOwnTargetGoal();
                break;
            default:
                break;
        }
    }

    // Possible Controlls:
    // Shoulder Bumper: Switch wolfs target
    // Hold Shoulder Bumper: Switch wolfs target to hero target
    // Note -> Give this ability later in the game so it feels like the wolf is learning
    // Left Shoulder Trigger: Switch to wolf comands
    // Comands are then the north south east west buttons as long as the trigger is held
    // Right Shoulder Trigger: Could be used for interactions?

    private void CheckIdleGoal()
    {
        // If the hero is moving and the wolf is in the follow radius, follow the hero

        // If the hero is moving and the wolf is not in the follow radius, teleport to the hero

        // If the hero is not moving and the wolf is in the follow radius, stay idle

        // If the hero is attacking and the wolf is in the follow radius, attack the hero's target

        // If the hero is attacked and the wolf is in the follow radius, attack the hero's attacker

        // If the hero is attacked and the wolf is not in the follow radius, teleport to the hero's attacker
    }

    private void CheckWaitGoal()
    {
        // If the hero calls the wolf, and the wolf is in the follow radius, follow the hero

        // If the hero calls the wolf, and the wolf is not in the follow radius, teleport to the hero
    }

    private void CheckFollowHeroGoal()
    {
        // If the hero is moving and the wolf is in the follow radius, follow the hero

        // If the hero is moving and the wolf is not in the follow radius, teleport to the hero

        // If the hero is not moving and the wolf is in the follow radius, go idle
    }

    private void CheckAttackHeroTargetGoal()
    {
        // If the hero is attacking and the wolf is in the follow radius, attack the hero's target

        // If the target is dead, go idle
    }

    private void CheckAttackOwnTargetGoal()
    {
        // If the target is dead, go idle

        // If the hero is outside the follow radius, teleport to the hero
    }
}
