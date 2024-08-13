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

    [Inject] private Player player;
    private void OnEnable()
    {
        if (player != null)
        {
            player.PropertyChanged += OnPropertyChanged;
            RefreshView();
        }
        else
        {
            Debug.LogError("PlayerView Is Null!");
        }
    }
    private void OnDisable()
    {        
        player.PropertyChanged -= OnPropertyChanged;
    }

    void RefreshView()
    {
        HPSlider.value = player.CurrentHP / player.HP;
        StaminaSlider.value = player.CurrentStamina / 100f;
        SkillSlider.value = player.CurrentSkill / 100f;
        CurrenAmmoText.text = player.CurrentAmmo.ToString("000");
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(player.CurrentHP):
            case nameof(player.HP):
                HPSlider.value = player.CurrentHP / player.HP;
                break;
            case nameof(player.CurrentStamina):
                StaminaSlider.value = player.CurrentStamina / 100f;
                break;
            case nameof(player.CurrentSkill):
                SkillSlider.value = player.CurrentSkill / 100f;
                break;
            case nameof(player.CurrentAmmo):
                CurrenAmmoText.text = player.CurrentAmmo.ToString("000");
                break;

        }
    }
}
