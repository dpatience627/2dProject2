using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyBulletT3Script : EnemyBulletScript
{
    // bullet3 for the boss
    // has all interactions of normal bullet3 except damages player and doesn't damage boss
    Rigidbody2D _rbody;
    private int numberOfCollisions;

    private void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        numberOfCollisions = CodeLibrary.weapon3NumBounces;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (checkCollision(collision))
        {
            attack(collision.gameObject);
            numberOfCollisions--;
            if (numberOfCollisions < 1)
            {
                Destroy(gameObject);
            }
        }
    }


}
