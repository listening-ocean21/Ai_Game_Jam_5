using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : GameActor
{

    public Rigidbody m_Rigidbody;
    public Transform m_Body;
    public PlayerAttributes m_Attributes;


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

    }

    protected override void Hit(DamageContext context)
    {
        base.Hit(context);
        //IndicatorManager.instance.Spawn(transform.position, context.value.ToString());
    }

    public class PlayerAttributes
    {
        public Vector3 Velocity;
        public float Speed;

        public PlayerAttributes()
        {
            Velocity = Vector3.zero;
            Speed = 1f;
        }
    }
}




