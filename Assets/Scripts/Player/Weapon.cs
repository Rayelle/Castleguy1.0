using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected float damage;
    protected Sprite weaponSprite;
    protected string weaponName;



    public virtual void Attack(bool lookingRight)
    {

    }

}

