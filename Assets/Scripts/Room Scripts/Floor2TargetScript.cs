using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Talks to second floor puzzle that current object got hit.
/// </summary>
public class Floor2TargetScript : MonoBehaviour
{
    /// <summary>
    /// Talks to second floor puzzle that current object got hit.
    /// </summary>
    /// <param name="collision">Object entering trigger</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != CodeLibrary.player)
        {
            PuzzleRoom2Script.hitRegistered(gameObject);
        }
    }
}
