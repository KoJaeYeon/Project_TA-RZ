using UnityEngine;

public abstract class Data
{
    public abstract Data DeepCopy();

}
//IEnumerator LoadData(string idStr)
//{
//    while (true)
//    {
//        var stat = dataManager.GetStat(idStr) as PC_Common_Stat;
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