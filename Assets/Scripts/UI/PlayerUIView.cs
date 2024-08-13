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
    [SerializeField] TextMeshProUGUI NowBullets;

    [Inject] private Player player;
    private void OnEnable()
    {
        if (player != null)
        {
            player.PropertyChanged += OnPropertyChanged;
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

    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(player.CurrentHP):
                break;
            case nameof(player.CurrentStamina):
                break;
            case nameof(player.CurrentSkill):
                break;
            case nameof(player.CurrentAmmo):
                break;
        }
    }
}
