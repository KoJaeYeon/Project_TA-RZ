using UnityEngine;
using UnityEngine.UI;

public class Monster_D_DashUI : MonoBehaviour
{
    [SerializeField] GameObject parentUI;
    public Image DashGauge;

    private Vector3 originalPosition;  // 원래 위치를 저장할 변수

    private void Awake()
    {
        // 원래 위치를 초기화
        originalPosition = transform.localPosition;
    }

    public void OnDash()
    {
        originalPosition = transform.localPosition;
        DashGauge.fillAmount = 1;
        transform.SetParent(null, false);
    }

    public void OnDashEnd()
    {
        transform.SetParent(parentUI.transform, false);
        transform.localPosition = originalPosition;  
        DashGauge.fillAmount = 0;
    }
}
