using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health;
    public int damage;
    public GSMScript gsmScript;
    public SpriteRenderer _spriteRenderer;

    // this is the parent class of all enemies

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage(int damage)
    {
        health -= damage;
        handleHit();
    }

    public void isDead(Animator ani)
    {
        if (health <= 0)
        {
            ani.SetBool("hasDied", true);
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(deathAni());  
        }
    }

    //If the enemy doesn't have an animation yet.
    public void isDead()
    {
        if (health <= 0)
        {
            gsmScript.lootEnemy(-1);
            Destroy(gameObject);
        }
    }

    public int Attack()
    {
        return damage;
    }

    private IEnumerator deathAni()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(1);
        gsmScript.lootEnemy(-1);
        Destroy(gameObject);
    }

    private IEnumerator flashRed()
    {
        if(_spriteRenderer.color == Color.red)
        {
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(CodeLibrary.iFramesDuration);
            _spriteRenderer.color = Color.red;
        }
        else
        {
            Color c = _spriteRenderer.color;
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(CodeLibrary.iFramesDuration);
            _spriteRenderer.color = c;
        }
    }

    virtual
    public void handleHit()
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        gsmScript.playEnemyHitSound();
        StartCoroutine(flashRed());
    }
}
