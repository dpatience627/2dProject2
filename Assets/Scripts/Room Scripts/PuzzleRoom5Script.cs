using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoom5Script : MonoBehaviour
{
    public GameObject _prizeDoors;

    /// <summary>
    /// If the monster makes it through the maze and hits the target, the doors to the chests open.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == CodeLibrary.enemy)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            _prizeDoors.SetActive(false);
        }
    }
}
