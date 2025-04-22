using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserScript : EnemyScript
{
    public GameObject player;
    private Transform playerTransform;
    private Transform _transform;
    private Animator _ani;

    private Rigidbody2D _rbody;
    public LayerMask _wall;
    private float moveSpeed;
    public float monsterSpeed = 5;
    private bool vision;

    // Start is called before the first frame update
    void Start()
    {
        gsmScript = GameObject.FindGameObjectWithTag(CodeLibrary.SceneManager).GetComponent<GSMScript>();
        player = GameObject.FindGameObjectWithTag(CodeLibrary.player);
        playerTransform = player.transform;
        _transform = transform;
        vision = false;
        moveSpeed = monsterSpeed;
        _rbody = GetComponent<Rigidbody2D>();
        health = health * gsmScript.monsterDifficulty;
        damage = damage * gsmScript.monsterDifficulty;
        _ani = GetComponent<Animator>();
        _ani.SetBool("hasDied", false);
        _ani.SetBool("targetLocked", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isDead(_ani);
        Vector2 closeness = (playerTransform.position - _transform.position);

        if (vision && !_ani.GetBool("hasDied"))
        {
            _ani.SetBool("targetLocked", true);
            // if player is seen, turns toward player
            float angle = Mathf.Atan2(closeness.y, closeness.x) * Mathf.Rad2Deg;
            _rbody.rotation = angle;
            Move(closeness);
        }
        // if player is within 20 degrees, will see the player
        if (((Vector2.Angle(_transform.right, closeness) < 20) || (Vector2.Angle(-_transform.right, closeness) < 20)) && (closeness.magnitude < 5))
        {
            vision = true;
        }

        if (!vision && !_ani.GetBool("hasDied"))
        {
            // moves side to side
            Goomba();
        }
    }

    void Move(Vector2 direction)
    {
        // moves toward player
        _rbody.MovePosition((Vector2)_transform.position + (direction.normalized * monsterSpeed * Time.deltaTime));
    }

    private bool hitWall()
    {
        if (moveSpeed > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(_rbody.position, Vector2.right, 0.6f, _wall);
            return (hit.collider != null);
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(_rbody.position, Vector2.left, 0.6f, _wall);
            return (hit.collider != null);
        }
    }

    private void Goomba()
    {
        _rbody.velocity = new Vector2(moveSpeed, 0);

        if (hitWall())
        {
            moveSpeed = -moveSpeed;
        }
    }
}
