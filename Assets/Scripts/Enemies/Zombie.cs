using UnityEngine;

public class Zombie : Enemy
{
    bool grounded = false, lookingRight = true;
    [SerializeField]
    float acceleration, maxSpeed, directionChangeTime;
    float directionChangeTimer=0;

    public override void Routine()
    {
        base.Routine();
        //Zombie only does his regualr routine if it is grounded.
        if (grounded)
            Walk();
    }
    /// <summary>
    /// Zombie moves left for directionChangeTime seconds, then moves right for the sime amount of seconds.
    /// </summary>
    private void Walk()
    {
        directionChangeTimer += Time.deltaTime;
        if (lookingRight)
        {
            if (directionChangeTimer < directionChangeTime)
            {
                //Increase speed to the right if maxSpeed has not been reached yet.
                if (myRB.velocity.x < maxSpeed)
                {
                    myRB.AddForce(Vector2.right * Time.deltaTime * acceleration);
                }
            }
            else
            {
                //Change facing and direction after interval directionChangeTime is over.
                this.transform.localScale = new Vector3(1, 1, 1);
                directionChangeTimer = 0;
                lookingRight = false;
            }
        }
        else
        {
            if (directionChangeTimer < directionChangeTime)
            {
                //Increase speed to the right if maxSpeed has not been reached yet.
                if (myRB.velocity.x > maxSpeed * -1)
                {
                    myRB.AddForce(Vector2.left * Time.deltaTime * acceleration);
                }
            }
            else
            {
                //Change facing and direction after interval directionChangeTime is over.
                this.transform.localScale = new Vector3(-1, 1, 1);
                directionChangeTimer = 0;
                lookingRight = true;
            }
        }
    }
    public override void Respawn()
    {
        base.Respawn();
        directionChangeTimer = 0;
        if (!lookingRight)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
            lookingRight = true;
        }

    }
    //Check if Zombie is touching the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            grounded = true;
        }
    }
    //Check if Zombie is leaving the ground
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            grounded = false;
        }
    }
}
