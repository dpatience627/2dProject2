using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2SpawnScript : MonoBehaviour
{
    public GameObject boss;
    Boss2Script  bossScript;

    private void Start()
    {
        bossScript = boss.GetComponent<Boss2Script>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == CodeLibrary.player && bossScript.health > 0)
        {
            boss.SetActive(true);
        }
        else if (collision.gameObject.tag == CodeLibrary.bullet && bossScript.health > 0)
        {
            boss.SetActive(true);
        }
    }

}
