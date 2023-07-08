using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class JumpState : CharacterState
{
    float _jumpTimer;
    [SerializeField] private float _jumpDuration = 1;

    [SerializeField] private float _maxJumpTimes = 3;
    public int JumpTimes = 0;


    #region Unity Life Cycle
    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        PlayerController.OnLanded += OnLanded;
        PlayerController.OnFly += () => JumpTimes = 1;
    }
    #endregion

    void OnLanded()
    {
        JumpTimes = 0;
    }

    [Header("JUMPING")]
    [SerializeField] private float _jumpHeight1 = 30;
    [SerializeField] private float _jumpHeight2 = 25;
    [SerializeField] private float _jumpHeight3 = 20;
    [SerializeField] private float _wallJumpHeight = 20;
    [SerializeField] private float _wallJumpWidth = 10;
    [SerializeField] private int wallJumpCost = 3;
    [SerializeField] private float _jumpApexThreshold = 10f;
    [SerializeField] private float _coyoteTimeThreshold = 0.1f;
    [SerializeField] private float _jumpBuffer = 0.1f;
    [SerializeField] private float _jumpEndEarlyGravityModifier = 3;
    [SerializeField] private bool _enableAirJump = true;

    bool isWallJump;

    #region Check Transition
    public override bool CheckEnterTransition(CharacterState fromState)
    {
        isWallJump = fromState.GetType() == typeof(WallSlideState);
        if (!PlayerController.CanJump)
            return false;
        if ((_enableAirJump && JumpTimes < _maxJumpTimes) || isWallJump)
        {
            return true;
        }

        if ((PlayerController.CanUseCoyote || PlayerController.HasBufferedJump) 
            && !isWallJump && JumpTimes < _maxJumpTimes)
        {
            //PlayerController._coyoteUsable = false;
            //PlayerController._timeLeftGrounded = float.MinValue;
            //PlayerController.JumpingThisFrame = true;
            return true;
        }
        else
        {
            PlayerController.JumpingThisFrame = false;
            return false;
        }
    }

    public override void CheckExitTransition()
    {
        if (PlayerController.Input.JumpUp || _jumpTimer > _jumpDuration)
        {
            PlayerController.EndedJumpEarly = true;
            StateController.EnqueueTransition<NormalState>();
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
        _jumpTimer = 0;
        JumpTimes++;


        PlayerController.EndedJumpEarly = false;
        PlayerController.CanJump = false;
        StateController.Animator.SetBool(PlayerAnimator.GroundedKey, PlayerController.Grounded);
        StateController.Animator.SetInteger(PlayerAnimator.JumpTimesKey, JumpTimes);
        StateController.Animator.SetBool(PlayerAnimator.WallJumpKey, isWallJump);

        if (isWallJump)
        {
            PlayerController.CurrentVerticalSpeed = _wallJumpHeight;
            
            JumpTimes = wallJumpCost;
            PlayerController.CurrentHorizontalSpeed = ((WallSlideState)fromState).FacingRight ? -_wallJumpWidth : _wallJumpWidth;
        }
        else
        {
            switch (JumpTimes)
            {
                case 1:
                    PlayerController.CurrentVerticalSpeed = _jumpHeight1;
                    break;
                case 2:
                    PlayerController.CurrentVerticalSpeed = _jumpHeight2;
                    break;
                case 3:
                    PlayerController.CurrentVerticalSpeed = _jumpHeight3;
                    break;
                default:
                    Debug.LogError(124);
                    return;
            }
        }



        //_anim.ResetTrigger(GroundedKey);
        //_anim.SetBool(GroundedKey, false);


        // Only play particles when grounded (avoid coyote)
        if (PlayerController.Grounded)
        {
            //SetColor(_jumpParticles);
            //SetColor(_launchParticles);
            //_jumpParticles.Play();
        }
    }

    public override void PreUpdateBehaviour(float dt)
    {
        _jumpTimer += dt;
    }

    public override void UpdateBehaviour(float dt)
    {

        PlayerController.PerformWalk();
        PlayerController.PerformJump();
    }
}
