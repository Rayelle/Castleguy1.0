using TMPro;
using UnityEngine;

public class TutorialMessage : MonoBehaviour
{
    [SerializeField]
    private string message;
    [SerializeField]
    private TextMeshProUGUI myText;
    [SerializeField]
    private TutorialMessageManager myManager;
    [SerializeField]
    private float displayTime;
    private float displayTimer;
    private bool timerActive;
    public float DisplayTime { get => displayTime; }
    public float DisplayTimer { get => displayTimer; set => displayTimer = value; }
    public bool TimerActive { get => timerActive; set => timerActive = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if Player hits trigger, set TutorialMessageManager to display this objects message.
        if (collision.gameObject.layer == 8)
        {
            myText.text = message;
            timerActive = true;
            myManager.MyMessage = this;
        }
    }
    /// <summary>
    /// Sets message to be the empty string.
    /// </summary>
    public void RemoveMessage()
    {
        myText.text = "";
    }
}
