using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class passiveButton : MonoBehaviour
{
    [SerializeField] ShopUI shopUI;
    Button btn;
    Image img;
    private void Awake()
    {
        btn = GetComponent<Button>();
        img = GetComponent<Image>();
        btn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnSubmit_RequestAddPassive));
    }

    public void OnSubmit_RequestAddPassive()
    {
        Debug.Log(gameObject);
        shopUI.AddActiveObject(gameObject);        
        img.color = Color.red;
    }
}
