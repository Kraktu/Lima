using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVillage : MonoBehaviour
{
	private void OnMouseDown()
	{
		UIManager.Instance.enemyVillageCanvas.SetActive(true);
		UIManager.Instance.enemyVillageCanvas.transform.position = new Vector3 (gameObject.transform.position.x, UIManager.Instance.enemyVillageCanvas.transform.position.y, gameObject.transform.position.z);
	}
}
