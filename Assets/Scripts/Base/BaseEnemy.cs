using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour 
{
    public int life = 1;

    public int pointsEarn = 100;

    public float rateChanceFire = 0.33f;
    public float minFireRate = 1.0f;
    public float maxFireRate = 2.0f;
    private float _currentfireRate = 1.0f;
    private float _currentReload = 0.0f;
    private bool _canFire = false;

    public GameObject bullet;

    private BaseFormation parentBase;

    void Start()
    {
        _currentfireRate = Random.Range(minFireRate, maxFireRate);
    }

    void Update()
    {
        if(_canFire)
        {
            _currentReload += Time.deltaTime;
            if (_currentReload >= _currentfireRate)
            {
                _currentReload = 0.0f;
                _currentfireRate = Random.Range(minFireRate, maxFireRate);
                float rand = Random.Range(0.0f, 1.0f);
                if (rand <= rateChanceFire)
                    Fire();
            }
        }
    }

    public void SetParentBase(BaseFormation theParentBase)
    {
        parentBase = theParentBase;
    }

    public void SetCanFire(bool newState)
    {
        _canFire = newState;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<BaseBullet>() && collision.transform.GetComponent<BaseBullet>().GetOwner() != gameObject)
        {
            life--;
            Destroy(collision.gameObject);
            if (life <= 0)
            {
                parentBase.UpdateList(gameObject);

                GameObject.FindGameObjectWithTag("Player").GetComponent<BasePlayerScore>().AddScore(pointsEarn);

                Destroy(gameObject);
            }
        }
    }

    void Fire()
    {
        GameObject aBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
        aBullet.GetComponent<BaseBullet>().SetDirection(-Vector3.up);
        aBullet.GetComponent<BaseBullet>().SetOwner(gameObject);
    }
}
