using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurVillageOnMap : MonoBehaviour
{
	void GoToVillage()
	{
		Camera cam = FindObjectOfType<Camera>();
		cam.transform.position = MapManager.Instance.initialCameraPosition;
		UIManager.Instance.totalResourceCanvas.SetActive(true);
	}

	private void OnMouseDown()
	{
		GoToVillage();
	}
}
