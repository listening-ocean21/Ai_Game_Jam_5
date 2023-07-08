using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : CharacterState
{
    float _dashTimer;
    [SerializeField] private float _dashDuration = 0.5f;
    [SerializeField] private AnimationCurve _animationCurve;

    [SerializeField] private bool _continueDash = false;
    [SerializeField] private int _maxDashTimes = 2;
    
    int _dashTimes = 0;

    #region Unity Life Cycle
    public override void Awake()
    {
        base.Awake();

    }

    void Start()
    {
        PlayerController.OnFly += () => _dashTimes = 0;  
    }

    #endregion

    #region Check Transition
    public override bool CheckEnterTransition(CharacterState fromState)
    {
        bool flag = PlayerController.Grounded;
        if (!flag)
        {
            flag = _dashTimes < _maxDashTimes;
        }
        return (_continueDash || PlayerController.CanDash) && flag;
    }

    public override void CheckExitTransition()
    {
        if (_dashTimer > _dashDuration )
        {
            StateController.EnqueueTransition<NormalState>();
            return;
        }
    }
    #endregion


    public override void PreUpdateBehaviour(float dt)
    {
        _dashTimer += dt;
    }

    float dir = 1;

    public override void UpdateBehaviour(float dt)
    {
        bool closeWall = PlayerController.ColLeft && dir == -1
            || PlayerController.ColRight && dir == 1;
        if (closeWall) { dir = 0; }
        PlayerController.CurrentHorizontalSpeed = dir * 30
            * _animationCurve.Evaluate(_dashTimer/ _dashDuration);

    }

    #region Enter Exit
    public override void EnterBehaviour(float dt, CharacterState fromState)
    {
        _dashTimer = 0;
        _dashTimes++;
        dir = PlayerController.FacingRight;
        PlayerController.IgnoreGravity = true;
        PlayerController.CurrentVerticalSpeed = 0;
        PlayerController.CanDash = false;
    }
    public override void ExitBehaviour(float dt, CharacterState toState)
    {
        PlayerController.IgnoreGravity = false;
    }
    #endregion

}
