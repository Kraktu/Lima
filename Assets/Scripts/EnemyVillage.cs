﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVillage : MonoBehaviour
{
    public int levelDifferenceWithUs;
    public EnemyArmySO[] enemyArmySOs;

    [HideInInspector]
    public double minWoodWon, maxWoodWon, minOreWon, maxOreWon, minVenacidWon, maxVenacidWon;
    [HideInInspector]
    public float timeToGetAttacked;
    [HideInInspector]
    public string enemyName;
    [HideInInspector]
    public Sprite myIcon;
    [HideInInspector]
    public int level;
    [HideInInspector]
    public List<EnemyArmySO> possibleSO;
    [HideInInspector]
    public EnemyArmySO mySO;
    [HideInInspector]
    public List<Army> myArmy;
	private void OnMouseDown()
	{
		UIManager.Instance.enemyVillageCanvas.SetActive(true);
		UIManager.Instance.enemyVillageCanvas.transform.position = new Vector3 (gameObject.transform.position.x, UIManager.Instance.enemyVillageCanvas.transform.position.y, gameObject.transform.position.z);
		UIManager.Instance.spyPanel.SetActive(false);
		UIManager.Instance.isSpyPanelActive = false;
        UIManager.Instance.atEnemyName.text = enemyName;
        UIManager.Instance.atEnemyLvl.text = "Lv."+ UIManager.Instance.BigIntToString(level);
        UIManager.Instance.atEnemyIcon.sprite = myIcon;
        AttackManager.Instance.timeToAttack = timeToGetAttacked;
        AttackManager.Instance.AttackedVillage = this.gameObject;

	}

    public void LoadAnEnemy()
    {
        possibleSO = new List<EnemyArmySO>();
        if (enemyArmySOs.Length>0)
        {
            for (int i = 0; i < enemyArmySOs.Length; i++)
            {
                if (enemyArmySOs[i].Level == BuildingManager.Instance.generalQuarter.level + levelDifferenceWithUs)
                {
                    gameObject.SetActive(true);
                    possibleSO.Add(enemyArmySOs[i]);
                }
            }
        }
        if (possibleSO.Count>0)
        {
            mySO = possibleSO[Random.Range(0, possibleSO.Count)];
            level = mySO.Level;
            timeToGetAttacked = mySO.timeToGetAttacked;
            myIcon = mySO.myIcon;
            enemyName = mySO.enemyName;
            myArmy = new List<Army>();
            minWoodWon = mySO.minWoodWon;
            maxWoodWon = mySO.maxWoodWon;
            minOreWon = mySO.minOreWon;
            maxOreWon = mySO.maxWoodWon;
            minVenacidWon = mySO.minVenacidWon;
            maxVenacidWon = mySO.maxWoodWon;
            for (int i = 0; i < mySO.enemyArmy.Length; i++)
            {
                myArmy.Add(new Army(mySO.enemyArmy[i].armyName, mySO.enemyArmy[i].armyNbr, mySO.enemyArmy[i].armyAttack, mySO.enemyArmy[i].armyLife, mySO.enemyArmy[i].armyAttackPerTurn, mySO.enemyArmy[i].armyArmor, mySO.enemyArmy[i].armyPierce, mySO.enemyArmy[i].armyAccuracy));
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
