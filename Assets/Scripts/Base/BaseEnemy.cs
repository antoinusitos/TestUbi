using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour 
{
    public int life = 1;

    public int pointsEarn = 100;

    public Sprite type1;
    public Sprite type1_2;
    public Sprite type2;
    public Sprite type2_2;
    public Sprite type3;
    public Sprite type3_2;
    public Sprite type4;
    public Sprite type4_2;
    public Sprite type5;
    public Sprite type5_2;

    private int _typeSprite = 0;
    private bool _firstFrame = false;

    public Sprite explosion;

    public float rateChanceFire = 0.33f;
    public float minFireRate = 1.0f;
    public float maxFireRate = 2.0f;
    private float _currentfireRate = 1.0f;
    private float _currentReload = 0.0f;
    private bool _canFire = false;

    public GameObject bullet;

    private BaseFormation _parentBase;

    // accessor
    private SpriteRenderer _spriteRenderer;
    private BasePlayerScore _playerScore;

    void Start()
    {
        _currentfireRate = Random.Range(minFireRate, maxFireRate);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerScore = GameObject.FindGameObjectWithTag("Player").GetComponent<BasePlayerScore>();
    }

    public void ChangeFrame()
    {
        if (_typeSprite == 1)
        {
            _spriteRenderer.sprite = _firstFrame ? type1 : type1_2;
        }
        else if (_typeSprite == 2)
        {
            _spriteRenderer.sprite = _firstFrame ? type2 : type2_2;
        }
        else if (_typeSprite == 3)
        {
            _spriteRenderer.sprite = _firstFrame ? type3 : type3_2;
        }
        else if (_typeSprite == 4)
        {
            _spriteRenderer.sprite = _firstFrame ? type4 : type4_2;
        }
        else if (_typeSprite == 5)
        {
            _spriteRenderer.sprite = _firstFrame ? type5 : type5_2;
        }
        _firstFrame = !_firstFrame;
    }

    public void SetEnemyType(int thetype)
    {
        _typeSprite = thetype;
        if (thetype == 1)
        {
            GetComponent<SpriteRenderer>().sprite = type1;
        }
        else if (thetype == 2)
        {
            GetComponent<SpriteRenderer>().sprite = type2;
        }
        else if (thetype == 3)
        {
            GetComponent<SpriteRenderer>().sprite = type3;
        }
        else if (thetype == 4)
        {
            GetComponent<SpriteRenderer>().sprite = type4;
        }
        else if (thetype == 5)
        {
            GetComponent<SpriteRenderer>().sprite = type5;
        }
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
        _parentBase = theParentBase;
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
                if(_parentBase)
                    _parentBase.UpdateList(gameObject);

                _playerScore.AddScore(pointsEarn);

                BaseAudioManager.GetInstance().PlaySound(BaseAudioManager.audioToPlay.die);

                _spriteRenderer.sprite = explosion;
                StartCoroutine("Die");
            }
        }
    }

    void Fire()
    {
        GameObject aBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        aBullet.GetComponent<BaseEnemyBullet>().SetDirection(-Vector3.up);
        aBullet.GetComponent<BaseEnemyBullet>().SetOwner(gameObject);
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
