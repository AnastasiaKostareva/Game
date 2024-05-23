using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestWithBullet : MonoBehaviour
{
    public Player player;
    private int boostBullet = 10;
    public double requireddistance = 0.8;
    public UseWeapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        weapon = GameObject.FindGameObjectWithTag("HeroGun").GetComponent<UseWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null && HelpTool.FindDistance(gameObject, GameObject.FindGameObjectWithTag("Player")) <= requireddistance)
        {
            weapon.countBullet += boostBullet;
            Destroy(gameObject);
        }
    }
}
