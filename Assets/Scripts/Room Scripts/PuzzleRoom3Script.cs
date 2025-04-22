using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for Third Floor Puzzle
/// </summary>
public class PuzzleRoom3Script : MonoBehaviour
{
    //Allows for object reference in static methods.
    static public GameObject _enemyGates;
    static public GameObject _chestGates;
    static public GameObject _target1;
    static public GameObject _target2;
    static public GameObject _target3;

    //Allows for object reference from unity
    public GameObject _t1;
    public GameObject _t2;
    public GameObject _t3;
    public GameObject _eg;
    public GameObject _cg;

    //Object Hit Checkers
    static bool t1hit = false;
    static bool finalHit = false;

    /// <summary>
    /// Generates an order for the targets to be it in
    /// and translates the Unity objects to the Static ones.
    /// </summary>
    void Start()
    {
        _chestGates = _cg;
        _enemyGates = _eg;
        _target1 = _t1;

        if(Random.Range(0, 2)==0)
        {
            _target2 = _t2;
            _target3 = _t3;
        }
        else
        {
            _target2 = _t3;
            _target3 = _t2;
        }

    }

    /// <summary>
    /// Makes sure that objects are hit in the right order.
    /// If they are, then player can access the chests
    /// If they arent, then the player has to fight monsters.
    /// </summary>
    /// <param name="contact">Target that has been hit.</param>
    static public void hitRegistered(GameObject contact)
    {
        if (contact == _target1 && !t1hit)
        {
            t1hit = true;
            _target1.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if(contact == _target2 && t1hit && !finalHit)
        {
            _chestGates.SetActive(false);
            finalHit = true;
        }
        else if(contact == _target3 && t1hit && !finalHit)
        {
            _enemyGates.SetActive(false);
            finalHit = true;
        }
    }
}
