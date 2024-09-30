using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class EnhancedViewModel
{
    [Inject]
    private Player _player;
    [Inject]
    private UIEvent _uiEvent;

    private float _baseAttack;
    private float _elementalAttack;
    private float _addHp;
    private float _addMove;
    private float _addStatRecovery;
    private float _addOWnNum;

    public float BaseAttack
    {
        get
        {
            return _baseAttack;
        }
        set
        {
            if(_baseAttack == value)
            {
                return;
            }

            _baseAttack = value;
            OnPropertyChanged(nameof(BaseAttack));
        }
    }

    public float ElementalAttack
    {
        get
        {
            return _elementalAttack;
        }
        set
        {
            if( _elementalAttack == value)
            {
                return;
            }

            _elementalAttack = value;
            OnPropertyChanged(nameof(ElementalAttack));
        }
    }

    public float AddHP
    {
        get
        {
            return _addHp;
        }
        set
        {
            if(_addHp == value)
            {
                return;
            }

            _addHp = value;
            OnPropertyChanged(nameof(AddHP));
        }
    }

    public float AddMove
    {
        get
        {
            return _addMove;
        }
        set
        {
            if(_addMove == value)
            {
                return;
            }

            _addMove = value;
            OnPropertyChanged(nameof(AddMove));
        }
    }

    public float AddStatRecovery
    {
        get
        {
            return _addStatRecovery;
        }
        set
        {
            if(_addStatRecovery == value)
            {
                return;
            }

            _addStatRecovery = value;
            OnPropertyChanged(nameof(AddStatRecovery));
        }
    }

    public float AddOWnNum
    {
        get
        {
            return _addOWnNum;
        }
        set
        {
            if(_addOWnNum == value)
            {
                return;
            }

            _addOWnNum = value;
            OnPropertyChanged(nameof(AddOWnNum));
        }
    }

    public void OnResponsPropertyValue(string propertyName)
    {
        switch (propertyName)
        {
            case "BaseAttack":
                float baseAttackValue = _player.PlayerPassiveData.BAttack;
                BaseAttack = baseAttackValue;
                break;
            case "ElementalAttack":
                float elementalValue = _player.PlayerPassiveData.EAttack;
                ElementalAttack = elementalValue;
                break;
            case "AddHP":
                float addHpValue = _player.PlayerPassiveData.AddHP;
                AddHP = addHpValue;
                break;
            case "AddMove":
                float addMoveValue = _player.PlayerPassiveData.AddMove;
                AddMove = addMoveValue;
                break;
            case "AddStatRecovery":
                float addStatValue = _player.PlayerPassiveData.AddStaRecovery;
                AddStatRecovery = addStatValue;
                break;
            case "AddOWnNum":
                float addOwnNumValue = _player.PlayerPassiveData.AddOwnNum;
                AddOWnNum = addOwnNumValue;
                break;
        }
    }

    public void RegisterChangeEnhancedUIOnEnable()
    {
        _uiEvent.RegisterChangeEnhancedUI(this);
    }

    public void UnRegisterChangeEnhancedUIOnDisable()
    {
        _uiEvent.UnRegisterChangeEnhancedUI(this);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
