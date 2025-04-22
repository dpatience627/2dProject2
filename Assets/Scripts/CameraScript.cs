using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(CodeLibrary.player);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);
    }
}
