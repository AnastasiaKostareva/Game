using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineChest : MonoBehaviour
{
    public Player player;
    private int boostHP = 2;
    public int requiredÂistance = 10;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        var gameObject = GameObject.Find("medicineChest");
        var playerPos = player.transform.position - transform.position;
        if (playerPos.x * playerPos.x + playerPos.y * playerPos.y <= requiredÂistance)
        {
            if (player.maxHp - player.hp > 0 && player.maxHp - player.hp >= 2)
                player.hp += boostHP;
            if (player.maxHp - player.hp > 0 && player.maxHp - player.hp < 2)
                player.hp = player.maxHp;
            Destroy(gameObject);
        }
    }
}
