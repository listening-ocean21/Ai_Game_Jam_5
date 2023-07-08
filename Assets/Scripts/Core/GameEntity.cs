using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;




public class GameEntity : MonoBehaviour
{
    private bool initialized = false;


    public Action OnDeleted;
    public Action OnCreated;

    /// <summary>
    /// Internal init method, called after spawning the object, executes before the awake method
    /// </summary>
    internal void Init()
    {
        OnCreate();

        initialized = true;
        OnCreated?.Invoke();
    }

    /// <summary>
    /// Internal delete method, can be called by object instead, replaces Destroy(gameobject) function
    /// </summary>
    public void Delete()
    {
        //Spawner.RemoveFromPool(this);
        OnDeleted?.Invoke();
        OnDelete();
    }



    /// <summary>
    /// Called when object has been created (before Awake)
    /// </summary>
    protected virtual void OnCreate() { }

    /// <summary>
    /// Called on object deletion, by default just destroys the object, could be used for pooling behavior
    /// </summary>
    protected virtual void OnDelete()
    {
        Destroy(gameObject);
    }



    private void Awake()
    {
        // Normally object are gonna be created by spawner, but if Game entity starts up in scene it will initialize on Awake()
        if (!initialized)
        {
            Init();
        }
    }


    private void Update()
    {
        if (IsTickEnable())
        {
            Tick();
        }
    }

    public virtual void Tick()
    {

    }

    protected virtual bool IsTickEnable() { return true; }

    public virtual GameEntity Clone()
    {
        return new GameEntity();
    }
}
