using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public enum WolfGoal
{
    Idle,
    Wait,
    FollowHero,
    TeleportToHero,
    AttackHeroTarget,
    AttackOwnTarget,
    FollowTracks,
    Distract,
}

[Serializable]
public class WolfInput
{
    private Vector2 move = Vector2.zero;
    public Vector2 Move
    {
        get { if (OverrideMove) return OverrideMoveValue; return move; }
        set { move = value; }
    }
    public bool OverrideMove = false;
    public Vector2 OverrideMoveValue = Vector2.zero;

    private int lastMoveDirection = 1;
    public int LastMoveDirection
    {
        get { if (OverrideLastMoveDirection) return OverrideLastMoveDirectionValue; return lastMoveDirection; }
        set { lastMoveDirection = value; }
    }
    public bool OverrideLastMoveDirection = false;
    public int OverrideLastMoveDirectionValue = 1;

    private bool jump = false;
    public bool Jump
    {
        get { if (OverrideJump) return OverrideJumpValue; return jump; }
        set { jump = value; }
    }
    public bool OverrideJump = false;
    public bool OverrideJumpValue = false;

    public bool HeroJumped = false;
    public bool Attack = false;
    public bool TeleportToHero = false;
}

public class WolfInputController : MonoBehaviour
{
    [field: SerializeField] public Player Player { get; private set; } = null;
    public Wolf Wolf => Player.Wolf;
    public Hero Hero => Player.Hero;

    public WolfInput WolfInput { get; protected set; } = new WolfInput();
    public WolfGoal CurrentGoal { get; protected set; } = WolfGoal.Idle;

    [field: Header("Debugging")]
    [field: SerializeField] public TMPro.TMP_Text GoalText { get; private set; } = null;

    private void Start()
    {
        if (Hero)
            Hero.onJump.AddListener(OnHeroJump);
    }

    private void Update()
    {
        if (Wolf.IsControlledByPlayer) return;

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
            case WolfGoal.TeleportToHero:
                CheckTeleportToHeroGoal();
                break;
            case WolfGoal.AttackHeroTarget:
                CheckAttackHeroTargetGoal();
                break;
            case WolfGoal.AttackOwnTarget:
                CheckAttackOwnTargetGoal();
                break;
            case WolfGoal.FollowTracks:
                CheckFollowTracksGoal();
                break;
            case WolfGoal.Distract:
                CheckDistractGoal();
                break;
            default:
                break;
        }

        switch (CurrentGoal)
        {
            case WolfGoal.Idle:
                WolfIdle();
                break;
            case WolfGoal.Wait:
                WolfWait();
                break;
            case WolfGoal.FollowHero:
                FollowHero();
                break;
            case WolfGoal.TeleportToHero:
                TeleportToHero();
                break;
            case WolfGoal.AttackHeroTarget:
                WolfAttackHeroTarget();
                break;
            case WolfGoal.AttackOwnTarget:
                WolfAttackOwnTarget();
                break;
            case WolfGoal.FollowTracks:
                WolfFollowTracks();
                break;
            case WolfGoal.Distract:
                WolfDistract();
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
        if (Wolf.InFollowRadius) CurrentGoal = WolfGoal.FollowHero;
        else if (Wolf.InTeleportRadius && Wolf.Hero.Grounded()) CurrentGoal = WolfGoal.TeleportToHero;
        // If the hero is moving and the wolf is not in the follow radius, teleport to the hero

        // If the hero is not moving and the wolf is in the follow radius, stay idle

        // If the hero is attacking and the wolf is in the follow radius, attack the hero's target

        // If the hero is attacked and the wolf is in the follow radius, attack the hero's attacker

        // If the hero is attacked and the wolf is not in the follow radius, teleport to the hero's attacker
    }

    private void WolfIdle()
    {
        WolfInput.Move = Vector2.zero;
    }

    private void CheckWaitGoal()
    {
        // If the hero calls the wolf, and the wolf is in the follow radius, follow the hero

        // If the hero calls the wolf, and the wolf is not in the follow radius, teleport to the hero
    }

    private void WolfWait()
    {

    }

    private void CheckFollowHeroGoal()
    {
        // If the hero is moving and the wolf is in the follow radius, follow the hero

        // If the hero is moving and the wolf is not in the follow radius, teleport to the hero

        // If the hero is not moving and the wolf is in the follow radius, go idle

        if (!Wolf.InFollowRadius && !Wolf.InTeleportRadius)
        {
            if (Wolf.Hero.CurrentInput.Move.x == 0)
                CurrentGoal = WolfGoal.Idle;
        }
        else if (Wolf.InTeleportRadius && Wolf.Hero.Grounded()) CurrentGoal = WolfGoal.TeleportToHero;
    }

    private void FollowHero()
    {
        int direction = 0;

        // if (Wolf.IsGroundedInMovementDirection)
        direction = Wolf.FollowTransform.transform.position.x > Wolf.transform.position.x ? 1 : -1;

        WolfInput.LastMoveDirection = direction;
        WolfInput.Move = new Vector2(direction, 0);
    }

    private void CheckTeleportToHeroGoal()
    {
        if (!Wolf.InFollowRadius && !Wolf.InTeleportRadius) CurrentGoal = WolfGoal.Idle;
    }

    private void TeleportToHero()
    {
        if (Wolf.Hero.Grounded())
            WolfInput.TeleportToHero = true;
    }

    private void CheckAttackHeroTargetGoal()
    {
        // If the hero is attacking and the wolf is in the follow radius, attack the hero's target

        // If the target is dead, go idle
    }

    private void WolfAttackHeroTarget()
    {

    }

    private void CheckAttackOwnTargetGoal()
    {
        // If the target is dead, go idle

        // If the hero is outside the follow radius, teleport to the hero
    }

    private void WolfAttackOwnTarget()
    {

    }

    private void CheckFollowTracksGoal()
    {

    }

    private void WolfFollowTracks()
    {

    }

    private void CheckDistractGoal()
    {

    }

    private void WolfDistract()
    {

    }

    private void OnHeroJump()
    {
        WolfInput.HeroJumped = true;
    }
}
