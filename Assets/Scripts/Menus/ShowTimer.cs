using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowTimer : MonoBehaviour
{
    [SerializeField]
    SpeedrunTimer myTimer;

    [SerializeField]
    TextMeshProUGUI myTextField;

    // Shows the timer in the EndScene
    void Start()
    {
        myTextField.text = ($"{myTimer.Time}");
    }


}
