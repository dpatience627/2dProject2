using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeT1Script : BulletScript
{
    public PlayerWeaponScript pws;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (checkCollision(collision))
        {
            if(CodeLibrary.weapon1cost <= pws.stats.currency)
            {
                pws.stats.currency -= CodeLibrary.weapon2cost;
                attack(collision.gameObject);
            }
        }
    }

}
