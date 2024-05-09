using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;

    void Update()
    {
        var hit = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hit.collider is not null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Entity>().TakeDamage(damage);
            }
            else if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<Player>().hp -= damage;
            }
            Destroy(gameObject);
        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}