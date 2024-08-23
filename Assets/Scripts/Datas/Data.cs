using UnityEngine;

[System.Serializable]
public abstract class Data
{   

}
//IEnumerator LoadStat()
//{
//    yield return new WaitWhile(() => {
//        Debug.Log("Player의 데이터를 받아오는 중입니다.");
//        return dataManager.GetStat("P101") == null;
//    });

//    var stat = dataManager.GetStat("P101") as PC_Common_Stat;
//    _playerStat = stat;
//    Debug.Log("Player의 스탯을 성공적으로 받아왔습니다.");
//    CurrentAtk = _playerStat.Atk_Power;
//    CurrentHP = _playerStat.HP;
//    HP = _playerStat.HP;
//    CurrentStamina = 100;
//    CurrentSkill = 0;
//    CurrentAmmo = 0;
//    OnPropertyChanged(nameof(stat.Resource_Own_Num));
//    yield break;
//}