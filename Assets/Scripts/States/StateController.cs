using Lightbug.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpdatable
{
    void PreUpdateBehaviour(float dt);
    void UpdateBehaviour(float dt);
    void PostUpdateBehaviour(float dt);
}

public class StateController : MonoBehaviour
{
    [SerializeField] private CharacterState currentState = null;

    private CharacterState prevState = null;
    private Dictionary<string, CharacterState> states = new Dictionary<string, CharacterState>();
    private Queue<CharacterState> transitionsQueue = new Queue<CharacterState>();
    bool CanCurrentStateOverrideAnimatorController => currentState.OverrideAnimatorController && Animator != null && currentState.RuntimeAnimatorController != null;

    public CharacterState CurrentState
    {
        get
        {
            return currentState;
        }
    }

    public CharacterState PreviousState
    {
        get
        {
            return prevState;
        }
    }
    public Animator Animator { get; private set; }


    #region Unity Life Cycle
    private void Awake()
    {
        Animator = this.GetComponentInBranch<PlayerController, Animator>();
        AddAndInitializeStates();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dt = Time.deltaTime;

        if (currentState == null)
            return;

        bool changeOfState = CheckForTransitions();

        // Reset the transitions
        transitionsQueue.Clear();

        if (changeOfState)
        {
            prevState.ExitBehaviour(dt, currentState);

            if (CanCurrentStateOverrideAnimatorController)
                Animator.runtimeAnimatorController = currentState.RuntimeAnimatorController;

            currentState.EnterBehaviour(dt, prevState);

            Debug.Log($"State Change from {prevState.GetType().Name} to {currentState.GetType().Name}");
        }



        currentState.PreUpdateBehaviour(dt);
        currentState.UpdateBehaviour(dt);
        currentState.PostUpdateBehaviour(dt);

    }


    #endregion

    void AddAndInitializeStates()
    {
        CharacterState[] charcterStates = GetComponentsInChildren<CharacterState>(); 
        foreach (CharacterState state in charcterStates)
        {
            string name = state.GetType().Name;
            if (GetState(name) != null)
            {
                Debug.Log("Warning: GameObject " + state.gameObject.name + " has the state " + name + " repeated in the hierarchy.");
                continue;
            }
            states.Add(name, state);
        }
    }

    public CharacterState GetState(string stateName)
    {
        CharacterState state = null;
        bool validState = states.TryGetValue(stateName, out state);

        return state;
    }

    public CharacterState GetState<T>() where T : CharacterState
    {
        foreach (var state in states.Values)
        {
            if (state.GetType() == typeof(T))
                return state;

        }

        return null;
    }

    bool CheckForTransitions()
    {
        currentState.CheckExitTransition();

        CharacterState nextState = null;

        while (transitionsQueue.Count != 0)
        {
            CharacterState thisState = transitionsQueue.Dequeue();

            if (thisState == null)
                continue;

            if (!thisState.enabled)
                continue;

            bool success = thisState.CheckEnterTransition(currentState);

            if (success)
            {
                nextState = thisState;

                prevState = currentState;
                currentState = nextState;

                return true;
            }

        }
        return false;
    }

    public void EnqueueTransition<T>() where T : CharacterState
    {
        CharacterState state = GetState<T>();

        if (state != null)
            transitionsQueue.Enqueue(state);
    }


}
