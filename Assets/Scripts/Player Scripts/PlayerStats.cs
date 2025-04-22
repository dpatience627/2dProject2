using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Functions as a central clearing house for all player stats
public class PlayerStats : MonoBehaviour
{

    public Weapon weapon;
    public Weapon secondary;
    public int health;
    public int maxHealth;
    public float movementSpeed;
    public float attackCDModifier;
    public float weaponDamageModifier;
    public float bulletSpeedModifier;
    public float currencyRatio;

    public int currency;

    //NOT SAVED (yet)
    public float tpCD;
    public float tpEndLag;
    public float maxTPDistance;

    //Initializes weapons and stats upon loading a new scene

    private void Awake()
    {
        tpCD = CodeLibrary.teleportCD;
        tpEndLag = CodeLibrary.teleportEndLag;
        maxTPDistance = CodeLibrary.maxTeleportDistance;

        weapon = new Weapon(-1);
        secondary = new Weapon(1);
    }




    private void Start()
    {
        if (PlayerPrefs.HasKey(CodeLibrary.prefsHasStats) && PlayerPrefs.GetInt(CodeLibrary.prefsHasStats) == 1)
        {
            loadStats();
            if (weapon.getWeaponCode() == -1)
            {
                StartCoroutine(swapWeaponOver());
            }
        }
        else
        {
            this.gameObject.GetComponent<PlayerWeaponScript>().swapWeapons();
        }
    }
    private void Update()
    {
        if(currency < 0)
        {
            currency = 0;
        }
    }

    //Deals damage to the player
    public void takeDamage(int damage)
    {
        health -= damage;
    }

    //Saves the player's stats in playerPrefs
    public void saveStats()
    {
        //print("saving");

        PlayerPrefs.SetInt(CodeLibrary.prefsWeapon, weapon.getWeaponCode());
        //PlayerPrefs.SetInt(CodeLibrary.prefsSecondary, weapon.getWeaponCode());

        PlayerPrefs.SetInt(CodeLibrary.prefsHealth, health);

        PlayerPrefs.SetInt(CodeLibrary.prefsMaxHealth, maxHealth);
        PlayerPrefs.SetFloat(CodeLibrary.prefsMovementSpeed, movementSpeed);
        PlayerPrefs.SetFloat(CodeLibrary.prefsAttackCDModifier, attackCDModifier);
        PlayerPrefs.SetFloat(CodeLibrary.prefsWeaponDamageModifier, weaponDamageModifier);
        PlayerPrefs.SetFloat(CodeLibrary.prefsBulletSpeedModifier, bulletSpeedModifier);
        PlayerPrefs.SetFloat(CodeLibrary.prefsCurrencyRatio, currencyRatio);

        PlayerPrefs.SetInt(CodeLibrary.prefsCurrency, currency);

        if (PlayerPrefs.HasKey(CodeLibrary.prefsHighScore))
        {
            PlayerPrefs.SetInt(CodeLibrary.prefsHighScore, Mathf.Max(currency, PlayerPrefs.GetInt(CodeLibrary.prefsHighScore)));
        }
        else
        {
            PlayerPrefs.SetInt(CodeLibrary.prefsHighScore, currency);
        }

        PlayerPrefs.SetInt(CodeLibrary.prefsHasStats, 1);
    }


    //Wipes the player's stats in playerPrefs but saves their highScore
    public void clearStats()
    {
        PlayerPrefs.SetInt(CodeLibrary.prefsHasStats, 0);

        PlayerPrefs.SetInt(CodeLibrary.prefsWeapon, -1);

        PlayerPrefs.SetInt(CodeLibrary.prefsHealth, 0);

        PlayerPrefs.SetInt(CodeLibrary.prefsMaxHealth, 0);
        PlayerPrefs.SetFloat(CodeLibrary.prefsMovementSpeed, 0);
        PlayerPrefs.SetFloat(CodeLibrary.prefsAttackCDModifier, 0);
        PlayerPrefs.SetFloat(CodeLibrary.prefsWeaponDamageModifier, 0);
        PlayerPrefs.SetFloat(CodeLibrary.prefsBulletSpeedModifier, 0);
        PlayerPrefs.SetFloat(CodeLibrary.prefsCurrencyRatio, 0);

        PlayerPrefs.SetInt(CodeLibrary.prefsCurrency, 0);

        if (PlayerPrefs.HasKey(CodeLibrary.prefsHighScore))
        {
            PlayerPrefs.SetInt(CodeLibrary.prefsHighScore, Mathf.Max(currency, PlayerPrefs.GetInt(CodeLibrary.prefsHighScore)));
        }
        else
        {
            PlayerPrefs.SetInt(CodeLibrary.prefsHighScore, currency);
        }
    }

    //Loads the player's stats from playerPrefs, usually upon loading a new level
    public void loadStats()
    {
        if (PlayerPrefs.HasKey(CodeLibrary.prefsHasStats) && PlayerPrefs.GetInt(CodeLibrary.prefsHasStats) == 1)
        {

            this.gameObject.GetComponent<PlayerWeaponScript>().changePrimary(new Weapon(PlayerPrefs.GetInt(CodeLibrary.prefsWeapon)));

            health = PlayerPrefs.GetInt(CodeLibrary.prefsHealth);
            maxHealth = PlayerPrefs.GetInt(CodeLibrary.prefsMaxHealth);
            movementSpeed = PlayerPrefs.GetFloat(CodeLibrary.prefsMovementSpeed);
            attackCDModifier = PlayerPrefs.GetFloat(CodeLibrary.prefsAttackCDModifier);
            weaponDamageModifier = PlayerPrefs.GetFloat(CodeLibrary.prefsWeaponDamageModifier);
            bulletSpeedModifier = PlayerPrefs.GetFloat(CodeLibrary.prefsBulletSpeedModifier);
            currencyRatio = PlayerPrefs.GetFloat(CodeLibrary.prefsCurrencyRatio);
            currency = PlayerPrefs.GetInt(CodeLibrary.prefsCurrency);

            PlayerPrefs.SetInt(CodeLibrary.prefsHasStats, 1);
        }
    }

    override
    public string ToString()
    {
        return "Health: " + health +
        ", MaxHealth: " + maxHealth +
        ", MovementSpeed: " + movementSpeed +
        ", AttackCDModifier: " + attackCDModifier +
        ", WeaponDamageModifier: " + weaponDamageModifier +
        ", BulletSpeedModifier: " + bulletSpeedModifier +
        ", CurrencyRatio: " + currencyRatio +
        ", Currency:" + currency;
    }

    //If the player spawns with 
    private IEnumerator swapWeaponOver()
    {
        yield return new WaitForEndOfFrame();
        this.gameObject.GetComponent<PlayerWeaponScript>().swapWeapons();
    }


}
