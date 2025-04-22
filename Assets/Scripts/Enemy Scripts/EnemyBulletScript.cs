using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    // parent class for the enemy bullets
    public int damage;

    public int setDamage(int damage)
    {
        this.damage = damage;
        return damage;
    }

    public void attack(GameObject target)
    {

        if (target.tag == CodeLibrary.player)
        {
            // attacks player
            target.GetComponent<PlayerManager>().registerHit((Vector2)this.gameObject.transform.position, damage);
        }
    }

    public bool checkCollision(Collision2D collision)
    {
        // ignores these collisions
        return (collision.gameObject.tag != CodeLibrary.trigger) && (collision.gameObject.tag != CodeLibrary.floor) && (collision.gameObject.tag != CodeLibrary.noCollision) && (collision.gameObject.tag != CodeLibrary.bullet);
    }
    public bool checkCollision(Collider2D collision)
    {
        // ignores these collisions
        return (collision.gameObject.tag != CodeLibrary.trigger) && (collision.gameObject.tag != CodeLibrary.floor) && (collision.gameObject.tag != CodeLibrary.noCollision) && (collision.gameObject.tag != CodeLibrary.bullet);
    }
}
