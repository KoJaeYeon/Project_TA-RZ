using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Portal_Boss : MonoBehaviour
{
    #region InJect
    [Inject] private UIEvent _uiEvent;
    #endregion;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();

            if(player != null)
            {
                SceneManager.LoadScene(1);
            }
            
            this.gameObject.SetActive(false);
        }
    }
}
