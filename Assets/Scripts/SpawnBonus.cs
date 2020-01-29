using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBonus : MonoBehaviour
{
	public float spawnMinTime = 1;
	public float spawnMaxTime = 1;

	public Vector3 center;
	public Vector3 size;

	
	public ClickBonus[] Bonus;

	float totalSpawnChance;

    void Start()
    {
		Invoke("RandomSpawn", Random.Range(spawnMinTime, spawnMaxTime));
		for (int i = 0; i <Bonus.Length; i++)
		{
			totalSpawnChance+= Bonus[i].spawnChance;
		}
    }



	public void RandomSpawn()
	{
		int generatedNumber = Random.Range(0, (int)totalSpawnChance + 1);
		int objectToSpawn=0;
		int passedNumber=0;
		for (int i = 0; i < Bonus.Length; i++)
		{
			passedNumber += (int)Bonus[i].spawnChance;
			if (generatedNumber<=passedNumber)
			{
				objectToSpawn = i;
				break;
			}
		}
		Vector3 posistion = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));
		Instantiate(Bonus[objectToSpawn], posistion,Bonus[objectToSpawn].transform.rotation);
		float spawnTime = Random.Range(spawnMinTime, spawnMaxTime);
		Invoke("RandomSpawn", spawnTime);

	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1, 0, 0);
		Gizmos.DrawCube(center, size);
	}

}
