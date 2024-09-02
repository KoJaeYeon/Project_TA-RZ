using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_HPBar : MonoBehaviour
{
    [SerializeField] private Slider _hpBar;

    private float _maxHp;
    private float _curHp;
    private float _perHp;

    private void Start()
    {
        _perHp = _curHp / _maxHp;
        _hpBar.value = _perHp;
    }
}
