using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Script : EnemyScript
{
    public GameObject player;
    private Transform playerTransform;
    private Transform _transform;
    private Rigidbody2D _rbody;
    public GameObject laser;
    public GameObject laserAnim;
    public GameObject chaser;
    public GameObject turret;
    public GameObject pathfinder;

    public int maxHealth;
    public double cooldown;
    private bool rotate;
    private bool firing;
    private float chargeTime;
    private bool calledOnce;

    public GameObject _releaseWall; // Releases Player upon Victory

    private Animator _ani; // Animation of Boss

    // Start is called before the first frame update
    void Start()
    {

        // set line renderer equal to raycast
        // set laser animation to line renderer
        // make laser do damage


        gsmScript = GameObject.FindGameObjectWithTag(CodeLibrary.SceneManager).GetComponent<GSMScript>();
        player = GameObject.FindGameObjectWithTag(CodeLibrary.player);
        playerTransform = player.transform;
        _transform = transform;
        _rbody = GetComponent<Rigidbody2D>();
        maxHealth = 200;

        int playerEnergy = gsmScript.playerStats.currency;
        if (playerEnergy > 100)
        {
            maxHealth += (playerEnergy - 100);
        }
        health = maxHealth;


        damage = damage * gsmScript.monsterDifficulty;

        laserAnim.SetActive(false);
        cooldown = 4.0;
        rotate = true;
        firing = false;
        chargeTime = 2;
        calledOnce = true;

       _ani = GetComponent<Animator>();
       _ani.SetBool("hasDied", false);

        StartCoroutine(spawnEnemies());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hasDied();
        _rbody.velocity = new Vector2(0, 0);

        if ((health * 2 < maxHealth) && calledOnce)
        {
            // if below 50% health, spawn enemies
            GameObject turret1 = Instantiate(turret, new Vector2(-8, 32), Quaternion.identity);
            GameObject turret2 = Instantiate(turret, new Vector2(8, 48), Quaternion.identity);
            calledOnce = false;
        }

        if (rotate)
        {
            Vector2 closeness = (playerTransform.position - _transform.position);
            float angle = Mathf.Atan2(closeness.y, closeness.x) * Mathf.Rad2Deg;
            float rotateSpeed = 4.5f * Time.deltaTime; // speed of rotation
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            _rbody.transform.rotation = Quaternion.Slerp(_rbody.transform.rotation, rotation, rotateSpeed);
        }

        
       // shoot laser
       if (firing == false)
        {
            StartCoroutine(fireLaser());
        }
    }

    public void hasDied()
    {
        if (health <= 0)
        {
            _ani.SetBool("hasDied", true);
            StartCoroutine(deathAni());
        }
    }

    private IEnumerator deathAni()
    {
        yield return new WaitForSeconds(2);
        _releaseWall.SetActive(false);
        gsmScript.lootEnemy(-3);
        Destroy(gameObject);
    }

    private IEnumerator fireLaser()
    {
        firing = true;
        _ani.SetBool("Preparing", true);
        yield return new WaitForSeconds(chargeTime - 0.25f);
        rotate = false;
        yield return new WaitForSeconds(0.25f);
        _ani.SetBool("Preparing", false);

        _ani.SetBool("Firing", true);
        laserAnim.SetActive(true);
        Quaternion rotation = _transform.rotation;
        Instantiate(laser, new Vector2(_transform.position.x, _transform.position.y), Quaternion.identity);

        yield return new WaitForSeconds(2);
        laserAnim.SetActive(false);
        _ani.SetBool("Firing", false);
        rotate = true;

        yield return new WaitForSeconds(2);
        firing = false;
    }

    private IEnumerator spawnEnemies()
    {
        while (true)
        {
            spawnChasers();
            yield return new WaitForSeconds(20);
            spawnPathfinders();
            yield return new WaitForSeconds(20);
        }
    }

    private void spawnChasers()
    {
        GameObject chaser1 = Instantiate(chaser, new Vector2(0, 35), Quaternion.identity);
        chaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
        GameObject chaser2 = Instantiate(chaser, new Vector2(0, 45), Quaternion.identity);
        chaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
    }

    private void spawnPathfinders()
    {
        GameObject path1 = Instantiate(pathfinder, new Vector2(-10, 40), Quaternion.identity);
        GameObject path2 = Instantiate(pathfinder, new Vector2(10, 40), Quaternion.identity);
    }

    override
    public void handleHit()
    {
        gsmScript.playEnemyHitSound();
    }
}
