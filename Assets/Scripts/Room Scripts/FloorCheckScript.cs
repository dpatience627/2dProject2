using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps track of the presense of the stairs on each floor.
/// </summary>
public class FloorCheckScript : MonoBehaviour
{
    static bool _stairsSpawn = false;

    /// <summary>
    /// Returns if the stairs are present.
    /// </summary>
    /// <returns>True if stars exist on floor.</returns>
    public static bool getFloorSpawn()
    {
        return _stairsSpawn;
    }

    /// <summary>
    /// Allows for the reseting of the stair presence.
    /// </summary>
    /// <param name="setting">Object setting the stair presence.</param>
    public static void setFloorSpawn(bool setting)
    {
        _stairsSpawn = setting;
    }
}
