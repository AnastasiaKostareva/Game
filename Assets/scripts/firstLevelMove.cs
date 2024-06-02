using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class firstLevelMove : MonoBehaviour
{
    public int SceneIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (SceneIndex - 1 == 2 && other.tag == "Player")
        {
            if (CounterMonsters.instance.monsterCount == 0)
                SceneManager.LoadScene(SceneIndex, LoadSceneMode.Single);
            else
                CounterMonsters.instance.UpdateCounterText("Найди всех монстров");
        } 
        else if (other.tag == "Player")
        {
            SceneManager.LoadScene(SceneIndex, LoadSceneMode.Single);
        }
    }
}
