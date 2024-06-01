using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (HelpTool.FindDistance(player, gameObject) <= 0.5f)
        {
            player.GetComponent<Player>().keyCount++;
            Destroy(gameObject);
        }
    }
}