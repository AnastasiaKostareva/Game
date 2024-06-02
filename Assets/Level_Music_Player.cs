using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Music_Player : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource _source;
    void Start()
    {
        _source = gameObject.GetComponent<AudioSource>();
        _source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
