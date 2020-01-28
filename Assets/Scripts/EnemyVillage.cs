using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVillage : MonoBehaviour
{
    public float timeToAttack;
    public string enemyName;
    public int level;
    public Sprite myIcon;
	private void OnMouseDown()
	{
		UIManager.Instance.enemyVillageCanvas.SetActive(true);
		UIManager.Instance.enemyVillageCanvas.transform.position = new Vector3 (gameObject.transform.position.x, UIManager.Instance.enemyVillageCanvas.transform.position.y, gameObject.transform.position.z);
		UIManager.Instance.spyPanel.SetActive(false);
		UIManager.Instance.isSpyPanelActive = false;
        UIManager.Instance.atEnemyName.text = enemyName;
        UIManager.Instance.atEnemyLvl.text = "Lv."+level.ToString();
        UIManager.Instance.atEnemyIcon.sprite = myIcon;

	}
}
