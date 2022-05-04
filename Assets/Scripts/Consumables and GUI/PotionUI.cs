using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PotionUI : MonoBehaviour
{
    [SerializeField]
    private Sprite fullPotion, emptyPotion;

    [SerializeField]
    private Image potionImage;

    private void Start()
    {
        UpdatePotion();
    }


    public void UpdatePotion()
    {
        if (PlayerData.HealthPotion == true)
        {
            potionImage.sprite = fullPotion;
        }
        else
        {
            potionImage.sprite = emptyPotion;
        }
    }

}
