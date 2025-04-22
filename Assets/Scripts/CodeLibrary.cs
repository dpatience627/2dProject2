using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeLibrary
{

    public static bool cheatsEnabled = false;

    //General place to store information and game data

    //Item tags
    public static string weapon1 = "Electro Rifle";
    public static string weapon2 = "Energy Sword";
    public static string weapon3 = "Photon Bounce Cannon";
    public static string weapon4 = "Electric Hurricane";
    public static string utility = "Utility";
    public static string relic = "Relic";
    public static string currency = "Currency";
    public static string medkit = "Medkit";

    //Weapon 1 stats
    public static int weapon1Damage = 2;
    public static float weapon1AttackSpeed = 0.4f;
    public static float weapon1bulletSpeed = 10;
    public static int weapon1cost = 0;

    //Weapon 2 stats
    public static int weapon2Damage = 5;
    public static int weapon2cost = 0;

    //Weapon 3 stats
    public static int weapon3Damage = 2;
    public static float weapon3AttackSpeed = 0.3f;
    public static float weapon3bulletSpeed = 10;
    public static int weapon3NumBounces = 3;
    public static int weapon3cost = 0;

    //Weapon 4 stats
    public static int weapon4Damage = 4;
    public static float weapon4AttackSpeed = 2f;
    public static float weapon4Speed = 10;
    public static int weapon4cost = 0;


    //Player Movement Stats
    public static float teleportCD = 3;
    public static float teleportEndLag = 0.5f;
    public static float maxTeleportDistance = 100;

    //Other player data
    public static float iFramesDuration = 0.25f;
    public static float hitKB = 100;

    //Object Tags
    public static string player = "Player";
    public static string bullet = "Bullet";
    public static string enemy = "Enemy";
    public static string wall = "Wall";
    public static string trigger = "RoomSpawnTrigger";
    public static string floor = "Floor";
    public static string noCollision = "noCollision";
    public static string chest = "chest";
    public static string SceneManager = "SceneManager";
    public static string Laser = "Laser";

    //Floor IDs
    public static string floor1Scene = "Floor1";
    public static string floor2Scene = "Floor2";
    public static string floor3Scene = "Floor3";
    public static string floor4Scene = "Floor4";
    public static string floor5Scene = "Floor5";
    public static string floor6Scene = "Floor6";
    public static string floor7Scene = "Floor7";
    public static string TitleScene = "TitleScene";

    //LayerMasks
    //public static LayerMask wallLayerMask = LayerMask.NameToLayer("wall");
    //public static LayerMask AntiTeleportWallLayerMask = LayerMask.NameToLayer("wall"); //LayerMask.NameToLayer("Anti-TeleportWall");
    //public static LayerMask FloorLayerMask = LayerMask.NameToLayer("BackgroundFloor");


    //Player prefs keys
    public static string prefsWeapon = "playerWeapon";
    public static string prefsSecondary = "playerSecondary";
    public static string prefsHealth = "playerHealth";
    public static string prefsMaxHealth = "playerMaxHealth" ;
    public static string prefsMovementSpeed = "playerMovementSpeed";
    public static string prefsAttackCDModifier = "playerAttackCD";
    public static string prefsWeaponDamageModifier = "playerWeaponDmg";
    public static string prefsBulletSpeedModifier = "playerBulletSpeed";
    public static string prefsCurrencyRatio = "playerCurrencyRatio";
    public static string prefsCurrency = "playerCurrency";

    public static string prefsHasStats = "playerHasStats";
    public static string prefsHighScore = "playerHighScore";


    //Currency Drop Rates
    public static int maxMonsterDrop = 4;
    public static int maxBossDrop = 50;



    //Chest Loot Options:
    public enum LootEnum
    {
        Weapon,
        Utility,
        Relic,
        Currency,
        Medkit,
        Random
    };

    //Loot Chances
    public static int weaponChance = 8;
    public static int UtilityChance = 0 + weaponChance;
    public static int RelicChance = 20 + UtilityChance;
    public static int CurrencyChance = 30 + RelicChance;
    public static int MedkitChance = 20 + CurrencyChance;
    public static int totalProbability = MedkitChance;

    public enum WeaponLootTable
    {
        Random,
        ElectroRifle,
        EnergySword,
        PhotonBounceCannon,
        ElectricHurricane,
        None
    };

    public enum UtilitiesLootTable
    {

    };
    public enum RelicTypes
    {
        Random,
        MaxHealth,
        MovementSpeed,
        AttackCD,
        WeaponDamage,
        BulletSpeed,
        currencyRatio,
        None
    };

    //Relic stats 
    public static int maxHealthRelic = 10;
    public static float MovementSpeedRelic = 0.5f;
    public static float AttackCDRelic = 0.1f;
    public static float WeaponDamageRelic = 1;
    public static float BulletSpeedRelic = 1;
    public static float CurrencyRatioRelic = 0.2f;

    //Currency Values
    public enum currencyValues
    {
        One = 1,
        Five = 5,
        Ten = 10,
        TwentyFive = 25,
        Fifty = 50,
        RandomLow = -1,
        RandomMid = -2,
        RandomHigh = -3
    }

    //Room Random Operations
    public static float puzzleStairs = .5f;
    public static float goodLuck = 1 + puzzleStairs;
    public static float chestRoom = 1 + goodLuck;
    public static float basicRoom = 5 + goodLuck;
    public static float totalRoomProb = basicRoom;



}
