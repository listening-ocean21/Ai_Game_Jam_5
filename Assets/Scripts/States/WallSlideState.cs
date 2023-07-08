using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideState : CharacterState
{
    #region Unity Life Cycle
    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        
    }

    #endregion

    public bool FacingRight = false;

    public override bool CheckEnterTransition(CharacterState fromState)
    {
        if (PlayerController.Grounded) { return false; }
        if (PlayerController.ColLeft && (PlayerController.Input.X < 0 || PlayerController.Velocity.x <= 0) )
        {
            FacingRight = false;
            return true;
        }
        else if (PlayerController.ColRight && (PlayerController.Input.X > 0 || PlayerController.Velocity.x >= 0))
        {
            FacingRight = true;
            return true;
        }
        return false;

    }

    [SerializeField] float OutWallTime = 0.1f;
    [SerializeField] float OutWallOffset = 2f;
    float countDown;
    bool tryOut = false;

    public override void CheckExitTransition()
    {
        if (PlayerController.Grounded || FacingRight && !PlayerController.ColRight
            || !FacingRight && !PlayerController.ColLeft)
        {
            StateController.EnqueueTransition<NormalState>();
        }

        if (countDown > OutWallTime)
        {
            StateController.EnqueueTransition<NormalState>();

        }

        if (PlayerController.Input.JumpDown)
        {
            StateController.EnqueueTransition<JumpState>();
        }
    }

    public override void EnterBehaviour(float dt, CharacterState fromState)
    {
        tryOut = false;
        PlayerController.IsWallSlide = true;
        countDown = 0;
        //PlayerController._currentHorizontalSpeed = 0;
        StateController.Animator.SetBool(PlayerAnimator.FacingRightKey, FacingRight);
    }

    public override void ExitBehaviour(float dt, CharacterState toState)
    {
        PlayerController.IsWallSlide = false;
        if (toState.GetType() == typeof(NormalState))
            PlayerController.CurrentHorizontalSpeed += FacingRight ? -OutWallOffset : OutWallOffset;
    }

    public override void PreUpdateBehaviour(float dt)
    {
        if (FacingRight && PlayerController.Input.X < 0 || !FacingRight && PlayerController.Input.X > 0)
            tryOut= true;
        if (tryOut)
        {
            countDown += dt;
        }
    }

    public override void UpdateBehaviour(float dt)
    {
        PlayerController.PerformWalk();
    }
}
