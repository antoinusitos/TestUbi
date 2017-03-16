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
    private GameObject _lastSpawned;

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
                GameObject enemy = Instantiate(prefabEnemy, transform.position, Quaternion.identity);
                enemy.GetComponent<BaseMovementEnemy>().SetDirection(Vector3.right);
                _lastSpawned = enemy;
            }
        }
    }

    public void SetCanSpawn(bool newState)
    {
        _canSpawn = newState;
    }

    public GameObject GetLastSpawned()
    {
        return _lastSpawned;
    }

    public void StopLastSpawned()
    {
        if (_lastSpawned)
            _lastSpawned.GetComponent<BaseMovementEnemy>().SetCanMove(false);
    }

    public void DestroyLastSpawned()
    {
        if (_lastSpawned)
            Destroy(_lastSpawned);
    }
}
