using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for First Floor Puzzle
/// </summary>
public class PuzzleRoomFloor6 : MonoBehaviour
{
    //Allows for reference in static methods.
    static public GameObject _gates;
    static public GameObject _target1;
    static public GameObject _target2;
    static public GameObject _target3;
    static public GameObject _target4;
    static public GameObject _target5;

    //Allows for reference from unity
    public GameObject _t1;
    public GameObject _t2;
    public GameObject _t3;
    public GameObject _t4;
    public GameObject _t5;
    public GameObject _g;

    //Detectors of object hit order
    static bool t1hit = false;
    static bool t2hit = false;
    static bool t3hit = false;
    static bool t4hit = false;

    /// <summary>
    /// Generates an order for the targets to be it in
    /// and translates the Unity objects to the Static ones.
    /// </summary>
    void Start()
    {
        _gates = _g;
        int order = Random.Range(0, 5);

        if (order == 0)
        {
            _target1 = _t3;
            _target2 = _t5;
            _target3 = _t4;
            _target4 = _t2;
            _target5 = _t1;
        }
        else if (order == 1)
        {
            _target1 = _t1;
            _target2 = _t3;
            _target3 = _t2;
            _target4 = _t4;
            _target5 = _t5;
        }
        else if (order == 2)
        {
            _target1 = _t3;
            _target2 = _t1;
            _target3 = _t2;
            _target4 = _t5;
            _target5 = _t4;
        }
        else if (order == 3)
        {
            _target1 = _t3;
            _target2 = _t4;
            _target3 = _t5;
            _target4 = _t2;
            _target5 = _t1;
        }
        else if (order == 4)
        {
            _target1 = _t5;
            _target2 = _t1;
            _target3 = _t2;
            _target4 = _t3;
            _target5 = _t4;
        }
        else
        {
            _target1 = _t1;
            _target2 = _t2;
            _target3 = _t3;
            _target4 = _t4;
            _target5 = _t5;
        }
    }

    /// <summary>
    /// Makes sure that objects are hit in the right order.
    /// If they are, then player can access the chests
    /// If they arent, then the puzzle resets.
    /// </summary>
    /// <param name="contact">Target that has been hit.</param>
    static public void hitRegistered(GameObject contact)
    {
        if (contact == _target1 && !t1hit && !t2hit && !t3hit && !t4hit)
        {
            t1hit = true;
            _target1.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (contact == _target2 && t1hit && !t2hit && !t3hit && !t4hit)
        {
            t2hit = true;
            _target2.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (contact == _target3 && t2hit && t1hit && !t3hit && !t4hit)
        {
            t3hit = true;
            _target3.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (contact == _target4 && t2hit && t1hit && t3hit && !t4hit)
        {
            t4hit = true;
            _target4.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (contact == _target5 && t2hit && t1hit && t3hit && t4hit)
        {
            _target5.GetComponent<SpriteRenderer>().color = Color.green;
            _gates.SetActive(false);
        }
        else
        {
            t1hit = false;
            t2hit = false;
            t3hit = false;
            t4hit = false;
            _target1.GetComponent<SpriteRenderer>().color = Color.white;
            _target2.GetComponent<SpriteRenderer>().color = Color.white;
            _target3.GetComponent<SpriteRenderer>().color = Color.white;
            _target4.GetComponent<SpriteRenderer>().color = Color.white;
            _target5.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
