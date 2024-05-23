using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseWeapon : MonoBehaviour
{
    public Transform shootPos;
    public GameObject bullet;
    public float delay;
    private float timeBetweenShots;
    public int countBullet = 10;
    private PauseMenu pause;

    private void Awake()
    {
        pause = FindObjectOfType<PauseMenu>();
    }

    private void RotateGun()
    {
        var mouseCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var coefficent = Mathf.Atan2(mouseCoordinates.y, mouseCoordinates.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, coefficent);
    }

    void Update()
    {
        if (!pause.PauseGame)
        {
            RotateGun();
            if (timeBetweenShots <= 0)
            {
                if (Input.GetMouseButton(0) && countBullet > 0)
                {
                    Instantiate(bullet, shootPos.position, transform.rotation);
                    timeBetweenShots = delay;
                    countBullet -= 1;
                }
            }
            else timeBetweenShots -= Time.deltaTime;
        }
    }
}