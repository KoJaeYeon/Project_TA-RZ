using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class BlueChipSystem : MonoBehaviour
{
    #region InJect
    [Inject]
    private DataManager _dataManager;
    [Inject]
    private DiContainer _diContainer;
    #endregion

    [Header("ParticlePrefab")]
    [Header("Poison")]
    [SerializeField] private GameObject _poisonObject;
    [Header("Explosion")]
    [SerializeField] private GameObject _exploObject;
    [Header("PoisonExplosion")]
    [SerializeField] private GameObject _poisonExObject;

    private Dictionary<string, PC_BlueChip> _dataDictionary;
    private Dictionary<string, IBlueChipSystem> _currentBlueChipDictionary = new Dictionary<string, IBlueChipSystem>();
    private Action<Transform, AttackType, bool> _blueChipSystem;

    private void Awake()
    {
        StartCoroutine(LoadBlueChipData()); 
    }

    private IEnumerator LoadBlueChipData()
    {
        _dataDictionary = new Dictionary<string, PC_BlueChip>();

        yield return new WaitWhile(() =>
        {
            Debug.Log("블루칩 데이터를 가져오지 못했습니다.");
            return _dataManager.GetData("G201") == null;
        });

        for(int i = 0; i < 4; i++)
        {
            var data = _dataManager.GetData($"G20{i+1}") as PC_BlueChip;

            string id = "G20" + (i + 1).ToString();

            _dataDictionary.Add(id, data);
        }
        
    }

    public PC_BlueChip GetBlueChipData(string blueID)
    {
        if(_dataDictionary.TryGetValue(blueID, out PC_BlueChip data))
        {
            return data;
        }
        else
        {
            return null;
        }
    }

    public void UseBlueChip(Transform transform, AttackType currentAttackType, bool isStart = true)
    {
        _blueChipSystem?.Invoke(transform, currentAttackType, isStart);
    }

    public void SetBlueChipSkill(string blueID)
    {
        if (!_currentBlueChipDictionary.ContainsKey(blueID))
        {
            switch (blueID)
            {
                case "G201":
                    PoisonBlueChip poisonBlueChip = _diContainer.Instantiate<PoisonBlueChip>();
                    poisonBlueChip.InitializeBlueChip(this, GetBlueChipData(blueID));
                    RegisterBlueChip(poisonBlueChip, blueID);
                    poisonBlueChip.SetEffectObject(_poisonObject);
                    Debug.Log("Poison 기능을 얻었습니다.");
                    break;
                case "G202":
                    ExplosionBlueChip explosionBlueChip = _diContainer.Instantiate<ExplosionBlueChip>();
                    explosionBlueChip.InitializeBlueChip(this, GetBlueChipData(blueID));
                    RegisterBlueChip(explosionBlueChip, blueID);
                    explosionBlueChip.SetEffectObject(_exploObject);
                    Debug.Log("Explosion 기능을 얻었습니다.");
                    break;
                case "G203":
                    UnRegisterBlueChip("G201");
                    UnRegisterBlueChip("G202");

                    PoisonExplosionRedChip poisonExplosionRedChip = _diContainer.Instantiate<PoisonExplosionRedChip>();
                    poisonExplosionRedChip.InitializeBlueChip(this, GetBlueChipData(blueID));
                    RegisterBlueChip(poisonExplosionRedChip, blueID);
                    poisonExplosionRedChip.SetEffectObject(_poisonExObject);
                    Debug.Log("PoisonExplosion기능을 얻었습니다.");
                    break;
                case "G204":
                    MagneticBlueChip magneticBlueChip = _diContainer.Instantiate<MagneticBlueChip>();
                    magneticBlueChip.InitializeBlueChip(this, GetBlueChipData(blueID));
                    RegisterBlueChip(magneticBlueChip, blueID);
                    Debug.Log("Magnetic 기능을 얻었습니다.");
                    break;
            }
        }
        else
        {
            var selectBlueChip = _currentBlueChipDictionary[blueID];
            selectBlueChip.LevelUpBlueChip();
        }
    }

    private void RegisterBlueChip(IBlueChipSystem newBlueChip, string blueID)
    {
        _currentBlueChipDictionary.Add(blueID, newBlueChip);
        _blueChipSystem += newBlueChip.UseBlueChip;
    }

    private void UnRegisterBlueChip(string blueID)
    {
        if (_currentBlueChipDictionary.ContainsKey(blueID))
        {
            var blueChip = _currentBlueChipDictionary[blueID];

            blueChip.ResetSystem();

            _blueChipSystem -= blueChip.UseBlueChip;

            _currentBlueChipDictionary.Remove(blueID);
        }
    }
    
}
