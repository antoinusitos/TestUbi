using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : MonoBehaviour 
{
    public int life = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<BaseBullet>())
        {
            life--;
            Destroy(collision.gameObject);
            if(life <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
