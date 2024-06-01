using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public Entity player;

    void Start()
    {
        healthBar = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
    }


    void Update()
    {
        var doubleHP = double.Parse(player.hp.ToString());

        healthBar.fillAmount = float.Parse((doubleHP / player.maxHp).ToString());
    }
}
