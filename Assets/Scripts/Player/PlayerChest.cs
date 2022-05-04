using UnityEngine;


public class PlayerChest : MonoBehaviour
{
    [SerializeField]
    PlayerController myPC;

    [SerializeField]
    Rigidbody2D myRB;

    //keeps track of player-chest-collider hitting a wall to enable walljump.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            myPC.setOnWall(true);

        }
    }
    //keeps track of player-chest-collider leaving a wall to disable walljump.
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            myPC.setOnWall(false);

        }
    }
}
