using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFormation : MonoBehaviour 
{
    public int nbRow = 5;
    public int nbColomn = 11;
    public float spacing = 1.5f;
    public float verticalSpacing = 1.0f;

    // untick if formation was hand made
    public bool generateBaseFormation = false;

    public GameObject enemyPrefab;

    public float limitScreenBorder = 1.0f;
    public float movementRate = 2.0f;
    public float movementSize = 1.0f;
    private float _currentMovementReload = 0.0f;

    public List<GameObject> _allChildren;

    private bool _goingRight = true;
    private bool _mustGoingDown = false;
    private bool _canMove = true;

    void Start()
    {
        transform.position = GameObject.FindGameObjectWithTag("BaseFormationSpawner").transform.position;

        if(generateBaseFormation)
        {

            // generate from left to right, row by row first
            for(int row = 0; row < nbRow; row++)
            {
                for(int colomn = 0; colomn < nbColomn; colomn++)
                {
                    GameObject anEnemy = (GameObject)Instantiate(enemyPrefab);
                    anEnemy.transform.parent = transform;
                    anEnemy.transform.localPosition = new Vector3(colomn * spacing, row * verticalSpacing, 0);
                }
            }

            _allChildren = new List<GameObject>();
            for(int i = 0; i < transform.childCount; i++)
            {
                int row = i / nbColomn;

                _allChildren.Add(transform.GetChild(i).gameObject);
                _allChildren[i].GetComponent<BaseEnemy>().SetParentBase(this);
                _allChildren[i].GetComponent<BaseEnemy>().SetEnemyType(row + 1);
                if (row == 0)
                {
                    _allChildren[i].GetComponent<BaseEnemy>().SetCanFire(true);
                }
            }
        }
    }

    public void SetCanMove(bool newState)
    {
        _canMove = newState;
        if (!_canMove)
        {
            for (int i = 0; i < _allChildren.Count; i++)
            {
                if(_allChildren[i] != null)
                    _allChildren[i].GetComponent<BaseEnemy>().SetCanFire(false);
            }
        }
    }

    void Update()
    {
        if (!_canMove) return;


        _currentMovementReload += Time.deltaTime;
        if (_currentMovementReload >= movementRate)
        {
            _currentMovementReload = 0.0f;
            if(_mustGoingDown)
            {
                transform.position -= new Vector3(0, movementSize, 0);
                _mustGoingDown = false;
                return;
            }
            
            if(_goingRight)
            {
                // get the child the farthest to the right
                float Rightmost = 0.0f;
                for (int i = 0; i < _allChildren.Count; i++)
                {
                    if (_allChildren[i] && _allChildren[i].transform.position.x > Rightmost)
                        Rightmost = _allChildren[i].transform.position.x;
                }

                transform.position += new Vector3(movementSize, 0, 0);

                // check if we have reach the right side
                Vector3 tempPos = Camera.main.WorldToScreenPoint(new Vector3(Rightmost, 0, 0) + new Vector3(limitScreenBorder, 0, 0));
                if (tempPos.x >= Screen.width)
                {
                    _goingRight = false;
                    _mustGoingDown = true;
                }
            }
            else
            {
                // get the child the farthest to the left
                float leftmost = 0.0f;
                for (int i = 0; i < _allChildren.Count; i++)
                {
                    if (_allChildren[i] && _allChildren[i].transform.position.x < leftmost)
                        leftmost = _allChildren[i].transform.position.x;
                }

                transform.position -= new Vector3(movementSize, 0, 0);

                // check if we have reach the left side
                Vector3 tempPos = Camera.main.WorldToScreenPoint(new Vector3(leftmost, 0, 0) - new Vector3(limitScreenBorder, 0, 0));
                if (tempPos.x < 0)
                {
                    _goingRight = true;
                    _mustGoingDown = true;
                }
            }
        }
    }

    public void UpdateList(GameObject deletedGameObject)
    {
        for (int i = 0; i < _allChildren.Count; i++)
        {
            if (deletedGameObject == _allChildren[i])
            {
                int newIndex = i + nbColomn;
                if (newIndex >= 0 && newIndex < _allChildren.Count && _allChildren[newIndex] != null)
                {
                    _allChildren[newIndex].GetComponent<BaseEnemy>().SetCanFire(true);
                }
            }
        }

        if(_allChildren.Count <= 0)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<BaseGameManager>().Win();
        }
    }
}
