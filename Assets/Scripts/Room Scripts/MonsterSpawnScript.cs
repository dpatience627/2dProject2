using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawning of monsters off of a game object.
/// </summary>
public class MonsterSpawnScript : MonoBehaviour
{
    //List of available monsters.
    public GameObject _chaseMonster;
    public GameObject _pathMonster;
    public GameObject _turretMonster;
    public GameObject _patrolMonster;
    GSMScript _sceneManager;

    public double _roomVal;
    public GameObject _room;

    int _difficulty;

    /// <summary>
    /// Spawns a random monster from the list of available monsters.
    /// </summary>
    void Start()
    {
        _sceneManager = GameObject.Find("GameSceneManager").GetComponent<GSMScript>();
        int monsterSpawned = Random.Range(0, 10);
        _difficulty = _sceneManager.monsterDifficulty;

        if (monsterSpawned < 3)
        {
            Instantiate(_chaseMonster, new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y), Quaternion.identity);
        }
        else if (monsterSpawned < 6)
        {
            Instantiate(_pathMonster, new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y), Quaternion.identity);
        }
        else if (monsterSpawned < 8 && (_difficulty > 3))
        {
            Instantiate(_turretMonster, new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y), Quaternion.identity);
        }
        else if (_difficulty > 3)
        {
            print("patrol");
            GameObject patrol = Instantiate(_patrolMonster, new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y), Quaternion.identity);
            patrol.GetComponent<PatrolScript>().setData(_difficulty, _roomVal, _room);
        }
        else
        {
            Instantiate(_chaseMonster, new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y), Quaternion.identity);
        }
    }
}
