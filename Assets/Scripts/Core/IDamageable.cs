using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageContext
{
    public int value = 1;
    public GameEntity source = null;

    public DamageContext(int value, GameEntity source)
    {
        this.value = value;
        this.source = source;
    }
}

public interface IDamageable
{
    void TakeDamage(DamageContext damage)
    {

    }


}
