using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_RushMark : MonoBehaviour
{
    [SerializeField] private BossController _boss;

    private float _rushRange;
    private float _waitTime;
    private float _drawRushMark;

    private Vector3 _defaultScale;

    private void Awake()
    {
        _defaultScale = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = _defaultScale;

        _rushRange = _boss.rushRange;
        _waitTime = _boss.rushWailtTime;

        _drawRushMark = _rushRange / _waitTime;
    }

    private void Update()
    {
        OnUpdateUpScale();
    }

    private void OnUpdateUpScale()
    {
        if (transform.localScale.z >= _rushRange) return;

        Vector3 scale = transform.localScale;
        scale.z += _drawRushMark * Time.deltaTime;
        transform.localScale = scale;
    }
}
