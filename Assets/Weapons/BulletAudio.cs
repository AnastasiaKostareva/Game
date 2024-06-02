using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAudio : MonoBehaviour
{
    private Bullet _bullet;
    public AudioClip sound;
    public AudioSource player;
    void Start()
    {
        player.clip = sound;
        player.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
