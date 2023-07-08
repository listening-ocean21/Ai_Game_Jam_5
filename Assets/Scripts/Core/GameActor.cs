using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameActor : GameEntity, IDamageable
{
    public bool IsDead { get; private set; } = false;

    public UnityEvent OnDeath;
    public UnityEvent<DamageContext> OnTakeDamage;
    public UnityEvent OnSpawn;

    [SerializeField] protected int baseHealth;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] SFXObject deathSound;
    [SerializeField] protected float deathTime;

    public Transform body;

    public int Health { get; protected set; }


    protected override void OnCreate()
    {
        base.OnCreate();
        IsDead = false;
        Health = baseHealth;

        OnSpawn?.Invoke();
    }

    public void TakeDamage(DamageContext context)
    {
        if (IsDead) return;

        Health -= context.value;
        Hit(context);
        OnTakeDamage.Invoke(context);

        // IndicatorManager.instance.Spawn(transform.position, context.value.ToString());
        // StartCoroutine(Hit());

        if (Health <= 0)
        {
            StartCoroutine(Die());
        }
    }


    protected virtual void Hit(DamageContext context)
    {

    }

    private IEnumerator Die()
    {
        BeforeDead();
        float time = deathTime;
        if (deathParticle && deathTime < 0.01f) 
            time = deathParticle.main.duration;
        while (deathTime > 0f)
        {
            InDead(1f - time / deathTime);
            deathTime -= Time.deltaTime;
            yield return null;
        }
        AfterDead();
    }

    protected virtual void BeforeDead()
    {
        if (deathParticle) deathParticle.Play();
        //if (deathSound) AudioManager.instance.Play(deathSound);
        IsDead = true;
        body.gameObject.SetActive(false);
        OnDeath.Invoke();
    }

    protected virtual void InDead(float progress)
    {

    }

    protected virtual void AfterDead()
    {
        Delete();
    }

    protected override bool IsTickEnable()
    {
        return !IsDead && !GameInstance.IsPaused;
    }
}
