using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Transports player to the fourth floor.
/// </summary>
public class StairsScriptFloor3 : MonoBehaviour
{
    GSMScript gsmScript;

    /// <summary>
    /// Connects with Game Manager to facilitate Scene Change.
    /// </summary>
    void Start()
    {
        gsmScript = GameObject.FindGameObjectWithTag(CodeLibrary.SceneManager).GetComponent<GSMScript>();
    }

    /// <summary>
    /// If player enters trigger, changes to level 4.
    /// </summary>
    /// <param name="collision">Object Entering Trigger</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == CodeLibrary.player)
        {
            gsmScript.sceneChange(CodeLibrary.floor4Scene);
        }
    }
}
