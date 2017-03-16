using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpecialEnemySpawner : MonoBehaviour
{
    public float spawnTimeMin = 5.0f;
    public float spawnTimeMax = 8.0f;
    private float _currentspawnReload = 0.0f;
    private float _currentSpawntime = 0.0f;
    private bool _canSpawn = false;

    public GameObject prefabEnemy;

    void Start()
    {
        _currentSpawntime = Random.Range(spawnTimeMin, spawnTimeMax);
    }

	void Update ()
    {
        if (_canSpawn)
        {
            _currentspawnReload += Time.deltaTime;

            if (_currentspawnReload >= _currentSpawntime)
            {
                _currentspawnReload = 0.0f;
                _currentSpawntime = Random.Range(spawnTimeMin, spawnTimeMax);
                GameObject enemy = (GameObject)Instantiate(prefabEnemy, transform.position, Quaternion.identity);
                enemy.GetComponent<BaseMovementEnemy>().SetDirection(Vector3.right);
            }
        }
    }

    public void SetCanSpawn(bool newState)
    {
        _canSpawn = newState;
    }
}
