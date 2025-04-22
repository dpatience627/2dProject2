using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderScript : EnemyScript
{

    public float _timeRemaining = 1;
    private Rigidbody2D _rbody;
    private Animator _ani;
    

    // Start is called before the first frame update
    void Start()
    {
        gsmScript = GameObject.FindGameObjectWithTag(CodeLibrary.SceneManager).GetComponent<GSMScript>();
        _rbody = GetComponent<Rigidbody2D>();
        health = health * gsmScript.monsterDifficulty;
        damage = damage * gsmScript.monsterDifficulty;
        _ani = GetComponent<Animator>();
        _ani.SetBool("hasDied", false);
    }

    // Update is called once per frame
    void Update()
    {
        isDead(_ani);
        if (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;
        }
        else if(!_ani.GetBool("hasDied"))
        {
            // moves in a random direction every second
            _rbody.velocity = new Vector2((Random.Range(-1, 2) * 5), (Random.Range(-1, 2)) * 5);
            _timeRemaining = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == CodeLibrary.wall)
        {
            // if it collides with a wall, new velocity
            _rbody.velocity = new Vector2((Random.Range(-1, 2) * 5), (Random.Range(-1, 2)) * 5);
        }
    }
}
