using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerWeaponScript weaponScript;
    private PlayerMovement movementScript;
    private PlayerStats statsScript;

    public GSMScript gsmScript;


    private Animator _ani;
    private SpriteRenderer _spriteRenderer;
    private I_Interactable interactable;
    private bool canInteract;
    private bool in_iframes;
    public bool canAct;


    private void Awake()
    {
        weaponScript = GetComponent<PlayerWeaponScript>();
        movementScript = GetComponent<PlayerMovement>();
        statsScript = GetComponent<PlayerStats>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _ani = GetComponent<Animator>();
        gsmScript = GameObject.FindGameObjectWithTag(CodeLibrary.SceneManager).GetComponent<GSMScript>();

        weaponScript.stats = statsScript;
        movementScript.stats = statsScript;
    }

    // Start is called before the first frame update
    //Initializes components & other things
    void Start()
    {
        canInteract = false;
        in_iframes = false;
        canAct = true;
    }

    // Update is called once per frame
    //Does all player handling, no other player script should have an update cycle but should be called here
    void Update()
    {
        if (canAct)
        {
            weaponScript.handleWeaponSystem();

            //Allows the player to interact with a chest if in range
            if (canInteract && Input.GetKeyDown(KeyCode.E))
            {
                _ani.SetBool("isInteract", true);
                interactable.interact();
                StartCoroutine(closeInteract());
            }
        }

        //kills the player if they die
        if (hasDied())
        {
            _ani.SetBool("hasDied", true);
            movementScript.canMove = false;
            StartCoroutine(playerDeath());
        }
    }

    //Does all player handling, no other player script should have an FixedUpdate cycle but should be called here
    private void FixedUpdate()
    {
            if (canAct)
            {
                movementScript.handleMovement();
            }
        }

    //Returns if the player has died
    private bool hasDied()
    {
        return statsScript.health < 1;
    }

    //Exchanges current weapon with the weapon in the chest
    public Weapon collectWeapon(Weapon newWeapon)
    {
        return weaponScript.changePrimary(newWeapon);
    }


    public PlayerStats getStatsScript()
    {
        return statsScript;
    }

    //Is triggered by being in range of a chest
    public void canInteractWith(I_Interactable obj)
    {
        interactable = obj;
        canInteract = true;
    }

    //Is triggered upon leaving the range of a chest
    public void nowOutOfRange(I_Interactable obj)
    {
        if (interactable.Equals(obj))
        {
            interactable = null;
            canInteract = false;
        }
    }

    //Handles collision damage
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == CodeLibrary.enemy)
        {
            registerHit(collision.gameObject);
        }
    }

    public void registerHit(GameObject damager)
    {
        if (!in_iframes) 
        {
            if(damager.GetComponent<EnemyScript>() != null)
            {
                statsScript.takeDamage(damager.GetComponent<EnemyScript>().damage);
                StartCoroutine(movementScript.takeKnockback(damager.transform.position));
                StartCoroutine(iFrameCD());
            }
            gsmScript.playPlayerHitSound();
        }

    }

    //Used for bullets
    public void registerHit(Vector2 objPos, int dmg)
    {
        if (!in_iframes)
        {
            statsScript.takeDamage(dmg);
            //StartCoroutine(movementScript.takeKnockback(objPos));
            StartCoroutine(iFrameCD());
            gsmScript.playPlayerHitSound();
        }
    }

    private IEnumerator playerDeath()
    {
        yield return new WaitForSeconds(2);
        gsmScript.handlePlayerDeath();
    }

    private IEnumerator closeInteract()
    {
        yield return new WaitForSeconds(1);
        _ani.SetBool("isInteract", false);
    }

    private IEnumerator iFrameCD()
    {
        in_iframes = true;
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(CodeLibrary.iFramesDuration);
        _spriteRenderer.color = Color.white;
        in_iframes = false;
    }
}
