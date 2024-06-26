using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = System.Random;
public class Boss : MonoBehaviour
{
    public bool isTriggered;
    public bool isShooting;
    public bool isTeleporting;
    public bool onRage;
    private bool _isFlaming;
    [FormerlySerializedAs("_isDead")] public bool isDead;
    private GameObject player;
    private Rigidbody2D body;
    public float speed;
    public float chargeSpeed;
    private float walkCooldown;
    private bool forwalk;
    public int chargeCount;
    private float dashCooldown = 5f;
    private actions lastAction = actions.shoot;
    private GameObject[] possiblePos;
    private float tpCooldown;
    private int tpCount;
    public GameObject bullet;
    public Transform shootPos;
    private float shootCooldown;
    private Entity self;
    public GameObject Centre;
    private bool atCentre;
    private GameObject[] rageShootPos;
    private float rotation;
    private float invincibleTime = 19f;
    private int maxDash = 3;

    public AudioSource playa;
    public AudioClip chargeSound;
    public AudioClip teleportSound;
    public AudioClip shootSound;
    public AudioClip rageSound;
    public AudioClip flameRageSound;
    public AudioClip deathSound;
    public bool _isPlayingAttackAnimation;
    private Entity playerEntity;

    private enum actions
    {
        charge,
        shoot,
        teleport
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerEntity = player.GetComponent<Entity>();
        body = gameObject.GetComponent<Rigidbody2D>();
        possiblePos = GameObject.FindGameObjectsWithTag("ctrel0kPos");
        self = gameObject.GetComponent<Entity>();
        rageShootPos = GameObject.FindGameObjectsWithTag("BossRageShootPos");
        var koef = 45f;
        for (var i = 0; i < rageShootPos.Length; i++)
        {
            rageShootPos[i].transform.rotation = Quaternion.Euler(0, 0, koef);
            koef += 90f;
        }

        playa = gameObject.GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        if (self.hp < 0)
        {
            if (!isDead)
            {
                StartCoroutine(SuddenDance());
            }
            return;
        }
        // if (onRage)
        // {
        //     StartCoroutine(PlayRangeAttackCoroutine(4.5f, rageSound));
        // }
        // if (playa.clip == rageSound && playa.isPlaying) Debug.Log("31312312123213");
        if (self.hp >= 150) AttackBeforeRage();
        else if (invincibleTime >= 0)
        {
            if (!atCentre)
            {
                transform.position = Centre.transform.position;
                atCentre = true;
                StartCoroutine(PlayRangeAttackCoroutine(0.5f, teleportSound));
                
                Centre.GetComponent<BoxCollider2D>().enabled = true;
                body.velocity = Vector2.zero;
            }

            if (!onRage) StartCoroutine(PlayRangeAttackCoroutine(4f, rageSound));
            onRage = true;
            if (invincibleTime <= 14.8f)
            {
                if (!_isFlaming)
                {
                    Debug.Log("12312323123");
                    playa.Stop();
                    playa.clip = flameRageSound;
                    playa.Play();
                }

                _isFlaming = true;
                Rage();
            }
            else invincibleTime -= Time.deltaTime;
        }
        else
        {
            _isFlaming = false;
            onRage = false;
            if (atCentre)
            {
                atCentre = false;
                Centre.GetComponent<BoxCollider2D>().enabled = false;
                maxDash = 5;
            }

            AttackBeforeRage();
        }

        dashCooldown -= Time.deltaTime;
        if (transform.position.x < player.transform.position.x)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

    void Charge()
    {
        if (chargeCount <= maxDash)
        {
            if (walkCooldown <= 0)
            {
                body.velocity = (player.transform.position - transform.position).normalized * chargeSpeed;

                //
                PlayWithDistanceVolume(chargeSound);
                chargeCount++;
                walkCooldown = 1f;
            }

            walkCooldown -= Time.deltaTime;
        }
        else
        {
            dashCooldown = 1.5f;
            body.velocity = Vector2.zero;
            isTriggered = false;
            chargeCount = 0;
            lastAction = actions.charge;
        }
    }

    void Teleport()
    {
        var length = possiblePos.Length;
        if (tpCooldown <= 0)
        {
            isTeleporting = true;
            isShooting = false;
            tpCooldown = 1.5f;
            while (true)
            {
                var nextPos = possiblePos[new Random().Next(length)];
                if (HelpTool.FindDistance(nextPos, player) >= 15f)
                {
                    transform.position = nextPos.transform.position;
                    PlayWithDistanceVolume(teleportSound);
                    break;
                }
            }

            tpCount++;
            if (tpCount > 5) isTeleporting = false;
        }
        else if (shootCooldown <= 0)
        {
            isTeleporting = false;
            isShooting = true;
            // if (isShooting)
            // {
            //     playa.clip = shootSound;
            //     playa.Play();
            // }
            ShootAfterTP();
            shootCooldown = 0.1f;
        }

        shootCooldown -= Time.deltaTime;

        tpCooldown -= Time.deltaTime;
    }

    void ShootAfterTP()
    {
        if (!_isPlayingAttackAnimation)
        {
            StartCoroutine(PlayRangeAttackCoroutine(1.5f, shootSound));
        }

        var direction = player.transform.position - transform.position;
        var koef = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        shootPos.rotation = Quaternion.Euler(0, 0, koef);
        Instantiate(bullet, shootPos.position, shootPos.rotation);
    }

    void AttackBeforeRage()
    {
        if (isTriggered && Vector2.Distance(transform.position, player.transform.position) < 3f)
        {
            playerEntity.TakeDamage(2, 0.5f);
        }

        if (lastAction == actions.charge && dashCooldown <= 0)
        {
            if (tpCount <= 5)
                Teleport();
            else
            {
                isShooting = false;
                lastAction = actions.teleport;
                tpCount = 0;
            }
        }
        else if ((HelpTool.FindDistance(gameObject, player) >= 10 && !isTriggered) && lastAction != actions.charge)
        {
            speed += Time.deltaTime * 10;
            speed = Math.Min(speed, 12);
            body.velocity = (player.transform.position - transform.position).normalized * speed;
        }
        else if (dashCooldown <= 0)
        {
            isTriggered = true;
            if (!isTriggered) body.velocity = Vector2.zero;
            Charge();
        }
    }

    void Rage()
    {
        if (shootCooldown <= 0)
        {
            shootCooldown = 0.07f;
            foreach (var pos in rageShootPos)
            {
                Instantiate(bullet, pos.transform.position, pos.transform.rotation);
            }
        }

        invincibleTime -= Time.deltaTime;
        shootCooldown -= Time.deltaTime;
        shootPos.rotation = Quaternion.Euler(0, 0, rotation);
        rotation += 1f;
    }

    private IEnumerator PlayRangeAttackCoroutine(float time, AudioClip sound)
    {
        _isPlayingAttackAnimation = true;
        PlayWithDistanceVolume(sound);

        yield return new WaitForSeconds(time);

        _isPlayingAttackAnimation = false;
    }

    private void PlayWithDistanceVolume(AudioClip sound)
    {
        playa.clip = sound;
        playa.volume = 1f - Vector2.Distance(transform.position, player.transform.position) / 40;
        playa.Play();
    }

    private IEnumerator SuddenDance()
    {
        isDead = true;
        playa.Stop();
        playa.clip = deathSound;
        playa.Play();
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(10, LoadSceneMode.Single);
        Destroy(gameObject);
    }
    
}