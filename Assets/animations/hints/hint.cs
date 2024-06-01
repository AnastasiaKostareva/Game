using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class hint : MonoBehaviour
{
    public static Action<string> displayTipEvent; //������� ��������� ��������� �� �������
    public static Action disableTipEvent; //��������� ���������

    [SerializeField] private TMP_Text textHint; //����� ���������

    private Animator animator;
    private int activeTips; //���������� �������� ���������

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
