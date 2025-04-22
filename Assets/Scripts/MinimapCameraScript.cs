using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraScript : MonoBehaviour
{
    private Transform _transform;
    private Transform _playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        _playerTransform = _transform.parent.transform.GetChild(0).transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        _transform.position = new Vector3(_playerTransform.position.x, _playerTransform.position.y, -50);
    }
}
