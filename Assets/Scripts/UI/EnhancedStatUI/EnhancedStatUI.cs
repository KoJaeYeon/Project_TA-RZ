using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using Zenject;

public enum EnhancedList
{
    BaseAttack,
    ElementalAttack,
    AddHp,
    AddMove,
    AddStatRecovery,
    AddOWnNum
}

public class EnhancedStatUI : MonoBehaviour
{
    [Inject]
    private DiContainer _diContainer;
    [Inject]
    private UIEvent _uiEvent;
    [Inject]
    private DataManager _dataManager;

    private EnhancedViewModel _viewModel;

    [Header("StatNameText")]
    [SerializeField] private TextMeshProUGUI[] _statNameText;
    [Header("ValueText")]
    [SerializeField] private TextMeshProUGUI[] _valueText;
    [Header("stringArray")]
    [SerializeField] private string[] _enhancedName;

    private Dictionary<EnhancedList, string> _enhancedListDictionary;

    private void OnEnable()
    {
        if(_viewModel == null)
        {
            _viewModel = _diContainer.Instantiate<EnhancedViewModel>();

            _viewModel.PropertyChanged += ChangeEnhancedUI;

            _viewModel.RegisterChangeEnhancedUIOnEnable();

            AddEnhancedDictionary();
        }

        _uiEvent.RequestChangeEnhancedUI("BaseAttack");
        _uiEvent.RequestChangeEnhancedUI("ElementalAttack");
        _uiEvent.RequestChangeEnhancedUI("AddHP");
        _uiEvent.RequestChangeEnhancedUI("AddMove");
        _uiEvent.RequestChangeEnhancedUI("AddStatRecovery");
        _uiEvent.RequestChangeEnhancedUI("AddOWnNum");
    }

    private void AddEnhancedDictionary()
    {
        _enhancedListDictionary = new Dictionary<EnhancedList, string>();

        for(int i = 0; i < _enhancedName.Length; i++)
        {
            string name = _enhancedName[i];

            _enhancedListDictionary.Add((EnhancedList)i, name);
        }
    }

    private string GetEnhancedText(EnhancedList enhancedList)
    {
        if (_enhancedListDictionary.TryGetValue(enhancedList, out string text))
        {
            string enhancedText = text;

            return enhancedText;
        }
        else
            return string.Empty;
    }

    private void ChangeEnhancedUI(object sender, PropertyChangedEventArgs args)
    {
        switch (args.PropertyName)
        {
            case nameof(_viewModel.BaseAttack):
                OnEnableEnhancedUI(EnhancedList.BaseAttack, _viewModel.BaseAttack);
                break;
            case nameof(_viewModel.ElementalAttack):
                OnEnableEnhancedUI(EnhancedList.ElementalAttack, _viewModel.ElementalAttack);
                break;
            case nameof(_viewModel.AddHP):
                OnEnableEnhancedUI(EnhancedList.AddHp, _viewModel.AddHP);
                break;
            case nameof(_viewModel.AddMove):
                OnEnableEnhancedUI(EnhancedList.AddMove, _viewModel.AddMove);
                break;
            case nameof(_viewModel.AddStatRecovery):
                OnEnableEnhancedUI(EnhancedList.AddStatRecovery, _viewModel.AddStatRecovery);
                break;
            case nameof(_viewModel.AddOWnNum):
                OnEnableEnhancedUI(EnhancedList.AddOWnNum, _viewModel.AddOWnNum);
                break;
        }
    }

    private void OnEnableEnhancedUI(EnhancedList enhancedList, float value)
    {
        string enhancedText = GetEnhancedText(enhancedList);

        string valueText = _dataManager.GetString(enhancedText);

        string[] splitArray = valueText.Split('+');

        ActiveUI((int)enhancedList, true);

        _statNameText[(int)enhancedList].text = splitArray[0];

        _valueText[(int)enhancedList].text = " + " + string.Format(splitArray[1], value);
    }

    private void OnDisableEnhancedUI(EnhancedList enhancedList)
    {
        ActiveUI((int)enhancedList, false);
    }

    private void ActiveUI(int index, bool active)
    {
        _statNameText[index].gameObject.SetActive(active);
        _valueText[index].gameObject.SetActive(active);
    }
}
