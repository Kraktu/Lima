using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	Camera cam;
	public Vector3 cameraMapPosition, cameraSpherierPosition;
	public Quaternion cameraSpherierRotation;
	[HideInInspector]
	public Vector3 initialCameraPosition;

    public EnemyVillage[] enemyVillages;
	static public MapManager Instance { get; private set; }


	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

	void Start()
    {
		cam = FindObjectOfType<Camera>();
		initialCameraPosition = cam.transform.position;
    }
    public void RefreshEnemies()
    {
        for (int i = 0; i < enemyVillages.Length; i++)
        {
            enemyVillages[i].LoadAnEnemy();
        }
    }
}
