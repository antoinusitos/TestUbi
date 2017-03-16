using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePlayerLife : MonoBehaviour 
{
    public int lifeMax = 3;
    private int life = 3;
    public Text lifeText;
    public GameObject loseText;

    private BaseGameManager _gameManager;
    private GameObject _playerSpawner;

    private void Start()
    {
        life = lifeMax;
        _gameManager = GameObject.Find("GameManager").GetComponent<BaseGameManager>();
        _playerSpawner = GameObject.FindGameObjectWithTag("PlayerSpawner");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<BaseEnemyBullet>() && collision.transform.GetComponent<BaseEnemyBullet>().GetOwner() != gameObject)
        {
            life--;
            if (lifeText)
                lifeText.text = "Lives: " + life;

            BaseAudioManager.GetInstance().PlaySound(BaseAudioManager.audioToPlay.playerDie);

            Destroy(collision.gameObject);

            transform.position = _playerSpawner.transform.position;

            if (life <= 0)
            {
                _gameManager.Lose();
            }
        }
    }

    public void RefillPlayerLife()
    {
        life = lifeMax;
        if (lifeText)
            lifeText.text = "Lives: " + life;
    }
}
