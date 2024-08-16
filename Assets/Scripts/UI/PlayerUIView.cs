using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ViewModel.Extensions;
using Zenject;

public class PlayerUIView : MonoBehaviour
{
    [SerializeField] Slider HPSlider;
    [SerializeField] Slider SkillSlider;
    [SerializeField] Slider StaminaSlider;
    [SerializeField] TextMeshProUGUI CurrenAmmoText;
    [SerializeField] TextMeshProUGUI Resource_OwnNum_Text;
    [SerializeField]
    GameObject[] GageImages;

    [Inject] private Player _player;

    private int[] _skillCounption = new int[4] { 25, 50, 75, 100 };
    private void OnEnable()
    {
        if (_player != null)
        {
            _player.PropertyChanged += OnPropertyChanged;
            RefreshView();
        }
        else
        {
            Debug.LogError("PlayerView Is Null!");
        }
    }
    private void OnDisable()
    {
        _player.PropertyChanged -= OnPropertyChanged;
    }

    void RefreshView()
    {
        HPSlider.value = _player.CurrentHP / _player.HP;
        StaminaSlider.value = _player.CurrentStamina / 100f;
        SkillSlider.value = _player.CurrentSkill / 100f;
        CurrenAmmoText.text = _player.CurrentAmmo.ToString("000");
        Resource_OwnNum_Text.text = _player._playerStat.Resource_Own_Num.ToString("000");
        StartCoroutine(LoadData("S201"));
    }

    IEnumerator LoadData(string idStr)
    {
        while (true)
        {
            var data = _player.dataManager.GetData(idStr) as PC_Skill;
            if (data == null)
            {
                Debug.Log("Player의 스킬 소모값을 받아오지 못했습니다.");
                yield return new WaitForSeconds(1f);
            }
            else
            {
                _skillCounption[0] = data.Skill_Gauge_Consumption;
                for(int i = 1; i < 4; i++)
                {
                    idStr = $"S20{1+i}";
                    data = _player.dataManager.GetData(idStr) as PC_Skill;
                    _skillCounption[i] = data.Skill_Gauge_Consumption;
                }
                AcitveSkillImage(_player.CurrentSkill);
                Debug.Log("Player의 스킬 소모값을 성공적으로 받아왔습니다.");
                yield break;
            }

        }
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_player.CurrentHP):
            case nameof(_player.HP):
                HPSlider.value = _player.CurrentHP / _player.HP;
                break;
            case nameof(_player.CurrentStamina):
                StaminaSlider.value = _player.CurrentStamina / 100f;
                break;
            case nameof(_player.CurrentSkill):
                SkillSlider.value = _player.CurrentSkill / 100f;
                AcitveSkillImage(_player.CurrentSkill);
                break;
            case nameof(_player.CurrentAmmo):
                CurrenAmmoText.text = _player.CurrentAmmo.ToString("000");
                break;
            case nameof(_player._playerStat.Resource_Own_Num):
                Resource_OwnNum_Text.text = _player._playerStat.Resource_Own_Num.ToString("000");
                break;

        }
    }

    void AcitveSkillImage(float currentSkill)
    {
        if(currentSkill < _skillCounption[0])
        {
            GageImages[0].SetActive(false);
            GageImages[1].SetActive(false);
            GageImages[2].SetActive(false);
            GageImages[3].SetActive(false);
        }
        else if (currentSkill < _skillCounption[1])
        {
            GageImages[0].SetActive(true);
            GageImages[1].SetActive(false);
            GageImages[2].SetActive(false);
            GageImages[3].SetActive(false);
        }
        else if (currentSkill < _skillCounption[2])
        {
            GageImages[0].SetActive(true);
            GageImages[1].SetActive(true);
            GageImages[2].SetActive(false);
            GageImages[3].SetActive(false);
        }
        else if (currentSkill < _skillCounption[3])
        {
            GageImages[0].SetActive(true);
            GageImages[1].SetActive(true);
            GageImages[2].SetActive(true);
            GageImages[3].SetActive(false);
        }
        else
        {
            GageImages[0].SetActive(true);
            GageImages[1].SetActive(true);
            GageImages[2].SetActive(true);
            GageImages[3].SetActive(true);
        }
    }
}
