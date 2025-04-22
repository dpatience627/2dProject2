using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damage;

    public int setDamage(int damage)
    {
        this.damage = damage;
        return damage;
    }

    public void attack(GameObject target)
    {
        if (target.tag == CodeLibrary.enemy)
        {
            target.GetComponent<EnemyScript>().Damage(damage);
        }
        else if(target.tag == CodeLibrary.player)
        {
            target.GetComponent<PlayerManager>().registerHit((Vector2)this.gameObject.transform.position, damage);
        }
    }

    public bool checkCollision(Collision2D collision)
    {
        return (collision.gameObject.tag != CodeLibrary.trigger) && (collision.gameObject.tag != CodeLibrary.floor) && (collision.gameObject.tag != CodeLibrary.noCollision);
    }
    public bool checkCollision(Collider2D collision)
    {
        return (collision.gameObject.tag != CodeLibrary.trigger) && (collision.gameObject.tag != CodeLibrary.floor) && (collision.gameObject.tag != CodeLibrary.noCollision);
    }
}
