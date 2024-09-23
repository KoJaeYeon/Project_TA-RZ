using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerEffect : MonoBehaviour
{
    [Inject] private PoolManager _poolManager;
    private Dictionary<AttackType, GameObject> _attackEffectDic = new Dictionary<AttackType, GameObject>();
    private Dictionary<AttackType, Transform> _attackEffectTransformDic = new Dictionary<AttackType, Transform>();

    [Header("Hit_Effect")]
    [SerializeField] private GameObject _hitEffect;
    [Header("Position")]
    [SerializeField] private GameObject _effectPosition;
    [Header("AttackEffect")]
    [SerializeField] private GameObject[] _attackEffectArray;
    [SerializeField] private Transform _fourthEffectTransform;
    [Header("Gradient")]
    [SerializeField] private Gradient[] _gradients;
    [SerializeField] private Material _material;

    private Player _player;
    private Light _effectLight;
    private ParticleSystem[] particleSystems;
    private WaitForSeconds _returnTime = new WaitForSeconds(0.5f);

    private void Start()
    {
        InitializePlayerEffect();
    }

    private void OnDestroy()
    {
        _material.color = _gradients[1].colorKeys[1].color;
    }

    private void InitializePlayerEffect()
    {
        //_attackEffectDic ??= new(); 또 다른 초기화 방법.

        var childTransformList = new List<Transform>();

        foreach(Transform childTransform in _effectPosition.transform)
        {
            childTransformList.Add(childTransform);
        }

        for(int i = 0; i < _attackEffectArray.Length; i++)
        {
            if(i < childTransformList.Count)
            {
                GameObject effect = Instantiate(_attackEffectArray[i], childTransformList[i]);
                _attackEffectDic.Add((AttackType)i, effect);
                _attackEffectTransformDic.Add((AttackType)i, childTransformList[i]);
            }
        }

        _poolManager.CreatePool(_hitEffect, 30);

        var fourthAttackEffectTrans = _effectPosition.transform.GetChild(3);
        fourthAttackEffectTrans.parent = null;
        fourthAttackEffectTrans.SetParent(_fourthEffectTransform);
        fourthAttackEffectTrans.localPosition = Vector3.zero;

        _attackEffectDic[AttackType.fourthAttack] = fourthAttackEffectTrans.gameObject;
        particleSystems =  fourthAttackEffectTrans.GetComponentsInChildren<ParticleSystem>();
        _effectLight = fourthAttackEffectTrans.GetComponentInChildren<Light>();
        fourthAttackEffectTrans.gameObject.SetActive(false);

        _player = gameObject.GetComponent<Player>();    
    }

    public void ChangeColor(int index)
    {
        foreach (var item in particleSystems)
        {
            var colorOverLifetime = item.colorOverLifetime; // ColorOverLifetimeModule 가져오기
            colorOverLifetime.color = new ParticleSystem.MinMaxGradient(_gradients[index]); // 변경된 값을 다시 설정     
            _material.color = _gradients[index].colorKeys[1].color;
            _effectLight.enabled = index != 0;
            _effectLight.color = _gradients[index].colorKeys[1].color;
        }
    }

    public void Active_FirstEffect()
    {
        GameObject firstEffect = GetAttackEffect(AttackType.firstAttack);
        ParticleSystem effectParticle = firstEffect.GetComponent<ParticleSystem>();

        firstEffect.transform.localPosition = Vector3.zero;
        firstEffect.transform.localRotation = Quaternion.identity;
        firstEffect.transform.localScale = Vector3.one;
        firstEffect.transform.localScale *= _player.CurrentLevel != 4 ? 1 : 2;
        firstEffect.transform.parent = null;
        effectParticle.Play();

        StartCoroutine(ReturnFirstEffect(firstEffect));
    }

    public void Active_SecondEffect()
    {
        GameObject secondEffect = GetAttackEffect(AttackType.secondAttack);
        ParticleSystem effectParticle = secondEffect.GetComponent<ParticleSystem>();

        secondEffect.transform.localPosition = Vector3.zero;
        secondEffect.transform.localRotation = Quaternion.identity;
        secondEffect.transform.localScale = Vector3.one * 1.5f;
        secondEffect.transform.localScale *= _player.CurrentLevel != 4 ? 1 : 2;
        secondEffect.transform.parent = null;
        effectParticle.Play();

        StartCoroutine(ReturnSecondEffect(secondEffect));
    }

    public void Active_ThirdEffect()
    {
        GameObject thirdEffect = GetAttackEffect(AttackType.thirdAttack);
        ParticleSystem effectParticle = thirdEffect.GetComponent<ParticleSystem>();

        thirdEffect.transform.localPosition = Vector3.zero;
        thirdEffect.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
        thirdEffect.transform.localScale = Vector3.one;
        thirdEffect.transform.localScale *= _player.CurrentLevel != 4 ? 1 : 2;

        thirdEffect.transform.parent = null;
        effectParticle.Play();

        StartCoroutine(ReturnThirdEffect(thirdEffect));
    }

    public void Active_FourthEffect(float currentAmmo)
    {
        if (currentAmmo <= 1 && _player.CurrentLevel != 4)
        {
            return;
        }
        GameObject fourthEffect = GetAttackEffect(AttackType.fourthAttack);
        ParticleSystem effectParticle = fourthEffect.GetComponentInChildren<ParticleSystem>();

        fourthEffect.transform.localPosition = Vector3.zero;
        fourthEffect.SetActive(true);
        effectParticle.Play();
    }

    public void DeActive_FourthEffect()
    {
        GameObject fourEffect = GetAttackEffect(AttackType.fourthAttack);
        fourEffect.SetActive(false);
    }

    public void Active_FinishEffect()
    {
        GameObject finishEffect = GetAttackEffect(AttackType.finishAttack);
        ParticleSystem effectParticle = finishEffect.GetComponent<ParticleSystem>();

        finishEffect.transform.localRotation = Quaternion.identity;
        finishEffect.transform.localPosition = Vector3.zero;
        finishEffect.transform.parent = null;
        effectParticle .Play();

        StartCoroutine(ReturnFinishEffect(finishEffect));
    }

    #region Get
    private GameObject GetAttackEffect(AttackType attackType)
    {
        return _attackEffectDic[attackType];
    }

    private Transform GetAttackEffectTransform(AttackType attackType)
    {
        return _attackEffectTransformDic[attackType];   
    }
    #endregion

    #region HitEffect
    public GameObject GetHitEffect()
    {
        GameObject hitEffect = _poolManager.DequeueObject(_hitEffect);

        return hitEffect;
    }

    public void ReturnHit(GameObject hitParticle)
    {
        ReturnHitParticle returnComponent = hitParticle.GetComponent<ReturnHitParticle>();

        StartCoroutine(returnComponent.ReturnParticle(hitParticle, _poolManager));
    }
    #endregion

    #region Return
    private IEnumerator ReturnFirstEffect(GameObject effect)
    {
        yield return _returnTime;

        effect.transform.SetParent(GetAttackEffectTransform(AttackType.firstAttack));
        yield break;
    }

    private IEnumerator ReturnSecondEffect(GameObject effect)
    {
        yield return _returnTime;

        effect.transform.SetParent(GetAttackEffectTransform(AttackType.secondAttack));
    }

    private IEnumerator ReturnThirdEffect(GameObject effect)
    {
        yield return _returnTime;

        effect.transform.SetParent(GetAttackEffectTransform(AttackType.thirdAttack));
    }

    private IEnumerator ReturnFinishEffect(GameObject effect)
    {
        yield return new WaitForSeconds(1.5f);

        effect.transform.SetParent(GetAttackEffectTransform(AttackType.finishAttack));
    }

    #endregion
}
