using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stops the player from hiding in hallway when fighting boss.
/// </summary>
public class AntiCheeseScript : MonoBehaviour
{
    public GameObject _wall;

    /// <summary>
    /// When player enters trigger, summon wall to prevent retreat.
    /// </summary>
    /// <param name="collision">Thing that enters trigger.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == CodeLibrary.player)
        {
            _wall.SetActive(true);
        }
    }
}
