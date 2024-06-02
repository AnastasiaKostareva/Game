using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CockAudio : MonoBehaviour
{
    [FormerlySerializedAs("player")] public AudioSource playa;
    public AudioClip attack;
    public AudioClip death;
    private Ctrelok _cock;
    private Animator animator;
    void Start()
    {
        _cock = gameObject.GetComponent<Ctrelok>();
        animator = gameObject.GetComponentInChildren<Animator>();
        playa = gameObject.GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_cock.isShooting)
        {
            playa.clip = attack;
            playa.Play();
        }

        if (_cock.isDead)
        {
            playa.clip = death;
            playa.Play();
        }
    }
}
