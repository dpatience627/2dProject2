using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BulletT3Script : BulletScript
{
    private int numberOfCollisions;

    private void Start()
    {
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
