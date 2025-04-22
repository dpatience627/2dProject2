using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public GameObject _monsters;
    public GameObject _trapDoors;
    public GameObject _chests;
    int preEnemies;
    bool roomComplete;
    bool roomStart;

    /// <summary>
    /// Set up by counting the number of present enemies.
    /// </summary>
    void Start()
    {
        preEnemies = GameObject.FindGameObjectsWithTag(CodeLibrary.enemy).Length;
        roomComplete = false;
        roomStart = false;
    }

    /// <summary>
    /// Checks if the number of monsters are the same as before the room's monsters spawned,
    /// showing all the monsters in the room have died.
    /// </summary>
    void Update()
    {
        if(!roomComplete)
        {
            if (preEnemies == GameObject.FindGameObjectsWithTag(CodeLibrary.enemy).Length && roomStart)
            {
                _trapDoors.SetActive(false);
                roomComplete = true;
            }
        }
        else
        {
            _chests.SetActive(true);
        }
    }

    /// <summary>
    /// Triggers the closing of the doors and the spawning of the monsters.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!roomComplete)
        {
            if (collision.tag == CodeLibrary.player)
            {
                _monsters.SetActive(true);
                _trapDoors.SetActive(true);
                roomStart = true;
            }
        }
    }
}
