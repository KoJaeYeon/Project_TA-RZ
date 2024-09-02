using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class ShopUI : MonoBehaviour
{
    [Inject] UIEvent UIEvent;
    [SerializeField] InputActionReference cancelAction;
    private void OnEnable()
    {
        cancelAction.action.Enable();
        cancelAction.action.performed += OnCancel;
        UIEvent.SetActivePlayerControl(false);

        ShopUIRenew();
    }

    private void OnDisable()
    {
        cancelAction.action.performed -= OnCancel;
        cancelAction.action.Disable();
        UIEvent.SetActivePlayerControl(true);
    }

    void ShopUIRenew()
    {

    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        gameObject.SetActive(false);
    }

    public void DeActiveShopUI()
    {
        gameObject.SetActive(false);
    }
}
