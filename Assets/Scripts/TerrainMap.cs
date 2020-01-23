using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TerrainMap : MonoBehaviour
{
	private void OnMouseDown()
	{
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			UIManager.Instance.enemyVillageCanvas.SetActive(false);
			UIManager.Instance.spyPanel.SetActive(false);
			UIManager.Instance.isSpyPanelActive = false;
		}
	}
}
