using UnityEngine;
using UnityEngine.EventSystems;

public class UISelectNullDefence : MonoBehaviour
{
    [SerializeField] GameObject SelectedGameObject;
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(SelectedGameObject);
        }
        else
        {
            SelectedGameObject = EventSystem.current.currentSelectedGameObject;
        }
    }
}
