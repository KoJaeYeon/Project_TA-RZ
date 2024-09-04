using UnityEngine;
using UnityEngine.EventSystems;

public class EnableChoiceUI : MonoBehaviour
{
    [SerializeField] private GameObject _ui;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_ui);
    }
}
