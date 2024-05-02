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
            Destroy(gameObject);
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Entity>().TakeDamage(damage);
            }
        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}