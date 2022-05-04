using UnityEngine;

public class Ammo : MonoBehaviour
{
    private uint addedAmmo = 5;

    [SerializeField]
    AmmoUIScript myUIScript;

    public void OnTriggerEnter2D(Collider2D col)
    {
        //when Player was hit add Ammo and detroy Object

        if (col.gameObject.layer == 8 & col is CapsuleCollider2D)
        {
            PlayerData.Ammo += addedAmmo;
            myUIScript.UpdateAmmo();
            Destroy(this.gameObject);
        }
    }
}
