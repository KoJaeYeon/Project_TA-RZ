using UnityEngine;

public interface IHit
{
    public void Hit(float damage, float paralysisTime);

    public void ApplyKnockback(Vector3 otherPosition);
}

