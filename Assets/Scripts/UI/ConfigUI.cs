using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ConfigUI : MonoBehaviour
{
    [Inject] Player Player;
    int index = 2;
    [SerializeField] Image[] images;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && index > 0)
        {
            images[index--].color = new Color(100 / 255f, 100 / 255f, 100 / 255f);
            images[index].color = new Color(200 / 255f, 177 / 255f, 15 / 255f);
            
            if(index == 0)
            {
                Player.Rotate_Camera_Speed = 0.1f;
            }
            else
            {
                Player.Rotate_Camera_Speed = 0.25f * index;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) && index < 4)
        {
            images[index++].color = new Color(100 / 255f, 100 / 255f, 100 / 255f);
            images[index].color = new Color(200 / 255f, 177 / 255f, 15 / 255f);
            Player.Rotate_Camera_Speed = 0.25f * index;
        }
    }
}
