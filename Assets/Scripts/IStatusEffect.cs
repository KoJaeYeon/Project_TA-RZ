using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusEffect
{
    public void Poison(float damage, float maxTime, float intervalTime);
    public void Ice();
    public void Explosion(float damage);
}
