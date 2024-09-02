using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnableChoiceUI : MonoBehaviour
{
    [SerializeField] private GameObject _ui;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_ui);
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(_ui);
        }
    }
}
