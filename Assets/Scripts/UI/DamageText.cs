using UnityEngine;
using TMPro;
using System.Collections;
using Zenject;

public class DamageText : MonoBehaviour
{
    private float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;
    TextMeshProUGUI text;
    Color alpha;
    public int damage;
    [Inject]
    PoolManager _poolManager;

    // Start is called before the first frame update
    void Awake()
    {
        moveSpeed = 2.0f;
        alphaSpeed = 2.0f;
        destroyTime = 2.0f;

        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine(DisableObject());
    }

    public void OnSetData(float damage, DamageType damageType, Transform targetTrans)
    {
        if (targetTrans.CompareTag("BossPhase1")) return;
        text.text = ((int)damage).ToString();
        switch(damageType)
        {
            case DamageType.Normal:
                alpha = Color.white;
                transform.position = targetTrans.position;// + targetTrans.forward;
                break;
            case DamageType.Poison:
                alpha = new Color(0, 176 / 255f, 80 / 255f);
                transform.position = targetTrans.position + targetTrans.up;
                break;
            case DamageType.Explosive:
                alpha = Color.red;
                transform.position = targetTrans.position + targetTrans.up;
                break;
        }
        text.color = alpha;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        text.color = alpha;
    }

    IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(destroyTime);
        DestroyObject();
    }

    private void DestroyObject()
    {
        _poolManager.EnqueueObject(gameObject);
    }
}
