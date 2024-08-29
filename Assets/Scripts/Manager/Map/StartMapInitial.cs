using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StartMapInitial : MonoBehaviour
{
    [Inject]
    MapManager _mapManager;

    [SerializeField] Stage stage;
    // Start is called before the first frame update
    void Start()
    {
        stage.gameObject.SetActive(true);
        stage.StartStage(StageType.Elite);
    }
}
