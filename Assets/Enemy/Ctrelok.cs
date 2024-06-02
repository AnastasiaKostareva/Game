using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class Ctrelok : MonoBehaviour
{
    private GameObject[] possiblePos;

    public GameObject player;

    public bool isShooting;
    public bool isDead;

    private bool canShoot;
    //private GameObject gun;
    private Rigidbody2D body;
    public float teleportTime = 0.3f;
    private bool isTriggered = false;
    public GameObject bullet;
    private float timeBetweenShots = 1.5f;
    private float shootTimePrepare = 0.75f;
    public Transform shootPos;
    public bool ShouldDropKey;
    public GameObject Key;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        body = gameObject.GetComponent<Rigidbody2D>();
        possiblePos = GameObject.FindGameObjectsWithTag("ctrel0kPos");
        //gun = HelpTool.FindNearestGameObject("EnemyGun", gameObject);
    }

    void Update()
    {
        if (isDead) return;
        var playerPos = player.transform.position;
        var diffPos = playerPos - transform.position;
        if (HelpTool.FindDistance(player, gameObject) >= 15f) return;
        GetComponent<SpriteRenderer>().flipX = !(playerPos.x > transform.position.x);
        Teleport(playerPos);
        if (!isShooting && timeBetweenShots < 0) 
            StartCoroutine(AttackCoroutine());
        else timeBetweenShots -= Time.deltaTime;
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
    

    private IEnumerator AttackCoroutine()
    {
        isShooting = true;
        var timer = 0f;
        while (timer < shootTimePrepare)
        {
            timer += Time.deltaTime;
            var distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer > 15f)
            {
                isShooting = false;
                timeBetweenShots = 1.5f;
                yield break;
            }
            yield return null;
        }
        var direction = player.transform.position - transform.position;
        var koef = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        shootPos.transform.rotation = Quaternion.Euler(0, 0, koef);
        if (!isDead) Instantiate(bullet, shootPos.position, shootPos.transform.rotation);
        timeBetweenShots = 1.5f;
        isShooting = false;
    }

    private void OnDestroy()
    {
        if (ShouldDropKey) Instantiate(Key, transform.position, transform.rotation);
    }
}