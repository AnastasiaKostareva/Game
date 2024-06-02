using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int SceneIndex;
    void Update()
    {
        if (Input.anyKey) SceneManager.LoadScene(SceneIndex, LoadSceneMode.Single);
    }
}
