using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine;





public abstract class Loot
{
    public abstract void loot(PlayerManager pmScript, ChestManager cmScript);
    public abstract string getDisplay();

    public abstract string getDisplayText();
}


public class Weapon : Loot
{
    public string weaponType;
    public int damage;
    public float attackSpeed;
    public float bulletSpeed;

    private static int numWeapons = 3;

    // 0: Random, 1: Weapon1, 2: Weapon2, 3: Weapon3, Else weapon == ""
    public Weapon(int weaponNum)
    {
        if (weaponNum == 0)
        {
            weaponNum = (int)((UnityEngine.Random.value * numWeapons) + 2);
        }

        if (weaponNum == 1)
        {
            weaponType = CodeLibrary.weapon1;
            damage = CodeLibrary.weapon1Damage;
            attackSpeed = CodeLibrary.weapon1AttackSpeed;
            bulletSpeed = CodeLibrary.weapon1bulletSpeed;
        }
        else if (weaponNum == 2)
        {
            weaponType = CodeLibrary.weapon2;
            damage = CodeLibrary.weapon2Damage;
            attackSpeed = 0;
            bulletSpeed = 0;

        }
        else if (weaponNum == 3)
        {
            weaponType = CodeLibrary.weapon3;
            damage = CodeLibrary.weapon3Damage;
            attackSpeed = CodeLibrary.weapon3AttackSpeed;
            bulletSpeed = CodeLibrary.weapon3bulletSpeed;
        }
        else if (weaponNum == 4)
        {
            weaponType = CodeLibrary.weapon4;
            damage = CodeLibrary.weapon4Damage;
            attackSpeed = CodeLibrary.weapon4AttackSpeed;
            bulletSpeed = CodeLibrary.weapon4Speed;
        }
        else
        {
            weaponType = "";
            damage = 0;
            attackSpeed = 0;
            bulletSpeed = 0;

        }
    }

    override
    public void loot(PlayerManager pmScript, ChestManager cmScript)
    {
        cmScript.replaceContents(pmScript.collectWeapon(this));
    }

    override
    public string getDisplay()
    {
        return weaponType;
    }

    override
    public string getDisplayText()
    {
        return weaponType;
    }

    public int getWeaponCode()
    {
        if (weaponType == CodeLibrary.weapon1)
        {
            return 1;
        }
        else if (weaponType == CodeLibrary.weapon2)
        {
            return 2;
        }
        else if (weaponType == CodeLibrary.weapon3)
        {
            return 3;
        }
        else if (weaponType == CodeLibrary.weapon4)
        {
            return 4;
        }
        return -1;
    }

    override
    public string ToString()
    {
        return "WeaponType: " + weaponType + ", Damage: " + damage + ", AttackSpeed: " + attackSpeed + ", BulletSpeed: " + bulletSpeed;
    }
}

/**
public class Utility : Loot 
{

}
**/

public class Relic : Loot
{
    int type;
    float strength;
    int numRelicTypes = 6;
    bool expended;

    public Relic(int type, float strength)
    {
        if (type == 0)
        {
            type = (int)((UnityEngine.Random.value * numRelicTypes) + 1);
        }
        else
        {
            this.type = type;
        }
        this.type = type;
        this.strength = strength;
        expended = false;

    }

    override
    public void loot(PlayerManager pmScript, ChestManager cmScript)
    {
        if (expended)
        {
            return;
        }

        if (type == 1)
        {
            pmScript.getStatsScript().maxHealth += CodeLibrary.maxHealthRelic;
            pmScript.getStatsScript().health += CodeLibrary.maxHealthRelic;
        }
        else if (type == 2)
        {
            pmScript.getStatsScript().movementSpeed += CodeLibrary.MovementSpeedRelic;
        }
        else if (type == 3)
        {
            pmScript.getStatsScript().attackCDModifier -= CodeLibrary.AttackCDRelic;
        }
        else if (type == 4)
        {
            pmScript.getStatsScript().weaponDamageModifier += CodeLibrary.WeaponDamageRelic;
        }
        else if (type == 5)
        {
            pmScript.getStatsScript().bulletSpeedModifier += CodeLibrary.BulletSpeedRelic;
        }
        else if (type == 6)
        {
            pmScript.getStatsScript().currencyRatio += CodeLibrary.CurrencyRatioRelic;
        }
        expended = true;
    }

    override
    public string getDisplay()
    {
        if (!expended)
        {
            return CodeLibrary.relic;
        }
        return "";
    }

    override
    public string getDisplayText()
    {
        return (CodeLibrary.RelicTypes) type + " Relic";
    }

    override
    public string ToString()
    {
        return "RelicType: " + type + ", Strength: " + strength + ", Expended: " + expended;
    }
}

public class Currency : Loot
{
    private int value;

    public Currency(int value)
    {
        this.value = value;
    }


    override
    public void loot(PlayerManager pmScript, ChestManager cmScript)
    {
        pmScript.getStatsScript().currency += (int)(value * pmScript.getStatsScript().currencyRatio);
        value = 0;
    }

    override
    public string getDisplay()
    {
        if (value != 0)
        {
            return CodeLibrary.currency;
        }
        return "";
    }

    override
    public string getDisplayText()
    {
        return "Energy: " + value;
    }

    override
    public string ToString()
    {
        return "Currency Value: " + value;
    }
}

public class Medkit : Loot
{
    private int value;

    public Medkit(int value)
    {
        this.value = value;
    }


    override
    public void loot(PlayerManager pmScript, ChestManager cmScript)
    {
        pmScript.getStatsScript().health += value;
        value = 0;

        if (pmScript.getStatsScript().health > pmScript.getStatsScript().maxHealth)
        {
            pmScript.getStatsScript().health = pmScript.getStatsScript().maxHealth;
        }

    }

    override
    public string getDisplay()
    {
        if (value != 0)
        {
            return CodeLibrary.medkit;
        }
        return "";
    }

    override
    public string getDisplayText()
    {
        return "Medkit: " + value;
    }

    override
    public string ToString()
    {
        return "Medkit Value: " + value;
    }
}

