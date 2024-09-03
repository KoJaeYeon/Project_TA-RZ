using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

public class ShopUI : MonoBehaviour
{
    [Inject] UIEvent UIEvent;
    [SerializeField] InputActionReference cancelAction;
    [SerializeField] GameObject initial_Select_GameObject;
    [SerializeField] GameObject Frame;
    private void OnEnable()
    {
        cancelAction.action.Enable();
        cancelAction.action.performed += OnCancel;
        UIEvent.SetActivePlayerControl(false);

        EventSystem.current.SetSelectedGameObject(initial_Select_GameObject);
        ShopUIRenew();
    }

    private void OnDisable()
    {
        cancelAction.action.performed -= OnCancel;
        cancelAction.action.Disable();
        UIEvent.SetActivePlayerControl(true);
    }

    private void Update()
    {
        Frame.transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
    }

    void ShopUIRenew()
    {

    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        UIEvent.DeActiveShopUI();
    }

    public void DeActiveShopUI()
    {
        UIEvent.DeActiveShopUI();
    }
}
