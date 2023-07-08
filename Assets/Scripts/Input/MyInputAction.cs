using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInputAction : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool dash;
    public bool downJump;

#if ENABLE_INPUT_SYSTEM
    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());

    }
    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void OnDash(InputValue value)
    {
        DashInput(value.isPressed);
    }

    public void OnDownJump(InputValue value) 
    { 
        DownJump(value.isPressed);
    }

#endif

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void DashInput(bool newDashState)
    {
        dash = newDashState;
    }

    public void DownJump(bool newDownState)
    {
        downJump = newDownState;
    }
}
