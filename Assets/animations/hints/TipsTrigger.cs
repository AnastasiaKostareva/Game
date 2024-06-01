using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsTrigger : MonoBehaviour
{
    [Header("Тект подсказки")]
    [TextArea(3, 10)]
    [SerializeField] private string message;

    [SerializeField] private float triggerDistance = 2; // Расстояние для срабатывания
    private Player player;
    private bool isPlayerInRange = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= triggerDistance && !isPlayerInRange)
        {
            hint.displayTipEvent?.Invoke(message);
            isPlayerInRange = true;
        }
        else if (distance > triggerDistance && isPlayerInRange)
        {
            hint.disableTipEvent?.Invoke();
            isPlayerInRange = false;
        }
    }
}
