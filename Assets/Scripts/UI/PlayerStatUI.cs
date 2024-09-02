using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerStatUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _powerText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _moveSpeedText;
    [SerializeField] private TextMeshProUGUI _resourceText;
    [SerializeField] private TextMeshProUGUI _staminaText;
    [SerializeField] private TextMeshProUGUI _elementText;

    [Inject]
    private Player _player;

    private void Start()
    {
        RefreshView();
    }

    private void OnEnable()
    {
        if(_player != null)
        {
            _player.PropertyChanged += OnPropertyChangedUI;
        }
    }

    private void OnDisable()
    {
        if(_player != null )
        {
            _player.PropertyChanged -= OnPropertyChangedUI;
        }
    }

    private void RefreshView()
    {
        _nameText.text = "로이";
        _healthText.text = _player.HP.ToString();
        _powerText.text = _player.CurrentAtk.ToString();
        _moveSpeedText.text = _player.CurrentSpeed.ToString();
        _resourceText.text = _player.CurrentAmmo.ToString();
        _staminaText.text = _player._playerStat.Stamina_Gain.ToString();
        _elementText.text = _player.PassiveAtk_Power.ToString();
    }

    private void OnPropertyChangedUI(object sender, PropertyChangedEventArgs arg)
    {
        switch(arg.PropertyName)
        {
            case nameof(_player.CurrentAtk):
                _powerText.text = _player.CurrentAtk.ToString();
                break;
            case nameof(_player.HP):
                _healthText.text = _player.HP.ToString();
                break;
            case nameof(_player.CurrentSpeed):
                _moveSpeedText.text = _player.CurrentSpeed.ToString();
                break;
            case nameof(_player.CurrentAmmo):
                _resourceText.text = _player.CurrentAmmo.ToString();
                break;
        }
    }

}
