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
    [SerializeField] GameObject _parent;

    // Start is called before the first frame update
    void Awake()
    {
        moveSpeed = 2.0f;
        alphaSpeed = 2.0f;
        destroyTime = 2.0f;

        text = GetComponent<TextMeshProUGUI>();
        alpha = text.color;
        text.text = damage.ToString();
    }

    private void OnEnable()
    {
        StartCoroutine(DisableObject());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        text.color = alpha;
    }

    IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(destroyTime);
    }

    private void DestroyObject()
    {
        _poolManager.EnqueueObject(_parent);
    }
}
