using System.Collections;
using UnityEngine;

public class GimmickController : MonoBehaviour
{
    [SerializeField] private float _radius;
    private float _result;
    [SerializeField] private float _downValue;
    [SerializeField] private float _upValue;

    private bool _isHurt;

    private Vector3 _scale;
    private Coroutine _hurtCoroutine;

    private void Awake()
    {
        _scale = transform.localScale;
        _result = (float)Mathf.Sqrt(_radius * _radius + _radius * _radius);
    }

    private void OnEnable()
    {
        transform.localScale = _scale;
        _isHurt = false;
    }

    private void Update()
    {
        if (!_isHurt) OnActivateNonCombat();
    }

    public void OnActivateCombat()
    {
        if (transform.localScale.x <= _downValue) ChangedScale(false, transform.localScale.x);
        else ChangedScale(false, _downValue);

        _isHurt = true;

        if (_hurtCoroutine != null)
        {
            StopCoroutine(_hurtCoroutine);
        }

        _hurtCoroutine = StartCoroutine(CoDelayTime());
    }

    private void OnActivateNonCombat()
    {
        if (transform.localScale.x >= _result) return;

        ChangedScale(true, _upValue);
    }

    private void ChangedScale(bool updown, float changedScale)
    {
        Vector3 scale = transform.localScale;
        switch (updown)
        {
            case true:          
                scale.x += changedScale * Time.deltaTime;
                scale.z += changedScale * Time.deltaTime;
                transform.localScale = scale;
                break;
            case false:
                scale.x -= changedScale;
                scale.z -= changedScale;
                transform.localScale = scale;
                break;
        }
    }

    private IEnumerator CoDelayTime()
    {
        yield return new WaitForSeconds(1f);
        _isHurt = false;
    }
}
