using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The spawning of the next room in the player's path of travel.
/// </summary>
public class RoomSpawnScript : MonoBehaviour
{
    //Set up for the raycast
    public LayerMask _backgroundLayer;

    //Random number to determine room
    public double room;

    //Rooms
    public GameObject newRoom;
    public GameObject _room1;
    public GameObject _chestRoom;
    public GameObject _goodLuck;
    public GameObject _stairs;
    public GameObject _puzzle;
    public GameObject _empty;

    //Prevents Spawning on boss floors.
    public bool _canSpawn;

    /// <summary>
    /// Generates first room in the dungeon.
    /// </summary>
    void Start()
    {
        if(_canSpawn)
        {
            newRoom = Instantiate(_empty, new Vector2(0, 0), Quaternion.identity);
            Transform roomTransform = newRoom.transform;
            for (int i = 0; i < roomTransform.childCount; i++)
            {
                doorScript hasDoor = roomTransform.GetChild(i).GetComponent<doorScript>();

                if (hasDoor != null)
                {
                    hasDoor._roomSpawn = this;
                }
            }
        }
    }

    /// <summary>
    /// When player enters trigger, a room will spawn in that direction if there isn't one already.
    /// </summary>
    /// <param name="collision">Object entering trigger.</param>
    public void spawnRoom(Vector2 direction, GameObject door)
    {
        // Generates a number for the next room to spawn.
        room = Random.Range(0, CodeLibrary.totalRoomProb + 1);

        //Position of next room.
        float x = 11 * direction.x;
        float y = 11 * direction.y;

        if(!isInContact(door, direction))
        {
            GameObject newRoom; //Used to connect the doors to the GSM.

            if(room<CodeLibrary.puzzleStairs)
            {
                if (!FloorCheckScript.getFloorSpawn())
                {
                    newRoom = Instantiate(_stairs, new Vector2(x + door.transform.position.x, y + door.transform.position.y), Quaternion.identity);
                    FloorCheckScript.setFloorSpawn(true);
                }
                else
                {
                    newRoom = Instantiate(_puzzle, new Vector2(x + door.transform.position.x, y + door.transform.position.y), Quaternion.identity);
                }
            }
            else if(room < CodeLibrary.goodLuck)
            {
                newRoom = Instantiate(_goodLuck, new Vector2(x + door.transform.position.x, y + door.transform.position.y), Quaternion.identity);
            }
            else if(room < CodeLibrary.chestRoom)
            {
                newRoom = Instantiate(_chestRoom, new Vector2(x + door.transform.position.x, y + door.transform.position.y), Quaternion.identity);
            }
            else
            {
                newRoom = Instantiate(_room1, new Vector2(x + door.transform.position.x, y + door.transform.position.y), Quaternion.identity);
            }

            Transform roomTransform = newRoom.transform;
            for(int i=0; i<roomTransform.childCount; i++)
            {
                doorScript hasDoor = roomTransform.GetChild(i).GetComponent<doorScript>();
                if(hasDoor != null)
                {
                    hasDoor._roomSpawn = this;
                }

                MonsterSpawnScript mss = roomTransform.GetChild(i).GetComponent<MonsterSpawnScript>();
                if (mss != null)
                {
                    mss._roomVal = room;
                    mss._room = newRoom;
                }
            }
        }
    }

    /// <summary>
    /// Raycast in the direction the trigger is relative to the room to see if another room is there.
    /// </summary>
    /// <returns>True if another room is present where the next room will go.</returns>
    bool isInContact(GameObject door, Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(door.transform.position, direction, 7f, _backgroundLayer);

        return (hit.collider != null);
    }
}




//    ...vvvv)))))).
//      / ~~\               ,,,c(((((((((((((((((/
//     / ~~c \.         .vv)))))))))))))))))))\``
//         G_G__   ,,(((KKKK//////////////'
//      , Z~__ '@,gW@@AKXX~MW,gmmmz==m_.
//     iP,dW@!,A @@@@@@@@@@@@@@@A` , W@@A\c
//      ]b_.__zf !P~@@@@@*P~b.~+=m@@@*~ g@Ws.
//         ~`    ,2W2m. '\[ ['~~c'M7 _gW@@A`'s
//           v=XX)==== Y -  [ [    \c/*@@@*~ g@@i
//          /v~           !.!.     '\c7+sg@@@@@s.
//         //              'c'c       '\c7*X7~~~~
//        ]/                 ~=Xm_       '~=(Gm_.