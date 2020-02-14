﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : Building
{
    public Sprite goToBarrackMenuSprite;
    public string goToBarrackMenuText;
    public Unit[] unitsToUnlock;
    public int[] levelToUnlockNextUnit;
	public float timeReducedOnClick =1;

    public override void Start()
    {
        base.Start();

    }
    public override void OnMouseDown()
    {
        base.OnMouseDown();
        UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeBarrack);
        UIManager.Instance.goToMenuButton.onClick.AddListener(ShowUnitInterface);
		ReduceProductionTime();
        RefreshInterface();
        UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, "", "", villagers, workerIconBuilding, buildingIcon, skillPoints.ToString() + " skill points",
        firstSkillPointUpgradeName + " lvl." + firstSkillPointLevel, secondSkillPointUpgradeName + " lvl." + secondSkillPointLevel, thirdSkillPointUpgradeName + " lvl." + thirdSkillPointLevel, fourthSkillPointUpgradeName + " lvl" + fourthSkillPointLevel,
        goToBarrackMenuText, goToBarrackMenuSprite);
    }

	public void ReduceProductionTime()
	{
		if (UnitManager.Instance.canProduceNewUnit == false)
		{
			UnitManager.Instance.currentProducedUnit.totalTimeToProduce -= timeReducedOnClick;
		}
	}


	public override void AddFirstSkillPoint()
	{
		base.AddFirstSkillPoint();
		if (skillFirstUpgraded)
		{
			UnitManager.Instance.currentProducedUnit.timeToProduce += skillFirstBonus;
			UpdateBarrack();
			RefreshInterface();
		}
	}
	public override void AddSecondSkillPoint()
	{
		base.AddSecondSkillPoint();
		if (skillSecondUpgraded)
		{
			ResourceManager.Instance.flatOreBonusPerSec += skillSecondBonus;
			UpdateBarrack();
			RefreshInterface();
		}
	}
	public override void AddThirdSkillPoint()
	{
		base.AddThirdSkillPoint();
		if (skillThirdUpgraded)
		{
			ResourceManager.Instance.percentOreBonusPerClick += skillThirdBonus;
			UpdateBarrack();
			RefreshInterface();
		}
	}
	public override void AddFourthSkillPoint()
	{
		base.AddFourthSkillPoint();
		if (skillFourthUpgraded)
		{
			ResourceManager.Instance.flatOreBonusPerClick += skillFourthBonus;
			UpdateBarrack();
			RefreshInterface();
		}
	}


	public void UpgradeBarrack()
    {
        if (LevelUp())
        {
            UpdateBarrack();
            RefreshInterface();
        }
    }
	public void UpdateBarrack()
    {
        for (int i = 0; i < unitsToUnlock.Length; i++)
        {
            if (levelToUnlockNextUnit[i]==level)
            {
                unitsToUnlock[i].gameObject.SetActive(true);
                unitsToUnlock[i].myAttackPanel.SetActive(true);
				UnitManager.Instance.allUnits.Add(unitsToUnlock[i]);
            }
        }
    }
    public void ShowUnitInterface()
    {
        UIManager.Instance.TroopsProducingCanvas.SetActive(true);
    }
}
