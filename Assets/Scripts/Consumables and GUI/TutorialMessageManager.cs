using UnityEngine;
/// <summary>
/// Tutorial Message Manager keeps track of current tutorial message and turns it off after a certain time.
/// </summary>
public class TutorialMessageManager : MonoBehaviour
{
    private TutorialMessage myMessage;

    public TutorialMessage MyMessage { set => myMessage = value; }

    private void Update()
    {
        if (myMessage != null)
        {
            if (myMessage.TimerActive)
            {
                //after interval is over, call RemoveMessage().
                myMessage.DisplayTimer += Time.deltaTime;
                if (myMessage.DisplayTimer >= myMessage.DisplayTime)
                {
                    myMessage.TimerActive = false;
                    myMessage.DisplayTimer = 0;
                    myMessage.RemoveMessage();
                }
            }
        }
    }
}
