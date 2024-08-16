using UnityEngine;

public interface IHit
{
    //hit 대미지, 경직 시간, 공격자의 트랜스폼(위치 정보 받기 위함)
    /// <summary>
    /// 공격을 할 때 상대방의 피격 함수
    /// </summary>
    /// <param name="damage">공격의 데미지</param>
    /// <param name="paralysisTime">공격의 경직 시간</param>
    /// <param name="attackTrans">공격자의 트랜스폼</param>
    public void Hit(float damage, float paralysisTime, Transform attackTrans);
    //hit position, 넉백 시간
    public void ApplyKnockback(Vector3 otherPosition, float knockBackTime);
}

