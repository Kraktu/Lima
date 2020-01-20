using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBonus : MonoBehaviour
{
	public Transform[] SpawnChest;
	public GameObject[] Bonus;
	public int spawnTime = 1;

    void Start()
    {
		InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Update()
    {
        
    }

	void Spawn()
	{
		for (int i = 0; i < Bonus.Length; i++)
		{
			int spawnIndex = Random.Range(0, spawnTime);
			Instantiate(Bonus[i], transform.position, SpawnChest[spawnIndex].rotation);
		}
	}
	
}
