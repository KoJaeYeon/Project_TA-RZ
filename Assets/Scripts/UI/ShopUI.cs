using UnityEngine;
using Zenject;

public class ShopUI : MonoBehaviour
{
    [Inject] UIEvent UIEvent;
    private void OnEnable()
    {
        UIEvent.SetActivePlayerControl(false);
    }

    private void OnDisable()
    {
        UIEvent.SetActivePlayerControl(true);
    }

    public void DeActiveBlueChipUI()
    {
        gameObject.SetActive(false);
    }
}
