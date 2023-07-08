using AnttiStarterKit.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;


#if ENABLE_INPUT_SYSTEM
[RequireComponent(typeof(PlayerInput))]
#endif
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour {

    [SerializeField] private LevelChanger levelChanger;

    public static PlayerController Instance;

    
    [HideInInspector] public Player Player;
    // This is horrible, but for some reason colliders are not fully established when update starts...
    private bool active;
    private BoxCollider2D boxCollider;


    public UnityAction OnLevelUp;





    void Awake()
    {
        Invoke(nameof(Activate), 0.5f);
        boxCollider = GetComponent<BoxCollider2D>();
        Player = GetComponent<Player>();
        Instance = this;
    }
    void Activate() =>  active = true;

    private void Update() {
        if(!active) return;
        // ¿ì½Ý¼ü
        if (Input.GetKeyDown(KeyCode.R))
        {
            // TODO: Rotate
        }

        // Dev
        if (DevKey.Down(KeyCode.L))
        {
            LevelUp();
        }
    }

    void FixedUpdate()
    {
        if(!active) return;
    }

    void LateUpdate()
    {
        if(!active) return;
    }




    private void Rotate()
    {

    }

    public void AddPlayerHealth(int val)
    {
        Player.Attributes.Health += val;
    }

    public void AddPlayerSan(int val)
    {
        Player.Attributes.San += val;
    }

    public void AddPlayerAdaptability(int val)
    {
        Player.Attributes.Adaptability += val;
    }

    public void LevelUp()
    {
        if (Player.Attributes.Level >= Player.MaxLevel) return;
        Player.Attributes.LevelUp(1);
        OnLevelUp?.Invoke();
    }


}
