using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePlayerLife : MonoBehaviour 
{
    public int lifeMax = 3;
    private int life = 3;
    public Text lifeText;
    public GameObject looseText;

    private void Start()
    {
        life = lifeMax;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<BaseBullet>() && collision.transform.GetComponent<BaseBullet>().GetOwner() != gameObject)
        {
            life--;
            if (lifeText)
                lifeText.text = "Lives: " + life;

            Destroy(collision.gameObject);

            transform.position = GameObject.FindGameObjectWithTag("PlayerSpawner").transform.position;

            if (life <= 0)
            {
                GameObject.Find("GameManager").GetComponent<BaseGameManager>().Loose();

            }
        }
    }

    public void RefillPlayerLife()
    {
        life = lifeMax;
    }
}
