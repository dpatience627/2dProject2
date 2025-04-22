using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneManagerScript : MonoBehaviour
{
    public Canvas TitleScreen;
    public Canvas ControlScreen;
    public Text highScore;



    // Start is called before the first frame update
    void Start()
    {
        TitleScreen.enabled = true;
        ControlScreen.enabled = false;

        if (PlayerPrefs.HasKey(CodeLibrary.prefsHighScore))
        {
            highScore.text = "High Score: " + PlayerPrefs.GetInt(CodeLibrary.prefsHighScore) + " Energy";
        }
        else
        {
            highScore.text = "High Score: 0 Energy";
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            back();
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter) && TitleScreen.enabled){
            OnStartButtonPress();
        }
    }

    public void OnStartButtonPress()
    {
        SceneManager.LoadScene(CodeLibrary.floor1Scene);
    }

    public void onQuitButtonPress()
    {
        Application.Quit();
    }

    private void back()
    {
        if (TitleScreen.enabled)
        {
            onQuitButtonPress();
        }
        else if (ControlScreen.enabled)
        {
            onTitleScreenButton();
        }
    }

    public void onControlsButtonPress()
    {
        TitleScreen.enabled = false;
        ControlScreen.enabled = true;
    }

    public void onTitleScreenButton()
    {
        TitleScreen.enabled = true;
        ControlScreen.enabled = false;
    }

}
