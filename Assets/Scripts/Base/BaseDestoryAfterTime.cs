using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDestoryAfterTime : MonoBehaviour
{

    public float timeToDestroy = 5.0f;

    private float _currentTimeToDestroy = 0.0f;
	
	void Update ()
    {
        _currentTimeToDestroy += Time.deltaTime;
        if(_currentTimeToDestroy >= timeToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
