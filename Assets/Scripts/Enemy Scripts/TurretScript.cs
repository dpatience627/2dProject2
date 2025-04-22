using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : EnemyScript
{
    public GameObject turret;
    public Transform bulletSpawnPoint;
    private Transform _transform;
    public GameObject player;
    private Transform playerTransform;
    public GameObject missilePrefab;
    public LayerMask targetLayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(CodeLibrary.player);
        gsmScript = GameObject.FindGameObjectWithTag(CodeLibrary.SceneManager).GetComponent<GSMScript>();
        playerTransform = player.transform;
        _transform = transform;
        health = health * gsmScript.monsterDifficulty;
        damage = damage * gsmScript.monsterDifficulty;
        GetComponent<SpriteRenderer>().color = Color.red;

        StartCoroutine(HandleShooting());
    }

    // Update is called once per frame
    void Update()
    {
        isDead();
        // turret points toward player
        Vector2 playerPosition = player.transform.position;
        Vector2 direction = playerPosition - new Vector2(_transform.position.x, _transform.position.y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (direction.magnitude < 6)
        {
            // fractionally moving toward the point we want
            float rotateSpeed = 4 * Time.deltaTime; // speed of rotation
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
                ShootBullet();
                yield return new WaitForSeconds(1.0f);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    void ShootBullet()
    {
        GameObject missile = GameObject.Instantiate(missilePrefab);
        missile.transform.position = bulletSpawnPoint.position;
        missile.transform.rotation = turret.transform.rotation;
        missile.GetComponent<Rigidbody2D>().velocity = turret.transform.rotation * new Vector3(4, 0, 0);
    }
}
