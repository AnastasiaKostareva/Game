using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class firstLevelMove : MonoBehaviour
{
    public int SceneIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(SceneIndex, LoadSceneMode.Single);
        }
    }
}
