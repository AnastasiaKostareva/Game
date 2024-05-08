using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public Player player;

    void Start()
    {
        healthBar = GetComponent<Image>();
        player = FindObjectOfType<Player>();
    }


    void Update()
    {
        var doubleHP = double.Parse(player.hp.ToString());

        healthBar.fillAmount = float.Parse((doubleHP / player.maxHp).ToString());
    }
}
