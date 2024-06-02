using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    private AudioSource player;

    private void Awake()
    {
        player = GameObject.Find("BulletAudio").GetComponent<AudioSource>();
        player.Play();
    }

    void Update()
    {
        var hit = Physics2D.Raycast(transform.position, transform.right, distance, whatIsSolid);
        if (hit.collider is not null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Entity>().TakeDamage(damage,0.3f);
            }
            else if (hit.collider.CompareTag("Player"))
            {
                var player = hit.collider.GetComponent<Entity>();
                player.TakeDamage(damage,1f);
            }
            Destroy(gameObject);
        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}