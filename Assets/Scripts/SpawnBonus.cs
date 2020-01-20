using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBonus : MonoBehaviour
{
	public float spawnTime = 1;

	public Vector3 center;
	public Vector3 size;

	
	public Transform[] SpawnChest;
	public GameObject[] Bonus;

    void Start()
    {
		InvokeRepeating("RandomSpawn", spawnTime, spawnTime);
    }

	public void RandomSpawn()
	{
		Vector3 posistion = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));
		Instantiate(Bonus[0], posistion, Quaternion.identity);
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1, 0, 0);
		Gizmos.DrawCube(center, size);
	}

}
