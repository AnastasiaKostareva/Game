using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineChest : MonoBehaviour
{
    public Entity player;
    private int boostHP = 2;
    public int requireddistance = 3;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
    }
    
    void Update()
    {
        var playerPos = player.transform.position - transform.position;
        if (HelpTool.FindDistance(gameObject, GameObject.FindGameObjectWithTag("Player")) <= requireddistance)
        {
            if (player.maxHp - player.hp > 0)
            {
                if (player.maxHp - player.hp >= 2) player.hp += boostHP;
                else player.hp = player.maxHp;
                Destroy(gameObject);
            }
        }
    }
}