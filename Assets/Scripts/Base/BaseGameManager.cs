using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseGameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject specialEnemiesspawner;
    public GameObject textLoose;
    public GameObject textWin;
    public GameObject FormationPrefab;
    public GameObject playerSpawner;
    public GameObject startText;
    public GameObject obstacle;

    private GameObject _instancedObstable;
    private GameObject _instancedFormation;

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
    }

    public void Loose()
    {
        _currentGameState = gameState.menu;
        if (textLoose)
            textLoose.SetActive(true);

        GameObject.FindGameObjectWithTag("BaseFormation").GetComponent<BaseFormation>().SetCanMove(false);

        player.GetComponent<BasePlayerMovement>().SetStuck(true);
        player.GetComponent<BasePlayerMovement>().SetcanShoot(false);
        specialEnemiesspawner.GetComponent<BaseSpecialEnemySpawner>().SetCanSpawn(false);
    }

    public void Win()
    {
        _currentGameState = gameState.menu;
        if (textWin)
            textWin.SetActive(true);

        GameObject.FindGameObjectWithTag("BaseFormation").GetComponent<BaseFormation>().SetCanMove(false);

        player.GetComponent<BasePlayerMovement>().SetStuck(true);
        player.GetComponent<BasePlayerMovement>().SetcanShoot(false);
        specialEnemiesspawner.GetComponent<BaseSpecialEnemySpawner>().SetCanSpawn(false);
    }

    public void StartGame()
    {
        _currentGameState = gameState.inGame;

        startText.SetActive(false);
        textLoose.SetActive(false);
        textWin.SetActive(false);

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _currentGameState == gameState.menu)
        {
            StartGame();
        }
    }
}
