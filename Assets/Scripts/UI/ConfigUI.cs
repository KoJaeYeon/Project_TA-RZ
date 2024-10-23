using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ConfigUI : MonoBehaviour
{
    [Inject] Player Player;
    [SerializeField] Image[] images;

    private void OnEnable()
    {
        for(int i = 0; i < images.Length; i++)
        {
            if(i == Player.SavePlayerData.mouseIndex)
            {
                images[i].color = new Color(200 / 255f, 177 / 255f, 15 / 255f);
            }
            else
            {
                images[i].color = new Color(100 / 255f, 100 / 255f, 100 / 255f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && Player.SavePlayerData.mouseIndex > 0)
        {
            images[Player.SavePlayerData.mouseIndex--].color = new Color(100 / 255f, 100 / 255f, 100 / 255f);
            images[Player.SavePlayerData.mouseIndex].color = new Color(200 / 255f, 177 / 255f, 15 / 255f);
            
            if(Player.SavePlayerData.mouseIndex == 0)
            {
                Player.Rotate_Camera_Speed = 0.1f;
            }
            else
            {
                Player.Rotate_Camera_Speed = 0.25f * Player.SavePlayerData.mouseIndex;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) && Player.SavePlayerData.mouseIndex < 4)
        {
            images[Player.SavePlayerData.mouseIndex++].color = new Color(100 / 255f, 100 / 255f, 100 / 255f);
            images[Player.SavePlayerData.mouseIndex].color = new Color(200 / 255f, 177 / 255f, 15 / 255f);
            Player.Rotate_Camera_Speed = 0.25f * Player.SavePlayerData.mouseIndex;
        }
    }
}
