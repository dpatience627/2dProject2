using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : EnemyScript
{

    public double cooldown;
    public int i;
    public float degreesPerSec;
    double distance;
    int speed;

    public int maxHealth;
    public bool calledOnce;

    public GameObject EnemyBullet1;
    public GameObject EnemyBullet3;
    public GameObject Chaser;
    public GameObject _releaseWall;

    private Animator _ani;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0.4;
        i = 0;
        degreesPerSec = 196f;
        distance = 4;
        speed = 6;

        maxHealth = 100;
        calledOnce = true;

        gsmScript = GameObject.FindGameObjectWithTag(CodeLibrary.SceneManager).GetComponent<GSMScript>();
        int playerEnergy = gsmScript.playerStats.currency;
        if(playerEnergy > 100)
        {
            maxHealth += (playerEnergy - 100);
        }

        health = maxHealth;
        _ani = GetComponent<Animator>();
        _ani.SetBool("hasDied", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hasDied();

        if ((health * 2 < maxHealth) && calledOnce)
        {
            // if below 50% health, spawn enemies
            GameObject chaser1 = Instantiate(Chaser, new Vector2(0, 40), Quaternion.identity);
            chaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
            GameObject chaser2 = Instantiate(Chaser, new Vector2(0, 48), Quaternion.identity);
            chaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
            calledOnce = false;
        }
        // spins
        float rotAmount = degreesPerSec * Time.deltaTime;
        float curRot = transform.localRotation.eulerAngles.z;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot + rotAmount));

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else if(!_ani.GetBool("hasDied"))
        {
            if (i % 3 == 0)
            {
                // spawns bullets1 and gives them velocity
                GameObject bullet1 = Instantiate(EnemyBullet3, new Vector2(transform.position.x + Mathf.Cos(curRot) * (float)distance, transform.position.y + Mathf.Sin(curRot) * (float)distance), Quaternion.identity);
                bullet1.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(curRot) * speed, Mathf.Sin(curRot) * speed);
                GameObject bullet2 = Instantiate(EnemyBullet3, new Vector2(transform.position.x + -Mathf.Cos(curRot) * (float)distance, transform.position.y + Mathf.Sin(curRot) * (float)distance), Quaternion.identity);
                bullet2.GetComponent<Rigidbody2D>().velocity = new Vector2(-Mathf.Cos(curRot) * speed, Mathf.Sin(curRot) * speed);
                GameObject bullet3 = Instantiate(EnemyBullet3, new Vector2(transform.position.x + Mathf.Cos(curRot) * (float)distance, transform.position.y + -Mathf.Sin(curRot) * (float)distance), Quaternion.identity);
                bullet3.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(curRot) * speed, -Mathf.Sin(curRot) * speed);
                GameObject bullet4 = Instantiate(EnemyBullet3, new Vector2(transform.position.x + -Mathf.Cos(curRot) * (float)distance, transform.position.y + -Mathf.Sin(curRot) * (float)distance), Quaternion.identity);
                bullet4.GetComponent<Rigidbody2D>().velocity = new Vector2(-Mathf.Cos(curRot) * speed, -Mathf.Sin(curRot) * speed);
                cooldown = 0.4;
            }
            else
            {
                // spawns bullets3 and gives them velocity
                GameObject bullet1 = Instantiate(EnemyBullet1, new Vector2(transform.position.x + Mathf.Cos(curRot) * (float)distance, transform.position.y + Mathf.Sin(curRot) * (float)distance), Quaternion.identity);
                bullet1.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(curRot) * speed, Mathf.Sin(curRot) * speed);
                GameObject bullet2 = Instantiate(EnemyBullet1, new Vector2(transform.position.x + -Mathf.Cos(curRot) * (float)distance, transform.position.y + Mathf.Sin(curRot) * (float)distance), Quaternion.identity);
                bullet2.GetComponent<Rigidbody2D>().velocity = new Vector2(-Mathf.Cos(curRot) * speed, Mathf.Sin(curRot) * speed);
                GameObject bullet3 = Instantiate(EnemyBullet1, new Vector2(transform.position.x + Mathf.Cos(curRot) * (float)distance, transform.position.y + -Mathf.Sin(curRot) * (float)distance), Quaternion.identity);
                bullet3.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(curRot) * speed, -Mathf.Sin(curRot) * speed);
                GameObject bullet4 = Instantiate(EnemyBullet1, new Vector2(transform.position.x + -Mathf.Cos(curRot) * (float)distance, transform.position.y + -Mathf.Sin(curRot) * (float)distance), Quaternion.identity);
                bullet4.GetComponent<Rigidbody2D>().velocity = new Vector2(-Mathf.Cos(curRot) * speed, -Mathf.Sin(curRot) * speed);
                cooldown = 0.4;
            }
            i++;
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

    override
    public void handleHit()
    {
        gsmScript.playEnemyHitSound();
    }
}
