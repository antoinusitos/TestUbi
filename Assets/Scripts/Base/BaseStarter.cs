using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStarter : MonoBehaviour 
{
    public GameObject player;
    public GameObject formationSpawner;
    public GameObject playerSpawner;
    public GameObject startText;

    public GameObject FormationPrefab;

    void Start()
    {
        startText.SetActive(true);
    }

	void Update () 
    {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            startText.SetActive(false);
            player.transform.position = playerSpawner.transform.position;
            player.GetComponent<BasePlayerMovement>().SetStuck(false);
            Instantiate(FormationPrefab, formationSpawner.transform.position, Quaternion.identity);
        }
	}
}
