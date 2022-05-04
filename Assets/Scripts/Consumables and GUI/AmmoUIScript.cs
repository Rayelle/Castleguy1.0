using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUIScript : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI ammoCount;

    private void Start()
    {
        UpdateAmmo();
    }


    public void UpdateAmmo()
    {
        ammoCount.text = ("x" + PlayerData.Ammo);
    }

}
