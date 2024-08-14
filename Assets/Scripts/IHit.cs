using UnityEngine;

public interface IHit
{
    public void Hit( float damage);

    public void ApplyKnockback(Vector3 otherPosition);
}
