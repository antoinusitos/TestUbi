using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovementEnemy : MonoBehaviour {

    public float speed = 1.0f;
    private Vector3 _direction = Vector3.zero;

    public void SetDirection(Vector3 dir)
    {
        _direction = dir;
    }

    void Update()
    {
        transform.position += _direction * speed * Time.deltaTime;

        Vector3 tempPos = Camera.main.WorldToScreenPoint(transform.position);
        if (tempPos.x >= Screen.width)
        {
            Destroy(gameObject);
        }
    }
}
