using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class Ctrelok : MonoBehaviour
{
    private GameObject[] possiblePos;
    private GameObject player;
    private GameObject gun;
    private Rigidbody2D body;
    public float teleportTime = 0.3f;
    private bool isTriggered = false;
    public GameObject bullet;
    private float timeBetweenShots = 3f;
    public Transform shootPos;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        body = gameObject.GetComponent<Rigidbody2D>();
        possiblePos = GameObject.FindGameObjectsWithTag("ctrel0kPos");
        gun = HelpTool.FindNearestGameObject("EnemyGun", gameObject);
    }

    void Update()
    {
        var playerPos = player.transform.position;
        var gunRender = gun.GetComponent<SpriteRenderer>();
        if (playerPos.x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            gunRender.flipY = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            gunRender.flipY = true;
        }
        
        Shoot(playerPos);
        Teleport(playerPos);
    }

    private void Teleport(Vector3 playerPos)
    {
        var inVisibleRadius = HelpTool.FindDistance(player, gameObject);
        if (isTriggered || inVisibleRadius <= 5)
        {
            isTriggered = true;
            teleportTime -= Time.deltaTime;
            if (teleportTime <= 0)
            {
                if (possiblePos.Length > 0)
                {
                    Console.WriteLine(possiblePos.Length);
                    var nextPos = FindNextPos(playerPos);
                    transform.position = nextPos.transform.position;
                }
                isTriggered = false;
                teleportTime = 0.3f;
            }
        }
    }

    private GameObject FindNextPos(Vector3 playerPos)
    {
        var found = false;
        GameObject nextPos = default;
        while (!found)
        {
            nextPos = possiblePos[new Random().Next(possiblePos.Length)];
            if (HelpTool.FindDistance(nextPos, player) > 5) found = true;
        }

        return nextPos;
    }

    private void Shoot(Vector3 playerPos)
    {
        var direction = playerPos - transform.position;
        var koef = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0, 0, koef);
        
        if (timeBetweenShots <= 0)
        {
            Instantiate(bullet, shootPos.position, gun.transform.rotation);
            timeBetweenShots = 3f;
        }
        else timeBetweenShots -= Time.deltaTime;
    }
}