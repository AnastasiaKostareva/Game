using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private GameObject playerAsObj;
    public GameObject exit;
    public int SceneIndex;

    void Start()
    {
        playerAsObj = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        var player = playerAsObj.GetComponent<Player>();
        if (HelpTool.FindDistance(gameObject, playerAsObj) <= 2f && player.keyCount > 0)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (SceneIndex != 0) SceneManager.LoadScene(SceneIndex, LoadSceneMode.Single);
                else
                {
                    playerAsObj.transform.position = exit.transform.position;
                    player.keyCount--;
                    Destroy(gameObject);
                }
            }
        }
    }
}