using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon4Script : MonoBehaviour
{
    public Transform _playerTransform;
    public PlayerStats pStats;

    Rigidbody2D _rbody;

    public bool deployed;
    float hoverTime;
    Vector3 homeStateOffset;

    private void Awake()
    {
        _playerTransform = transform.parent.GetChild(0);
        pStats = _playerTransform.GetComponent<PlayerStats>();
    }

    // Start is called before the first frame update
    void Start()
    {
        deployed = false;
        _rbody = GetComponent<Rigidbody2D>();
        homeStateOffset = Vector3.zero;
        hoverTime = CodeLibrary.weapon4AttackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!deployed)
        {
            _rbody.position = _playerTransform.position + homeStateOffset;
        }
    }

    public void deploy(Vector2 target)
    {
        StartCoroutine(runAttack(target));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == CodeLibrary.enemy)
        {
            collision.gameObject.GetComponent<EnemyScript>().Damage(pStats.weapon.damage);
            //print("do" + pStats.weapon.damage);
        }
    }



    private IEnumerator runAttack(Vector2 target)
    {
        deployed = true;

        //Home in upon the target with decreasing velocity or stop if the velocity is low
        Vector2 direction = target.normalized;
        _rbody.velocity = direction * CodeLibrary.weapon4Speed;
        while ((_rbody.position - target).magnitude > 0.1 && _rbody.velocity.magnitude > 1.5)
        {
            direction = target.normalized;
            _rbody.velocity = direction * _rbody.velocity.magnitude / (1 + 2 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }


        hoverTime = CodeLibrary.weapon4AttackSpeed * (1 - (pStats.bulletSpeedModifier - 1));
        _rbody.velocity = Vector2.zero;
        yield return new WaitForSeconds(hoverTime);
        
        direction = ((Vector2)_playerTransform.position - _rbody.position).normalized;
        _rbody.velocity = direction * 2;

        while ((_rbody.position - (Vector2) _playerTransform.position).magnitude > 0.1)
        {
            direction = ((Vector2)_playerTransform.position - _rbody.position).normalized;
            _rbody.velocity = _rbody.velocity.magnitude * direction;
            if (_rbody.velocity.magnitude < CodeLibrary.weapon4Speed)
            {
                _rbody.velocity = _rbody.velocity * (1 + 2 * Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
        deployed = false;
    }
}
