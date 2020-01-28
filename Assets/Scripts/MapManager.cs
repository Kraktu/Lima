using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	Camera cam;
	public Vector3 cameraMapPosition;
	[HideInInspector]
	public Vector3 initialCameraPosition;
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
}
