using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEditor.Experimental.GraphView.GraphView;

public class NormalState : CharacterState
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

    #region Check Transition
    public override void CheckExitTransition()
    {
        if (PlayerController.Input.JumpDown)
        {
            StateController.EnqueueTransition<JumpState>();
        }
        if (PlayerController.Input.Dash)
        {
            StateController.EnqueueTransition<DashState>();
        }
        StateController.EnqueueTransition<WallSlideState>();
    }
    #endregion



    public override void EnterBehaviour(float dt, CharacterState fromState)
    {


    }

    [SerializeField, Range(1f, 3f)] private float _maxIdleSpeed = 2;

    public override void UpdateBehaviour(float dt)
    {
        PlayerController.PerformWalk();
        // Speed up idle while running
        StateController.Animator.SetFloat(PlayerAnimator.IdleSpeedKey, Mathf.Lerp(1, _maxIdleSpeed, Mathf.Abs(PlayerController.Input.X)));

    }

    public override void PostUpdateBehaviour(float dt)
    {

    }
}
