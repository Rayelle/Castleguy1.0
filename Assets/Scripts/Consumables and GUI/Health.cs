using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    //private int  numOfHearts = 10;

    [SerializeField]
    private Image[] hearts;

    [SerializeField]
    private Sprite fullHeart, emptyHeart;

    public void UpdateHealthPoints()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < PlayerData.Healthpoints)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
