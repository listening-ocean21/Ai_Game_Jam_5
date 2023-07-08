using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.Utilities;
using Unity.VisualScripting;

public class CharacterState : MonoBehaviour, IUpdatable
{
    [SerializeField] private bool overrideAnimatorController = true;
    [SerializeField] private RuntimeAnimatorController runtimeAnimatorController = null;

    public PlayerController PlayerController { get; private set; }
    public StateController StateController { get; private set; }


    public bool OverrideAnimatorController => overrideAnimatorController;
    public RuntimeAnimatorController RuntimeAnimatorController => runtimeAnimatorController;


    public virtual void Awake()
    {
        PlayerController = this.GetComponentInBranch<PlayerController>();
        StateController = this.GetComponentInBranch<PlayerController, StateController>();
    }

    #region Check Transition
    public virtual bool CheckEnterTransition(CharacterState fromState)
    {
        return true;
    }

    public virtual void CheckExitTransition()
    {

    }
    #endregion

    #region Update
    public virtual void PreUpdateBehaviour(float dt)
    {

    }

    public virtual void UpdateBehaviour(float dt)
    {

    }

    public virtual void PostUpdateBehaviour(float dt)
    {

    }
    #endregion

    #region Enter Exit
    public virtual void ExitBehaviour(float dt, CharacterState toState)
    {

    }

    public virtual void EnterBehaviour(float dt, CharacterState fromState)
    {

    }
    #endregion

}
