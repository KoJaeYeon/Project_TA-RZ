using UnityEngine;

public abstract class Stat
{
    public abstract Stat DeepCopy();

}
//IEnumerator LoadStat()
//{
//    while (true)
//    {
//        var stat = dataManager.GetStat("P101") as PC_Common_Stat;
//        if (stat == null)
//        {
//            Debug.Log("Player의 스탯을 받아오지 못했습니다.");
//            yield return new WaitForSeconds(1f);
//        }
//        else
//        {
//            _playerStat = stat;
//            Debug.Log("Player의 스탯을 성공적으로 받아왔습니다.");
//            yield break;
//        }

//    }
//}