using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GSMScript : MonoBehaviour
{
    public Text healthText;
    public Text energyText;
    public Transform HPbar;

    public AudioClip tpRecovery;
    public AudioClip tpSuccess;
    public AudioClip tpFailure;
    public AudioClip enemyHit;
    public AudioClip playerHit;
    public AudioClip doorOpen;
    public AudioClip chestOpen;

    private AudioSource _audioSource;


    public PlayerManager playerManager;
    public PlayerStats playerStats;
    public int monsterDifficulty;

    private bool paused;
    public GameObject pauseMenu;

    public GameObject explosionPrefab;
    public GameObject missile;

    private bool cheatsEnabled;

    public GameObject testBoxes;

    // Start is called before the first frame update
    //Initializes variables
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag(CodeLibrary.player);
        playerManager = player.GetComponent<PlayerManager>();
        playerStats = player.GetComponent<PlayerStats>();
        _audioSource = GetComponent<AudioSource>();
        pauseMenu.SetActive(false);
        paused = false;

        cheatsEnabled = CodeLibrary.cheatsEnabled;
        if (!cheatsEnabled && testBoxes != null)
        {
            testBoxes.SetActive(false);
        }

    }

    // Update is called once per frame
    //Sets the UI text and handles general input (IE, Esc)
    void Update()
    {
        healthText.text = "Health: " + playerStats.health;
        energyText.text = "Energy: " + playerStats.currency;
        if(playerStats.health < 0)
        {
            HPbar.localScale = new Vector3(0, 20, 1);
        }
        else
        {
            HPbar.localScale = new Vector3(300 * playerStats.health / playerStats.maxHealth, 20, 1);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                pause();
            }
            else
            {
                unpause();
            }
        }

        if (cheatsEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                sceneChange(CodeLibrary.floor1Scene);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                sceneChange(CodeLibrary.floor2Scene);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                sceneChange(CodeLibrary.floor3Scene);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                sceneChange(CodeLibrary.floor4Scene);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                sceneChange(CodeLibrary.floor5Scene);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                sceneChange(CodeLibrary.floor6Scene);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                sceneChange(CodeLibrary.floor7Scene);
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                playerStats.health = playerStats.maxHealth;
            }
        }
    }

    //Pauses game and opens pause menu
    private void pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        paused = true;
    }

    //Unpauses game and closes pause menu
    private void unpause()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        paused = false;
    }

    //Function used on the pause menu "Resume" button
    public void onResumeButtonPressed()
    {
        unpause();

    }

    //Function used on the pause menu "Quit" button
    public void onQuitButtonPressed()
    {
        unpause();
        handlePlayerDeath();
    }

    //Give the player a random amount of currency called by an enemy on death
    public void lootEnemy(int tier)
    {
        if(tier == -1)
        {
            new Currency((int)(Random.value * CodeLibrary.maxMonsterDrop) + 1).loot(playerManager, null);

        }
        else if (tier == -3)
        {
            new Currency((int)((Random.value * CodeLibrary.maxBossDrop) + 1)).loot(playerManager, null);

        }
    }

    //Effects player death, Mainly returning to the title screen and wiping stats
    public void handlePlayerDeath()
    {
        playerStats.clearStats();
        SceneManager.LoadScene(CodeLibrary.TitleScene);
    }

    //Loads the scene as stated, also saves player data to be synced across floors
    public void sceneChange(string newScene)
    {
        playerStats.saveStats();
        SceneManager.LoadScene(newScene);
    }

    public void playTPRecoverySound()
    {
        _audioSource.PlayOneShot(tpRecovery);
    }

    public void playTPSuccessSound()
    {
        _audioSource.PlayOneShot(tpSuccess);
    }

    public void playTPFailSound()
    {
        _audioSource.PlayOneShot(tpFailure);
    }
    public void playEnemyHitSound()
    {
        _audioSource.PlayOneShot(enemyHit);
    }
    public void playPlayerHitSound()
    {
        _audioSource.PlayOneShot(playerHit);
    }
    public void playDoorOpenSound()
    {
        _audioSource.PlayOneShot(doorOpen);
    }
    public void playOpenChestSound()
    {
        _audioSource.PlayOneShot(chestOpen);
    }

    public void missileDead(Vector3 position)
    {
        GameObject explosion = GameObject.Instantiate(explosionPrefab);
        explosion.transform.position = position;
    }

}
