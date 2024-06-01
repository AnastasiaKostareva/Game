using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class hint : MonoBehaviour
{
    public static Action<string> displayTipEvent; //делегат появления подсказки со строкой
    public static Action disableTipEvent; //исчезание подсказки

    [SerializeField] private TMP_Text textHint; //текст подсказки

    private Animator animator;
    private int activeTips; //количество активных подсказок

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        displayTipEvent += DisplayTip;
        disableTipEvent += DisableTip;
    }

    private void OnDisable()
    {
        displayTipEvent -= DisplayTip;
        disableTipEvent -= DisableTip;
    }

    void DisplayTip(string tip)
    {
        textHint.text = tip;
        animator.SetInteger("State", ++activeTips);
    }

    void DisableTip()
    {
        animator.SetInteger("State", --activeTips);
    }
}
