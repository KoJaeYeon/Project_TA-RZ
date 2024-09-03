using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
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
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
