using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interact_Text;

    public void OnSetText(string interactText)
    {
        interact_Text.text = interactText;
    }
}
