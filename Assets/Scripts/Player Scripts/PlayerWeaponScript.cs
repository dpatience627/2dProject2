
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour
{

    public Camera cam;
    public PlayerStats stats;
    Transform _transform;

    Vector2 mouseDirection;
    Vector3 mousePosn;

    bool attackIsAvailible;
    bool usingSecondary;
    bool stillSwapping;
    bool deactivating;

    public GameObject _BulletT1Prefab;
    public BladeT1Script _BladeT1Script;
    public GameObject _BulletT3Prefab;
    public Weapon4Script _Weapon4Script;

    public GameObject weapon1Sight;
    public GameObject weapon2Sight;
    public GameObject weapon3Sight;
    public GameObject weapon4Sight;

    public AudioSource audioSource;
    public AudioClip weapon1Sound;
    public AudioClip weapon3Sound;


    //Awake is called before start
    private void Awake()
    {
        cam = Camera.main;
        _transform = transform;
        _BladeT1Script = _transform.GetChild(1).gameObject.GetComponent<BladeT1Script>();
        _Weapon4Script = _transform.parent.GetChild(2).GetComponent<Weapon4Script>();
    }

    // Start is called before the first frame update
    //Initializes things if they haven't been already
    void Start()
    {
        attackIsAvailible = true;
        usingSecondary = false;
        stillSwapping = false;
        deactivating = false;
        mouseDirection = Vector2.zero;

        _BladeT1Script.pws = this;

        //Needs stats & transform to be set before
        //_Weapon4Script.pStats = stats;
        //_Weapon4Script._playerTransform = _transform;

    }

    // Must be called in update not fixedUpdate
    //Is called in update of PlayerManager (Is essentially the update cycle of this script)
    public void handleWeaponSystem()
    {
        updateAim();

        if (Input.GetKeyDown(KeyCode.F))
        {
            swapWeapons();
        }

        //Fires if the player clicks and is not on CD
        if (Input.GetMouseButtonDown(0) && attackIsAvailible)
        {
           // print(stats.weapon);
            if (usingSecondary)
            {
                handleSecondary();
            }
            else
            {
                if (stats.weapon.weaponType.Equals(CodeLibrary.weapon1) && CodeLibrary.weapon1cost <= stats.currency)
                {
                    handleWeapon1();
                }
                else if (stats.weapon.weaponType.Equals(CodeLibrary.weapon2) && CodeLibrary.weapon2cost <= stats.currency)
                {
                    handleWeapon2();
                }
                else if (stats.weapon.weaponType.Equals(CodeLibrary.weapon3) && CodeLibrary.weapon3cost <= stats.currency)
                {
                    handleWeapon3();
                }
                else if (stats.weapon.weaponType.Equals(CodeLibrary.weapon4) && CodeLibrary.weapon4cost <= stats.currency)
                {
                    handleWeapon4();
                }
            }
        }
    }

    //Makes the player face in the direction of the cursors
    void updateAim()
    {
        Vector3 playerPosOnScreen = new Vector3(4 / 5, 1 / 2, 0);
        mousePosn = cam.ScreenToWorldPoint(Input.mousePosition) - _transform.position;
        mouseDirection = new Vector2(mousePosn.x, mousePosn.y).normalized;
        float aimDegrees = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        if (aimDegrees < 0)
        {
            aimDegrees += 360;
        }
        _transform.rotation = Quaternion.Euler(0, 0, aimDegrees);
    }

    //Fires the secondary weapon setting the bullet dmg (same as weapon1)
    void handleSecondary()
    {
        StartCoroutine(StartAttackCooldown(stats.secondary.attackSpeed));
        GameObject bullet = Instantiate(_BulletT1Prefab, (Vector2)_transform.position + (mouseDirection.normalized * 0.5f), Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = mouseDirection * stats.secondary.bulletSpeed;
        bullet.GetComponent<BulletScript>().setDamage((int)(stats.secondary.damage * stats.weaponDamageModifier));
        audioSource.PlayOneShot(weapon1Sound);
    }

    //Fires weapon one when called, and sets bullet dmg
    void handleWeapon1()
    {
        StartCoroutine(StartAttackCooldown(stats.weapon.attackSpeed));
        GameObject bullet = Instantiate(_BulletT1Prefab, (Vector2)_transform.position + (mouseDirection.normalized * 0.5f), Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = mouseDirection * stats.weapon.bulletSpeed;
        bullet.GetComponent<BulletScript>().setDamage((int)(stats.weapon.damage * stats.weaponDamageModifier));
        audioSource.PlayOneShot(weapon1Sound);
        stats.currency -= CodeLibrary.weapon1cost;
    }

    //Practically, does nothing, Weapon works without a method here (works on collision)
    void handleWeapon2()
    {
        //StartCoroutine(StartAttackCooldown(stats.weapon.attackSpeed));
        _BladeT1Script.damage = stats.weapon.damage;

    }

    //Fires weapon three when called, and sets bullet dmg
    void handleWeapon3()
    {
        StartCoroutine(StartAttackCooldown(stats.weapon.attackSpeed));
        GameObject bullet = Instantiate(_BulletT3Prefab, (Vector2)_transform.position + (mouseDirection.normalized * 0.5f), Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = mouseDirection * stats.weapon.bulletSpeed;
        bullet.GetComponent<BulletScript>().setDamage((int)(stats.weapon.damage * stats.weaponDamageModifier));
        stats.currency -= CodeLibrary.weapon3cost;
        audioSource.PlayOneShot(weapon3Sound);
    }

    //Fires out the 4th weapon which is handled by itself
    void handleWeapon4()
    {
        _Weapon4Script.deploy(mousePosn);
        stats.currency -= CodeLibrary.weapon4cost;
        StartCoroutine(weapon4AttackCD());
    }


    //Changes the weapon in playerStats to the new weapon and changes the sprites on the player to be showing the correct weapon
    public Weapon changePrimary(Weapon weapon)
    {
        if (usingSecondary)
        {
            swapWeapons();
        }

        Weapon oldWeapon = stats.weapon;
        stats.weapon = weapon;
        // Find a better way to do all this:
        changeSprites(stats.weapon);

        return oldWeapon;
    }

    private void changeSprites(Weapon newWeapon)
    {
        //sets new weapon sprite to on and all others to off
        if (newWeapon.weaponType == CodeLibrary.weapon1)
        {
            weapon1Sight.SetActive(true);
        }
        else
        {
            weapon1Sight.SetActive(false);
        }

        if (newWeapon.weaponType == CodeLibrary.weapon2)
        {
            weapon2Sight.SetActive(true);
            _BladeT1Script.damage = stats.weapon.damage;
        }
        else
        {
            weapon2Sight.SetActive(false);
        }

        if (newWeapon.weaponType == CodeLibrary.weapon3)
        {
            weapon3Sight.SetActive(true);
        }
        else
        {
            weapon3Sight.SetActive(false);
        }

        if (newWeapon.weaponType == CodeLibrary.weapon4)
        {
            stillSwapping = false;
            weapon4Sight.SetActive(true);
        }
        else
        {
            if (_Weapon4Script.deployed)
            {
                stillSwapping = true;
                if (!deactivating)
                {
                    StartCoroutine(deactivateWeapon4());
                }
            }
            else
            {
                weapon4Sight.SetActive(false);
            }
        }
    }

    //Switches the players primary weapon with the offhand
    public void swapWeapons()
    {
        if (usingSecondary)
        {
            changeSprites(stats.weapon);
        }
        else
        {
            changeSprites(stats.secondary);
        }
        usingSecondary = !usingSecondary;
    }

    //Runs an attack cooldown and prevents attacking again in the specified time
    public IEnumerator StartAttackCooldown(float weaponFireRate)
    {
        attackIsAvailible = false;
        yield return new WaitForSeconds(stats.attackCDModifier * weaponFireRate);
        attackIsAvailible = true;
    }

    //Runs CD for weapon 4, since it is a non constant time
    public IEnumerator weapon4AttackCD()
    {
        attackIsAvailible = false;
        yield return new WaitWhile(() => _Weapon4Script.deployed);
        attackIsAvailible = true;
    }

    //Sets weapon4 to inactive upon returning in case it hadn't returned when swaped out
    public IEnumerator deactivateWeapon4()
    {
        deactivating = true;
        yield return new WaitWhile(() => _Weapon4Script.deployed);
        if (stillSwapping)
        {
            weapon4Sight.SetActive(false);
        }
        deactivating = false;
    }
}
