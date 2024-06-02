using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int SceneIndex;
    private float timeBeforeSkip = 1f;
    void Update()
    {
        if (Input.anyKey && timeBeforeSkip <= 0) SceneManager.LoadScene(SceneIndex, LoadSceneMode.Single);
        timeBeforeSkip -= Time.deltaTime;
    }
}
