using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public PlayerStats stats;
    public PlayerManager pmScript;
    Camera _Maincam;
    Rigidbody2D _rbody;

    public LayerMask antiTeleportWallsLMask;
    public LayerMask nonCollisionLMask;
    private Animator _ani;

    public bool canTeleport;
    public bool canMove;



    private void Awake()
    {
        _Maincam = Camera.main;
        _rbody = GetComponent<Rigidbody2D>();
        pmScript = GetComponent<PlayerManager>();
        _ani = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canTeleport = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        _ani.SetBool("canWarp", canTeleport);
    }

    private void FixedUpdate()
    {

    }

    //Called in Update of the PlayerManager
    public void handleMovement()
    {

        if (Input.GetMouseButton(1) && !_ani.GetBool("hasDied"))
        {
            if (canTeleport)
            teleport();
        }


        if (canMove)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            _rbody.velocity = new Vector2(x, y) * stats.movementSpeed;
        }
    }


    private void teleport()
    {
        
        Vector3 dest = _Maincam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 MovementDelta = new Vector2(dest.x, dest.y) - _rbody.position;
        print(MovementDelta.magnitude);

        if (MovementDelta.magnitude > stats.maxTPDistance)
        {
            print(MovementDelta);
            MovementDelta = MovementDelta.normalized * stats.maxTPDistance;
            print("shrunk");
            print(MovementDelta);

        }

        if (isValidTPDestination(MovementDelta))
        {
            _ani.SetBool("isWarp", true);
            StartCoroutine(teleportCD());
            _rbody.position = MovementDelta + _rbody.position;
            _rbody.velocity = Vector2.zero;
        }
        else
        {
            StartCoroutine(playTPFail());
            //Fizzle animation & sound
        }

        StartCoroutine(closeWarp());
    }

    private bool isValidTPDestination(Vector2 movementVector)
    {
        if (Physics2D.Raycast(_rbody.position, movementVector.normalized, movementVector.magnitude, antiTeleportWallsLMask))
        {
            //print("RealWall");
            //print(movementVector);
            return false;
        }

        if (Physics2D.BoxCast(_rbody.position + movementVector, new Vector2(0.75f, 0.75f), 0, Vector2.zero, 1, ~nonCollisionLMask))
        {
            //print("wall");
            return false;
        }
        return true;
    }

    public IEnumerator takeKnockback(Vector2 dmgSource)
    {
        //print("take KB");
        print((_rbody.position - dmgSource).normalized * CodeLibrary.hitKB);

        canMove = false;
        _rbody.AddForce((_rbody.position - dmgSource).normalized * CodeLibrary.hitKB, ForceMode2D.Force);
        yield return new WaitForSeconds(0.2f);
        canMove = true;

    }

    private IEnumerator playTPFail()
    {
        canTeleport = false;
        pmScript.gsmScript.playTPFailSound();
        yield return new WaitForSeconds(stats.tpEndLag);
        canTeleport = true;
    }

    private IEnumerator teleportCD()
    {
        canTeleport = false;
        canMove = false;
        pmScript.gsmScript.playTPSuccessSound();
        yield return new WaitForSeconds(stats.tpEndLag);
        canMove = true;
        yield return new WaitForSeconds(stats.tpCD - stats.tpEndLag);
        pmScript.gsmScript.playTPRecoverySound();
        canTeleport = true;
    }

    private IEnumerator closeWarp()
    {
        yield return new WaitForSeconds(1);
        _ani.SetBool("isWarp", false);
    }
}
