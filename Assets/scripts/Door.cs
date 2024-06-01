using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameObject playerAsObj;
    public GameObject exit;

    void Start()
    {
        playerAsObj = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var player = playerAsObj.GetComponent<Player>();
        if (HelpTool.FindDistance(gameObject, playerAsObj) <= 0.5f && player.keyCount > 0)
        {
            player.interactionKey.GetComponent<SpriteRenderer>().enabled = true;
            if (Input.GetKeyUp(KeyCode.E)) playerAsObj.transform.position = exit.transform.position;
        }
        else player.interactionKey.GetComponent<SpriteRenderer>().enabled = false;
    }
}