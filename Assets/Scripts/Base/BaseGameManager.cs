using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseGameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject specialEnemiesspawner;
    public GameObject textLose;
    public GameObject textWin;
    public GameObject FormationPrefab;
    public GameObject playerSpawner;
    public GameObject startText;
    public GameObject obstacle;

    public GameObject scoreText;
    public GameObject livesText;

    private GameObject _instancedObstable;
    private GameObject _instancedFormation;

    private BaseAudioManager _audioManager;

    enum gameState
    {
        menu,
        inGame,
    };

    private gameState _currentGameState = gameState.menu;

    void Start()
    {
        startText.SetActive(true);
        player.GetComponent<BasePlayerMovement>().SetStuck(true);

        _audioManager = BaseAudioManager.GetInstance();
    }

    public void Lose()
    {
        _currentGameState = gameState.menu;
        if (textLose)
            textLose.SetActive(true);

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("BulletBase");
        for(int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i]);
        }

        _audioManager.StopBackground();
        _audioManager.PlaySound(BaseAudioManager.audioToPlay.lose);

        GameObject.FindGameObjectWithTag("BaseFormation").GetComponent<BaseFormation>().SetCanMove(false);

        player.GetComponent<BasePlayerMovement>().SetStuck(true);
        player.GetComponent<BasePlayerMovement>().SetcanShoot(false);
        specialEnemiesspawner.GetComponent<BaseSpecialEnemySpawner>().SetCanSpawn(false);
        specialEnemiesspawner.GetComponent<BaseSpecialEnemySpawner>().StopLastSpawned();
    }

    public void Win()
    {
        _currentGameState = gameState.menu;
        if (textWin)
            textWin.SetActive(true);

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("BulletBase");
        for (int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i]);
        }

        _audioManager.StopBackground();
        _audioManager.PlaySound(BaseAudioManager.audioToPlay.win);

        GameObject.FindGameObjectWithTag("BaseFormation").GetComponent<BaseFormation>().SetCanMove(false);

        player.GetComponent<BasePlayerMovement>().SetStuck(true);
        player.GetComponent<BasePlayerMovement>().SetcanShoot(false);
        specialEnemiesspawner.GetComponent<BaseSpecialEnemySpawner>().SetCanSpawn(false);
    }

    public void StartGame()
    {
        _currentGameState = gameState.inGame;

        _audioManager.PlaySound(BaseAudioManager.audioToPlay.start);
        StartCoroutine(waitDelay(2));

        startText.SetActive(false);
        textLose.SetActive(false);
        textWin.SetActive(false);
        scoreText.SetActive(true);
        livesText.SetActive(true);

        specialEnemiesspawner.GetComponent<BaseSpecialEnemySpawner>().DestroyLastSpawned();
        specialEnemiesspawner.GetComponent<BaseSpecialEnemySpawner>().SetCanSpawn(true);

        if (_instancedFormation)
            Destroy(_instancedFormation);
        _instancedFormation = Instantiate(FormationPrefab);

        if (_instancedObstable)
            Destroy(_instancedObstable);
        _instancedObstable = Instantiate(obstacle);

        player.transform.position = playerSpawner.transform.position;
        player.GetComponent<BasePlayerMovement>().SetStuck(false);
        player.GetComponent<BasePlayerScore>().ResetScore();
        player.GetComponent<BasePlayerLife>().RefillPlayerLife();


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _currentGameState == gameState.menu)
        {
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator waitDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        _audioManager.PlayBackground();
    }
}
