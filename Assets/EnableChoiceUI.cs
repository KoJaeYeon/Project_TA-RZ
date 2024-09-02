using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnableChoiceUI : MonoBehaviour
{
    [SerializeField] private GameObject _ui;
    static GameObject SelectedChoiceButton;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_ui);
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(SelectedChoiceButton);
        }
        else
        {
            SelectedChoiceButton = EventSystem.current.currentSelectedGameObject;
        }
    }
}
