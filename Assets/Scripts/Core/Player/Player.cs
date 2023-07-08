using AnttiStarterKit.Utils;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public int MaxLevel = 5;
    public int MaxDay = 25;

    public Transform m_Body;
    public PlayerAttributes Attributes;


    private void Awake()
    {
        Attributes = new PlayerAttributes();
    }

    // Start is called before the first frame update
    public void Start()
    {

    }


    public void Update()
    {

    }

    //protected override void Hit(DamageContext context)
    //{
    //    base.Hit(context);
    //    //IndicatorManager.instance.Spawn(transform.position, context.value.ToString());
    //}


    public class PlayerAttributes
    {
        public int Level;           // 等级
        public int Health;          // 生命值
        public int San;             // 理智
        public int Strength;    // 适应力
        public int Day;             // 天数


        public PlayerAttributes()
        {
            Level = 1;
            Day = 1;
            Health = 5;
            San = 4;
            Strength = 12;
        }

        public void LevelUp(int val)
        {
            Level += val;
            Health += 2 * val;
            San += 1 * val;
            Strength -= 2 * val;
        }
    }
}




