using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class ChestManager : MonoBehaviour, I_Interactable
{
    public CodeLibrary.LootEnum lootType = new CodeLibrary.LootEnum();
    public CodeLibrary.WeaponLootTable weaponInChest = new CodeLibrary.WeaponLootTable();
    public CodeLibrary.RelicTypes relicType = new CodeLibrary.RelicTypes();
    public CodeLibrary.currencyValues currencyValue = new CodeLibrary.currencyValues();


    private Loot contents;

    private bool displayingContents;
    private GameObject display;
    private string displayValue;

    public GameObject weapon1SpritePrefab;
    public GameObject weapon2SpritePrefab;
    public GameObject weapon3SpritePrefab;
    public GameObject weapon4SpritePrefab;
    public GameObject relicSpritePrefab;
    public GameObject CurrencySpritePrefab;
    public GameObject MedkitSpritePrefab;

    private Transform _transform;
    private Animator _ani;


    private PlayerManager pmScript;
     

    // Start is called before the first frame update
    //Sets contents based on set enums.
    //Random rolls a number to get what's chosen.
    //Only one type of item can be in any given chest
    void Start()
    {
        //Handles type of loot
        if(lootType == CodeLibrary.LootEnum.Random)
        {
            int randNum = (int)(Random.value * CodeLibrary.totalProbability);
            if(randNum < CodeLibrary.weaponChance)
            {
                lootType = CodeLibrary.LootEnum.Weapon;
            }
            else if (randNum < CodeLibrary.UtilityChance)
            {
                lootType = CodeLibrary.LootEnum.Utility;
            }
            else if (randNum < CodeLibrary.RelicChance)
            {
                lootType = CodeLibrary.LootEnum.Relic;
            }
            else if (randNum < CodeLibrary.CurrencyChance)
            {
                lootType = CodeLibrary.LootEnum.Currency;
            }
            else if (randNum < CodeLibrary.MedkitChance)
            {
                lootType = CodeLibrary.LootEnum.Medkit;
            }
        }

        //Determines what the loot actualy is
        if(lootType == CodeLibrary.LootEnum.Weapon)
        {
            contents = new Weapon((int) weaponInChest);
        }
        else if(lootType == CodeLibrary.LootEnum.Utility)
        {
            contents = new Currency(0);
        }
        else if (lootType == CodeLibrary.LootEnum.Relic)
        {
            contents = new Relic((int) relicType, -1);
        }
        else if (lootType == CodeLibrary.LootEnum.Currency || lootType == CodeLibrary.LootEnum.Medkit)
        {
            int val = 0;
            if((int)currencyValue > 0)
            {
                val = (int)currencyValue;
            }
            else if (currencyValue == CodeLibrary.currencyValues.RandomLow)
            {
                val = (int)((Random.value * 7) + 1);
            }
            else if (currencyValue == CodeLibrary.currencyValues.RandomMid)
            {
                val = (int)((Random.value * 25) + 1);
            }
            else if (currencyValue == CodeLibrary.currencyValues.RandomHigh)
            {
                val = (int)((Random.value * 50) + 1);
            }

            if(lootType == CodeLibrary.LootEnum.Currency)
            {
                contents = new Currency(val);
            }
            else
            {
                contents = new Medkit(val);
            }
        }
        else
        {
            contents = new Currency(0);
        }

        //Sets other variables
        pmScript = null;
        displayingContents = false;
        _transform = transform;
        _ani = GetComponent<Animator>();
        display = null;
        displayValue = "";
        _ani.SetBool("HasOpened", false);
    }

    //Handles interaction, first interaction reveals contents second loots the chest
    public void interact()
    {
        if (contents != null)
        {
            _ani.SetBool("HasOpened", true);
            if (displayingContents)
            {
                contents.loot(pmScript, this);
            }
            else
            {
                displayingContents = true;
                pmScript.gsmScript.playOpenChestSound();
            }
            refreshDisplay(contents.getDisplay());
        }
        //print(contents.getDisplay());

    }

    //Helper method for chest
    public Loot getContents()
    {
        return contents;
    }

    //Loads the correct display item, particularly in case of weapon replacement
    public void refreshDisplay(string item)
    {

        if (displayValue != item)
        {

            Destroy(display);
            displayValue = item;

            if (item.Equals(CodeLibrary.weapon1))
            {
                display = Instantiate(weapon1SpritePrefab, (Vector2)_transform.position, Quaternion.identity);
                
            }
            else if (item.Equals(CodeLibrary.weapon2))
            {
                display = Instantiate(weapon2SpritePrefab, (Vector2)_transform.position, Quaternion.identity);
            }
            else if (item.Equals(CodeLibrary.weapon3))
            {
                display = Instantiate(weapon3SpritePrefab, (Vector2)_transform.position, Quaternion.identity);
            }
            else if (item.Equals(CodeLibrary.weapon4))
            {
                display = Instantiate(weapon4SpritePrefab, (Vector2)_transform.position, Quaternion.identity);
            }
            else if (item.Equals(CodeLibrary.utility))
            {
                display = null;
            }
            else if (item.Equals(CodeLibrary.relic))
            {
                display = Instantiate(relicSpritePrefab, (Vector2)_transform.position, Quaternion.identity);
            }
            else if (item.Equals(CodeLibrary.currency))
            {
                display = Instantiate(CurrencySpritePrefab, (Vector2)_transform.position, Quaternion.identity);
            }
            else if (item.Equals(CodeLibrary.medkit))
            {
                display = Instantiate(MedkitSpritePrefab, (Vector2)_transform.position, Quaternion.identity);
            }
            else
            {
                display = null;
            }

            if(display != null)
            {
                display.transform.GetChild(0).GetComponent<TextMesh>().text = contents.getDisplayText();
            }
        }
    }

    //Exchanges the contents with a different item
    public Loot replaceContents(Loot newContents)
    {
        Loot oldContents = contents;
        contents = newContents;
        return oldContents;
    }


    //triggers if the player enters range of the chest
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == CodeLibrary.player)
        {
            pmScript = collision.gameObject.GetComponent<PlayerManager>();
            if(pmScript != null)
            {
                pmScript.canInteractWith(this);
            }
        }
    }

    //tells the playerManager that the player is no longer in range
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == CodeLibrary.player)
        {
            pmScript = collision.gameObject.GetComponent<PlayerManager>();
            if (pmScript != null)
            {
                pmScript.nowOutOfRange(this);
            }
        }
    }
}
