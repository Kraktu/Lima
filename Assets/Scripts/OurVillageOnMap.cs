using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurVillageOnMap : MonoBehaviour
{
	void GoToVillage()
	{
		Camera cam = FindObjectOfType<Camera>();
        cam.transform.position = MapManager.Instance.initialCameraPosition;
        cam.transform.Rotate(new Vector3(-35, 0, 0));
        cam.orthographic = false;
        cam.orthographicSize = 5;
        UIManager.Instance.gameLight.color = new Color(1, 0.9568f, 0.8392f, 1);
        UIManager.Instance.totalResourceCanvas.SetActive(true);
	}

	private void OnMouseDown()
	{
		GoToVillage();
	}
}
