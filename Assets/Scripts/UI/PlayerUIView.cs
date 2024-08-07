using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ViewModel.Extensions;

public class PlayerUIView : MonoBehaviour
{
    [SerializeField] Slider HPSlider;
    [SerializeField] Slider SkillSlider;
    [SerializeField] Slider StaminaSlider;
    [SerializeField] TextMeshProUGUI NowBullets;

    private PlayerUIViewModel _vm;
    private void OnEnable()
    {
        if (_vm == null)
        {
            _vm = new PlayerUIViewModel();
            _vm.PropertyChanged += OnPropertyChanged;
            _vm.RegisterEventsOnEnable();
            _vm.RefreshViewModel();
        }
    }
    private void OnDisable()
    {
        if (_vm != null)
        {
            _vm.UnRegisterOnDisable();
            _vm.PropertyChanged -= OnPropertyChanged;
            _vm = null;
        }
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.Hp):
                break;
            case nameof(_vm.Skills):
                break;
            case nameof(_vm.Stamina):
                break;
            case nameof(_vm.Nowbullets):
                break;
        }
    }
}
