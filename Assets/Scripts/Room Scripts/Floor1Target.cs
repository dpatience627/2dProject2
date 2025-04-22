using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Talks to first floor puzzle that current object got hit.
/// </summary>
public class Floor1Target : MonoBehaviour
{
    /// <summary>
    /// Talks to first floor puzzle that current object got hit.
    /// </summary>
    /// <param name="collision">Object entering trigger</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != CodeLibrary.player)
        {
            PuzzleRoomScript.hitRegistered(gameObject);
        }
    }
}
