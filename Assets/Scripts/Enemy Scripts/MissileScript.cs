using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : EnemyBulletScript
{
    public GSMScript gsmScript;
    public double deathTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        gsmScript = GameObject.FindGameObjectWithTag(CodeLibrary.SceneManager).GetComponent<GSMScript>();
        deathTimer = 2.0;
    }

    // Update is called once per frame
    void Update()
    {
        if (deathTimer > 0)
        {
           
            deathTimer -= Time.deltaTime;
        }
        else
        {
            // explode
            Destroy(gameObject);          
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (checkCollision(collision))
        {
            // explode
            attack(collision.gameObject);
            gsmScript.missileDead(gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}
