using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

public class PlayerUIView : MonoBehaviour
{
    [SerializeField] InputActionReference cancelAction;
    [SerializeField] InputActionReference infoAction;
    [SerializeField] Slider HPSlider;
    [SerializeField] Slider SkillSlider;
    [SerializeField] Slider StaminaSlider;
    [SerializeField] TextMeshProUGUI CurrenAmmoText;
    [SerializeField] TextMeshProUGUI Resource_OwnNum_Text;
    [SerializeField]
    GameObject[] GageImages;
    [Header("Panel")]
    [SerializeField] GameOverPanel GameOverPanel;

    [Inject] Player _player;
    [Inject] UIEvent UIEvent;
        
    private void OnEnable()
    {
        if (_player != null)
        {
            _player.PropertyChanged += OnPropertyChanged;
            RefreshView();
            cancelAction.action.Enable();
            cancelAction.action.performed += OnCancel;
            InfoActionOnEnable();
        }
        else
        {
            Debug.LogError("PlayerView Is Null!");
        }
    }
    private void OnDisable()
    {
        InfoActionOnDisable();
        _player.PropertyChanged -= OnPropertyChanged;
        cancelAction.action.performed -= OnCancel;
    }

    private void InfoActionOnEnable()
    {
        infoAction.action.Enable();
        infoAction.action.performed += OnInfo;
    }

    private void InfoActionOnDisable()
    {
        infoAction.action.performed -= OnInfo;
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        if (UIEvent._gameUI.gameObject.activeSelf == true) return;
        UIEvent.SetActiveMenuUI();
    }

    private void OnInfo(InputAction.CallbackContext context)
    {
        UIEvent.SetActiveInfoUI();
    }

    void RefreshView()
    {
        HPSlider.value = _player.CurrentHP / _player.HP;
        StaminaSlider.value = _player.CurrentStamina / 100f;
        SkillSlider.value = _player.CurrentSkill / 100f;
        CurrenAmmoText.text = _player.CurrentAmmo.ToString("000");
        Resource_OwnNum_Text.text = _player.CurrentResourceOwn.ToString("000");
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
            case nameof(_player.CurrentResourceOwn):
                Resource_OwnNum_Text.text = _player.CurrentResourceOwn.ToString("000");
                break;
            case nameof(PlayerDeath):
                GameOverPanel.gameObject.SetActive(true);
                break;

        }
    }

    void AcitveSkillImage(float currentSkill)
    {
        if(currentSkill < _player._skillCounption[0])
        {
            GageImages[0].SetActive(false);
            GageImages[1].SetActive(false);
            GageImages[2].SetActive(false);
            GageImages[3].SetActive(false);
        }
        else if (currentSkill < _player._skillCounption[1])
        {
            GageImages[0].SetActive(true);
            GageImages[1].SetActive(false);
            GageImages[2].SetActive(false);
            GageImages[3].SetActive(false);
        }
        else if (currentSkill < _player._skillCounption[2])
        {
            GageImages[0].SetActive(true);
            GageImages[1].SetActive(true);
            GageImages[2].SetActive(false);
            GageImages[3].SetActive(false);
        }
        else if (currentSkill < _player._skillCounption[3])
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
