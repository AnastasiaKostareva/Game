using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockAudio : MonoBehaviour
{
    public AudioSource player;
    public AudioClip attack;
    public AudioClip death;
    private Ctrelok _cock;
    private Animator animator;
    void Start()
    {
        _cock = gameObject.GetComponent<Ctrelok>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_cock.isShooting)
        {
            player.clip = attack;
            player.Play();
        }

        if (_cock.isDead)
        {
            player.clip = death;
            player.Play();
        }
    }
}
