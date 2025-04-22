using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour, I_Interactable
{
    private PlayerManager pmScript;
    public Vector2 _direction;
    public RoomSpawnScript _roomSpawn; //Manager
    public LayerMask _doorLayer;

    // Start is called before the first frame update
    void Start()
    {
        //S_roomSpawn = GameObject.Find("GameSceneManager").GetComponent<RoomSpawnScript>();
        pmScript = null;
    }

    //Calls Coroutine so that animation can play out before destruction.
    public void interact()
    {
        _roomSpawn.spawnRoom(_direction, gameObject);
        StartCoroutine(openDoor());
    }

    //triggers if the player enters range of the chest
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == CodeLibrary.player)
        {
            pmScript = collision.gameObject.GetComponent<PlayerManager>();
            if (pmScript != null)
            {
                pmScript.canInteractWith(this);
            }
        }
    }

    //tells the playerManager that the player is no longer in range
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == CodeLibrary.player)
        {
            pmScript = collision.gameObject.GetComponent<PlayerManager>();
            if (pmScript != null)
            {
                pmScript.nowOutOfRange(this);
            }
        }
    }


    /// <summary>
    /// Allows timing for the animation to run, and removes both doors to oil the exploration process.
    /// </summary>
    /// <returns></returns>
    private IEnumerator openDoor()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)gameObject.transform.position + _direction*1.5f, _direction, 7f, _doorLayer);
        pmScript.gsmScript.playDoorOpenSound();
        if(hit)
        {
            print(hit.transform.position);
            GameObject otherDoor = hit.collider.gameObject;
            yield return new WaitForSeconds(.5f);
            otherDoor.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(.5f);
        }
        gameObject.SetActive(false);
    }
}
