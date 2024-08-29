using UnityEngine;
using Zenject;

public class UILookPlayer : MonoBehaviour
{
    [Inject] Player _player;
    Transform _cameraTrans;
    private void Awake()
    {
        _cameraTrans = _player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_cameraTrans);
        transform.Rotate(0, 180, 0);
    }
}
