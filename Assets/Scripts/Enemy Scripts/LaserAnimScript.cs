using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAnimScript : EnemyBulletScript
{
    // Start is called before the first frame update
    void Start()
    {
        damage = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == CodeLibrary.player)
        {
            //Changing to a bullet style attack
            collision.gameObject.GetComponent<PlayerManager>().registerHit(transform.position, damage);
        }
    }
}
