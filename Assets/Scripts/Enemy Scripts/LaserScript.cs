using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public GameObject player;
    private Transform playerTransform;
    public double cooldown;
    public GameObject boss;
    public Transform bossTransform;

    Vector2 closeness;
    Vector3 endofLine;
    float angle;
    public LayerMask _wallLayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(CodeLibrary.player);
        playerTransform = player.transform;
        bossTransform = boss.transform;

        // getting angle to shoot raycast at
        closeness = (playerTransform.position - bossTransform.position);
        angle = Mathf.Atan2(closeness.y, closeness.x) * Mathf.Rad2Deg;

        // setting collision point to end of line renderer
        endofLine = hitWall();
        print(endofLine);

        LineRenderer l = gameObject.GetComponent<LineRenderer>();
        List<Vector3> pos = new List<Vector3>();
        pos.Add(new Vector3(bossTransform.position.x, bossTransform.position.y));
        pos.Add(endofLine);
        l.startWidth = 1f;
        l.endWidth = 1f;
        l.SetPositions(pos.ToArray());
        l.useWorldSpace = true;
        
        cooldown = 2.0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            // destroy itself
            Destroy(gameObject);
        }
    }

    public Vector3 hitWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(bossTransform.position, closeness, 50f, _wallLayer);

        return hit.point;

    }
}
