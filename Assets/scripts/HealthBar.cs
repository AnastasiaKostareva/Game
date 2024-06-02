using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public Entity player;
    public string thisObjectName;

    void Start()
    {
        healthBar = GetComponent<Image>();
        if (thisObjectName != "" && thisObjectName != null)
            player = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Entity>();
        else
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();  
    }


    void Update()
    {
        var doubleHP = double.Parse(player.hp.ToString());

        healthBar.fillAmount = float.Parse((doubleHP / player.maxHp).ToString());
    }
}
