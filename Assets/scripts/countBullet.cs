using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CountBullet : MonoBehaviour
{
    public TextMeshProUGUI text;
    public UseWeapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        weapon = FindObjectOfType<UseWeapon> ();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Патроны: " + weapon.countBullet.ToString();
    }
}
