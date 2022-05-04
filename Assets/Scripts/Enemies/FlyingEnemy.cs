using UnityEngine;

public class FlyingEnemy : Enemy
{
    [SerializeField]
    float speed, floatingCooldown, floatingAcceleration;
    float floatingTimer = 0, floatingSpeed = 0;
    bool floatingStart = true, floatingUp = false,lookingLeft=true;

    [SerializeField]
    GameObject myAnimationParent;
    public override void Routine()
    {
        base.Routine();
        floatingTimer += Time.deltaTime;
        Chase();
    }
    private void Chase()
    {
        //FlyingEnemy always moves directly towards the player.
        Vector2 targetVector = new Vector2(playerM.transform.position.x - this.transform.position.x, playerM.transform.position.y - this.transform.position.y);
        targetVector.Normalize();
        myRB.velocity = Vector2.zero;
        myRB.AddForce(targetVector * speed*Time.deltaTime);
        if (lookingLeft && playerM.transform.position.x > this.transform.position.x)
        {
            myAnimationParent.transform.localScale = new Vector3(-1, 1, 1);
            lookingLeft = false;
        }
        if (!lookingLeft && playerM.transform.position.x < this.transform.position.x)
        {
            myAnimationParent.transform.localScale = new Vector3(1, 1, 1);
            lookingLeft = true;
        }
        //Up/Down-Deviation is added to make up a floating effect.
        if (floatingUp)
        {
            floatingSpeed += floatingAcceleration;
            myRB.AddForce(Vector2.up * floatingSpeed*Time.deltaTime);
            //after cooldown is over, up/down direction is changed.
            if (floatingTimer >= floatingCooldown)
            {
                floatingUp = false;
                floatingTimer = 0;
                floatingSpeed = 0;
            }
        }
        else
        {
            floatingSpeed += floatingAcceleration;
            myRB.AddForce(Vector2.down * floatingSpeed*Time.deltaTime);
            //after cooldown is over, up/down direction is changed.
            if (floatingTimer >= floatingCooldown)
            {
                floatingUp = true;
                floatingTimer = 0;
                floatingSpeed = 0;
            }
        }
    }
    public override void Respawn()
    {
        base.Respawn();
        floatingUp = false;
        floatingTimer = 0;
        floatingSpeed = 0;
        if (!lookingLeft)
        {
            myAnimationParent.transform.localScale = new Vector3(1, 1, 1);
            lookingLeft = true;
        }
    }
}
