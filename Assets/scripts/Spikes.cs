using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spikes : MonoBehaviour
{
    private GameObject player;
    private float coolDown;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("legs");
    }
    
    void Update()
    {
        coolDown -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (coolDown <= 0) other.GetComponent<Player>().hp -= 1;
    }
}
