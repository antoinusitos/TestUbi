using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour 
{
    public float speed = 1.0f;
    private Vector3 _direction = Vector3.zero;

    private GameObject _owner;

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

	void Update () 
    {
        transform.position += _direction * speed * Time.deltaTime;
	}
}
