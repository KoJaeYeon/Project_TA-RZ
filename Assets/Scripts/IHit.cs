using UnityEngine;

public interface IHit
{
    //hit 대미지, 경직 시간
    public void Hit(float damage, float paralysisTime);
    //hit position, 넉백 시간
    public void ApplyKnockback(Vector3 otherPosition, float knockBackTime);
}

