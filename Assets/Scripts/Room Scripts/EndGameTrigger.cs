using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Allows for player to beat the game.
/// </summary>
public class EndGameTrigger : MonoBehaviour
{
    GSMScript gsmScript;
    public PlayerManager _pM;

    public GameObject _victoryText;
    public GameObject _player;
    public GameObject _HUD;
    public GameObject _weapon4;

    Transform _playerPlace;
    Transform _victoryPlace;
    Rigidbody2D _playerMove;

    bool gameEnded;

    /// <summary>
    /// Makes connection to Game Scene Manager to allow scene changing.
    /// </summary>
    void Start()
    {
        gsmScript = GameObject.FindGameObjectWithTag(CodeLibrary.SceneManager).GetComponent<GSMScript>();
        _playerPlace = _player.GetComponent<Transform>();
        _victoryPlace = _victoryText.GetComponent<Transform>();
        _playerMove = _player.GetComponent<Rigidbody2D>();
        gameEnded = false;
    }

    private void Update()
    {
        if(gameEnded)
        {
            //Making sure the player can't move and only the text is seen.
            _victoryText.SetActive(true);
            _HUD.SetActive(false);
            _weapon4.SetActive(false);
            _player.GetComponent<SpriteRenderer>().enabled = false;
            _player.GetComponent<CircleCollider2D>().enabled = false;

            foreach(Transform child in _playerPlace)
            {
                child.gameObject.SetActive(false);
            }

            _playerMove.velocity = new Vector2((_victoryPlace.position.x - _playerPlace.position.x), (_victoryPlace.position.y - _playerPlace.position.y));

            if(_playerMove.velocity.magnitude < 0.5f)
            {
                StartCoroutine(endGame());
            }
        }
    }

    /// <summary>
    /// Once player enters trigger, ends game cinematically before changing scene to title.
    /// </summary>
    /// <param name="collision">Thing that engers trigger.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.tag == CodeLibrary.player)
        {
            _pM.canAct = false;
            gameEnded = true;
        }
    }

    private IEnumerator endGame()
    {
        yield return new WaitForSeconds(5);
        gsmScript.sceneChange(CodeLibrary.TitleScene);
    }
}
