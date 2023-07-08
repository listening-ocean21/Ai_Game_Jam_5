using AnttiStarterKit.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;


#if ENABLE_INPUT_SYSTEM
[RequireComponent(typeof(PlayerInput))]
#endif
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour {

    public static PlayerController Instance;

    
    [HideInInspector] public Player Player;
    // This is horrible, but for some reason colliders are not fully established when update starts...
    private bool active;
    private BoxCollider2D boxCollider;

    #region UI
    [SerializeField] private TopPanel topPanel;
    #endregion


    public UnityAction OnLevelUp;
    public UnityEvent OnPlayerEat;





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
        // 快捷键
        if (Input.GetKeyDown(KeyCode.R))
        {
            // TODO: Rotate
        }

        // Dev
        //if (DevKey.Down(KeyCode.L))
        //{
        //    LevelUp();
        //}
        if (DevKey.Down(KeyCode.H))
        {
            AddPlayerHealth(1);
        }
        if (DevKey.Down(KeyCode.S))
        {
            AddPlayerSan(1);
        }
        if (DevKey.Down(KeyCode.T))
        {
            AddPlayerStrength(1);
        }
        if (DevKey.Down(KeyCode.D))
        {
            AddDay();
        }
        if (DevKey.Down(KeyCode.E))
        {
            PlayerEat();
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


    // TODO: 食物类型
    private void PlayerEat()
    {

        OnPlayerEat?.Invoke();
    }


    private void Rotate()
    {

    }

    public void AddPlayerHealth(int val)
    {
        Player.Attributes.Health += val;
        topPanel.healthScroller.Add(val);
    }

    public void AddPlayerSan(int val)
    {
        Player.Attributes.San += val;
        topPanel.sanScroller.Add(val);
    }

    public void AddPlayerStrength(int val)
    {
        Player.Attributes.Strength += val;
        topPanel.strengthScroller.Add(val);
    }

    public void LevelUp()
    {
        if (Player.Attributes.Level >= Player.MaxLevel) return;
        Player.Attributes.LevelUp(1);
        topPanel.levelText.text = $"Lv {Player.Attributes.Level}";
        OnLevelUp?.Invoke();
    }

    public void AddDay()
    {
        if (Player.Attributes.Day >= Player.MaxDay) return;
        Player.Attributes.Day++;
        topPanel.dayText.text = $"{Player.Attributes.Day}";
        if (Player.Attributes.Day % 5 == 1)
        {
            LevelUp();
        }
    }



}
