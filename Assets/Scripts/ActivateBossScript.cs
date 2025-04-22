using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Activates the first boss when the player enters the trigger space.
/// </summary>
public class ActivateBossScript : MonoBehaviour
{
    public GameObject boss;
    public BossScript bossScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == CodeLibrary.player && bossScript.health > 0)
        {
            boss.SetActive(true);
        }
        else if (collision.gameObject.tag == CodeLibrary.bullet && bossScript.health > 0)
        {
            boss.SetActive(true);
        }
    }
}
