using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PatrolScript : EnemyScript
{
    List<Vector3> waypoints;
    int currentWaypoint = 0;

    public GameObject turret;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public GameObject player;
    private Transform playerTransform;
    private Transform _transform;
    private Rigidbody2D _rbody;
    public LayerMask targetLayer;
    int speed = 50;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(CodeLibrary.player);
        _rbody = GetComponent<Rigidbody2D>();
        gsmScript = GameObject.FindGameObjectWithTag(CodeLibrary.SceneManager).GetComponent<GSMScript>();
        playerTransform = player.transform;
        _transform = transform;
        health = health * gsmScript.monsterDifficulty;
        damage = damage * gsmScript.monsterDifficulty;

        StartCoroutine(HandleShooting());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isDead();
        if(waypoints.Count > 0)
        {
            _rbody.velocity = EvaluateWaypoint() * speed * Time.deltaTime;
        }

        Vector2 closeness = (playerTransform.position - _transform.position);
        float angle = Mathf.Atan2(closeness.y, closeness.x) * Mathf.Rad2Deg;
        if (closeness.magnitude < 4)
        {
            float rotateSpeed = 10 * Time.deltaTime; // speed of rotation
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, rotation, rotateSpeed);
        }
    }

    IEnumerator HandleShooting()
    {
        
        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(bulletSpawnPoint.position, turret.transform.rotation * Vector3.right, 20, targetLayer);
            if (hit)
            { 
                for (int i = 0; i < 3; i++)
                {
                    ShootBullet();
                    yield return new WaitForSeconds(0.3f);
                }
                yield return new WaitForSeconds(1.2f);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    void ShootBullet()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab);
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = turret.transform.rotation;
        bullet.GetComponent<Rigidbody2D>().velocity = turret.transform.rotation * new Vector3(8, 0, 0);
    }

    public Vector3 EvaluateWaypoint()
    {
        Vector3 dir = waypoints[currentWaypoint] - transform.position;
        float pointAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float rotateSpeed = 4 * Time.deltaTime; // speed of rotation
        Quaternion rotation = Quaternion.AngleAxis(pointAngle, Vector3.forward);
        _rbody.transform.rotation = Quaternion.Slerp(_rbody.transform.rotation, rotation, rotateSpeed);
        if (dir.magnitude < 0.1) // enemy has reached the waypoint
        {
            currentWaypoint++;
            if (currentWaypoint == waypoints.Count)
            {
                currentWaypoint = 0;
            }
        }
        dir.Normalize();
        return dir;
    }

    public void setData(int _difficulty, double _roomVal, GameObject _room)
    {
        Vector3 rpos = _room.transform.position;
        waypoints = new List<Vector3>();
        print("new List");
        if (_difficulty == 5)
        {
            if (_roomVal < CodeLibrary.puzzleStairs)
            {
                // doesn't spawn patrol enemies
            }
            else if (_roomVal < CodeLibrary.goodLuck)
            {

                waypoints.Add(new Vector3(rpos.x + 3, rpos.y + 3, 0));
                print(waypoints.Count);
                waypoints.Add(new Vector3(rpos.x - 3, rpos.y + 3, 0));
                waypoints.Add(new Vector3(rpos.x - 3, rpos.y - 3, 0));
                waypoints.Add(new Vector3(rpos.x + 3, rpos.y - 3, 0));
            }
            else if (_roomVal < CodeLibrary.chestRoom)
            {
                // Instantiate(_patrolMonster, new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y), Quaternion.identity);
                waypoints.Add(new Vector3(rpos.x + 3, rpos.y - 1, 0));
                waypoints.Add(new Vector3(rpos.x + 3, rpos.y + 1, 0));
                waypoints.Add(new Vector3(rpos.x + 2, rpos.y + 1, 0));
                waypoints.Add(new Vector3(rpos.x + 2, rpos.y + 3, 0));
                waypoints.Add(new Vector3(rpos.x - 2, rpos.y + 3, 0));
                waypoints.Add(new Vector3(rpos.x - 2, rpos.y + 1, 0));
                waypoints.Add(new Vector3(rpos.x - 3, rpos.y + 1, 0));
                waypoints.Add(new Vector3(rpos.x - 3, rpos.y - 1, 0));
                waypoints.Add(new Vector3(rpos.x - 2, rpos.y - 1, 0));
                waypoints.Add(new Vector3(rpos.x - 2, rpos.y - 3, 0));
                waypoints.Add(new Vector3(rpos.x + 2, rpos.y - 3, 0));
                waypoints.Add(new Vector3(rpos.x + 2, rpos.y - 1, 0));
            }
            else
            {
                // Instantiate(_patrolMonster, new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y), Quaternion.identity);
                if (this.gameObject.transform.position.y < _room.transform.position.y)
                {
                    waypoints.Add(new Vector3(rpos.x - 3, rpos.y + 5, 0));
                    waypoints.Add(new Vector3(rpos.x - 3, rpos.y - 5, 0));
                }
                else
                {
                    waypoints.Add(new Vector3(rpos.x + 3, rpos.y + 5, 0));
                    waypoints.Add(new Vector3(rpos.x + 3, rpos.y - 5, 0));
                }
            }
        }

        if (_difficulty == 6)
        {
            // floor 6
            if (_roomVal < CodeLibrary.puzzleStairs)
            {
                // doesn't spawn patrol enemies
            }
            else if (_roomVal < CodeLibrary.goodLuck)
            {
                // Instantiate(_patrolMonster, new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y), Quaternion.identity);
                waypoints.Add(new Vector3(rpos.x + 3, rpos.y + 3, 0));
                waypoints.Add(new Vector3(rpos.x - 3, rpos.y + 3, 0));
                waypoints.Add(new Vector3(rpos.x - 3, rpos.y - 3, 0));
                waypoints.Add(new Vector3(rpos.x + 3, rpos.y - 3, 0));
            }
            else if (_roomVal < CodeLibrary.chestRoom)
            {
                //Instantiate(_patrolMonster, new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y), Quaternion.identity);
                waypoints.Add(new Vector3(rpos.x + 5, rpos.y, 0));
                waypoints.Add(new Vector3(rpos.x - 5, rpos.y, 0));
            }
            else
            {
                // this enemy doesn't move
                //Instantiate(_patrolMonster, new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y), Quaternion.identity);
            }
        }
        print(waypoints.Count);
    }
}
