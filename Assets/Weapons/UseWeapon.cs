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
    private AudioClip shootAudio;

    private void Awake()
    {
        pause = FindObjectOfType<PauseMenu>();
        shootAudio = Resources.Load<AudioClip>("shoot_sound");
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
                    var audioObject = createSound(out var audioSource);

                    // Уничтожаем объект для аудио после проигрывания звука
                    Destroy(audioObject, audioSource.clip.length);
                    timeBetweenShots = delay;
                    countBullet -= 1;
                }
            }
            else timeBetweenShots -= Time.deltaTime;
        }
    }

    private GameObject createSound(out AudioSource audioSource)
    {
        GameObject audioObject = new GameObject("shootAudio");
        audioObject.transform.position = new Vector3(-1000, -1000);
        audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = shootAudio;
        audioSource.volume = 0.12f;
        audioSource.Play();
        return audioObject;
    }
}