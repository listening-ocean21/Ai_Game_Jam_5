using AnttiStarterKit.Utils;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : GameActor
{

    public Rigidbody m_Rigidbody;
    public Transform m_Body;
    public PlayerAttributes m_Attributes;

    public UnityAction OnLevelUp;


    // Start is called before the first frame update
    protected override void OnCreate()
    {
        base.OnCreate();
        m_Rigidbody = GetComponentInChildren<Rigidbody>();
        m_Attributes = new PlayerAttributes();
    }


    public override void Tick()
    {
        base.Tick();
        if (DevKey.Down(KeyCode.L))
        {
            LevelUp();
        }
    }

    protected override void Hit(DamageContext context)
    {
        base.Hit(context);
        //IndicatorManager.instance.Spawn(transform.position, context.value.ToString());
    }

    public void LevelUp()
    {
        m_Attributes.LevelUp(1);
        OnLevelUp?.Invoke();
    }

    public class PlayerAttributes
    {
        public int Level;           // 等级
        public int Health;          // 生命值
        public int San;             // 理智
        public int Adaptability;    // 适应力




        public PlayerAttributes()
        {
            Level = 0;
            Health = 5;
            San = 3;
            Adaptability = 12;
        }

        public void LevelUp(int val)
        {
            Level += val;
            Health += 2 * val;
            San += 1 * val;
            Adaptability -= 2 * val;
        }


    }
}




