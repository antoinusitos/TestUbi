using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerMovement : MonoBehaviour 
{
    // Movement
    public KeyCode rightMovementKey = KeyCode.D;
    public KeyCode leftMovementKey = KeyCode.Q;
    public float speed = 2.0f;
    public float limitScreenBorder = 1.0f;

    // Shoot
    public KeyCode shootKey = KeyCode.Space;
    public float reloadSpeed = 1.0f;
    public GameObject bullet;
    public GameObject fireStart;

    private float _currentReload = 0.0f;
    private bool _canShoot = false;
    private bool _canMove = false;

	// Update is called once per frame
	void Update () 
    {
        if (!_canMove) return;

        if (!_canShoot)
        {
            _currentReload += Time.deltaTime;
            if(_currentReload >= reloadSpeed)
            {
                _currentReload = 0.0f;
                _canShoot = true;
            }
        }

        if (Input.GetKey(rightMovementKey))
        {
            Vector3 tempPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(limitScreenBorder,0,0) + new Vector3(speed * Time.deltaTime, 0.0f, 0.0f));
            if (tempPos.x < Screen.width)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
            }
        }

        if (Input.GetKey(leftMovementKey))
        {
            Vector3 tempPos = Camera.main.WorldToScreenPoint(transform.position - new Vector3(limitScreenBorder, 0, 0) - new Vector3(speed * Time.deltaTime, 0.0f, 0.0f));
            if (tempPos.x > 0)
            {
                transform.position -= new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
            }
        }

        if (Input.GetKey(shootKey) && _canShoot)
        {
            if (bullet)
            {
                _canShoot = false;
                BaseAudioManager.GetInstance().PlaySound(BaseAudioManager.audioToPlay.fire);
                GameObject aBullet = Instantiate(bullet, fireStart.transform.position, Quaternion.identity);
                aBullet.GetComponent<BaseBullet>().SetDirection(Vector3.up);
                aBullet.GetComponent<BaseBullet>().SetOwner(gameObject);
            }
            else
            {
                Debug.LogError("No Bullet Prefab in " + name);
            }
        }
	}

    public void SetStuck(bool newState)
    {
        _canMove = !newState;
    }

    public void SetcanShoot(bool newState)
    {
        _canShoot = newState;
    }
}
