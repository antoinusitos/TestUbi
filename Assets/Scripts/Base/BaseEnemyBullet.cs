using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyBullet : MonoBehaviour
{
    public float speed = 1.0f;
    private Vector3 _direction = Vector3.zero;

    private GameObject _owner;

    private bool _flip = true;
    private float _currentTimeToFlip = 0.0f;
    public float timeToFlip = 0.5f;

    public void SetDirection(Vector3 dir)
    {
        _direction = dir;
    }

    public GameObject GetOwner()
    {
        return _owner;
    }

    public void SetOwner(GameObject theOwner)
    {
        _owner = theOwner;
    }

    void Update()
    {
        _currentTimeToFlip += Time.deltaTime;
        if(_currentTimeToFlip >= timeToFlip)
        {
            _currentTimeToFlip = 0.0f;
            GetComponent<SpriteRenderer>().flipX = _flip;
            _flip = !_flip;
        }

        transform.position += _direction * speed * Time.deltaTime;
    }
}
