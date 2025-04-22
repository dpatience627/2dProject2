using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyBulletT1Script : EnemyBulletScript
{
    // bullet1 for the boss
    // has all interactions of normal bullet1 except damages player and doesn't damage boss
    Rigidbody2D _rbody;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (checkCollision(collision))
        {
            attack(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
