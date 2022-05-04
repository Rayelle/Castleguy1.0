using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    [SerializeField]
    PlayerController myPC;
    [SerializeField]
    private Player onePlayer, secondPlayer;


    //keeps track of player-feet-collider hitting the ground to enable jump. Also cancels jumping animation.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            myPC.setAirborne(false);
            onePlayer.MyAnimator.SetBool("IsJumping", false);
            secondPlayer.MyAnimator.SetBool("IsJumping", false);
        }
    }
    //keeps track of player-feet-collider leaving the ground to disable jump.
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            myPC.setAirborne(true);
        }
    }
}
