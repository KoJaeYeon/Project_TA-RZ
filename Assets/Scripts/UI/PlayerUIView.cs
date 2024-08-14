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

    [Inject] private Player _player;
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
                break;
            case nameof(_player.CurrentAmmo):
                CurrenAmmoText.text = _player.CurrentAmmo.ToString("000");
                break;
            case nameof(_player._playerStat.Resource_Own_Num):
                Resource_OwnNum_Text.text = _player._playerStat.Resource_Own_Num.ToString("000");
                break;

        }
    }
}
