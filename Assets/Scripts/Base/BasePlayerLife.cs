using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePlayerLife : MonoBehaviour 
{
    public int life = 3;
    public Text lifeText;
    public GameObject looseText;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<BaseBullet>() && collision.transform.GetComponent<BaseBullet>().GetOwner() != gameObject)
        {
            life--;
            if (lifeText)
                lifeText.text = "Life : " + life;

            Destroy(collision.gameObject);
            if (life <= 0)
            {
                if (looseText)
                    looseText.SetActive(true);

                GameObject.FindGameObjectWithTag("BaseFormation").GetComponent<BaseFormation>().SetCanMove(false);

                GetComponent<BasePlayerMovement>().SetStuck(true);
            }
        }
    }
}
