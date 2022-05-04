using UnityEngine;

public class HealthPotion : MonoBehaviour
{

    [SerializeField]
    PotionUI myUI;

    public void OnTriggerEnter2D(Collider2D col)
    {
        //when Player was hit give him his Health Potion back

        if (col.gameObject.layer == 8 && !PlayerData.HealthPotion)
        {
            PlayerData.HealthPotion = true;
            Destroy(this.gameObject);
            myUI.UpdatePotion();
        }
    }



}
