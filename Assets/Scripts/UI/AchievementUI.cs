using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    [Header("기획자 인스펙터")]
    [SerializeField] float UI_Achievement_Duration;
    [Header("개발자 인스펙터")]
    [SerializeField] Text Achieve_Content_Text;

    private void OnEnable()
    {
        StartCoroutine(DeActive());
    }

    public void OnSetText(string achieveText)
    {
        Achieve_Content_Text.text = achieveText;
    }

    IEnumerator DeActive()
    {
        yield return new WaitForSeconds(UI_Achievement_Duration);
        gameObject.SetActive(false);
    }
}
