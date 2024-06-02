using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterMonsters : MonoBehaviour
{
    public static CounterMonsters instance;
    public TextMeshProUGUI counterText; // Ссылка на текст для отображения количества монстров
    public int monsterCount; // Количество монстров

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        counterText = GetComponent<TextMeshProUGUI>();
        //monsterCount = 0;
        UpdateCounterText();
    }

    public void AddMonster()
    {
        monsterCount++;
        UpdateCounterText();
    }

    public void RemoveMonster()
    {
        monsterCount--;
        UpdateCounterText();
    }

    public void UpdateCounterText(string text = null)
    {
        if (text == null)
            counterText.text = "Монстров: " + monsterCount;
        else
            counterText.text = text;
    }
}
