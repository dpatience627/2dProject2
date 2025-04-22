using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Resets if the stairs have spawned
/// </summary>
public class FirstRoomScript : MonoBehaviour
{
    /// <summary>
    /// Sets the stair presence for the floor to false.
    /// </summary>
    void Start()
    {
        FloorCheckScript.setFloorSpawn(false);
    }
}
