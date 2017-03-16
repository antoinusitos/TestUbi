using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLimitToLoose : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "EnemyBase")
        {
            GameObject.Find("GameManager").GetComponent<BaseGameManager>().Loose();
        }
    }
}
