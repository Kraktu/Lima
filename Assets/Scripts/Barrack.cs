﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Barrack : Building
{
    public Sprite goToBarrackMenuSprite;
    public string goToBarrackMenuText;
    public Unit[] unitsToUnlock;
    public int[] levelToUnlockNextUnit;
	public double timeReducedOnClick =1;
	public double timePercentReducedOnClick = 1;

    public override void Start()
    {
        base.Start();

    }
    public override void OnMouseDown()
    {
        base.OnMouseDown();
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			if (isCurrentlyUpgrading == true)
			{
				elpasedTime += timeToReduce;
				InstantiateParticles(UIManager.Instance.BigIntToString(timeToReduce), imDuringUpgrade);
				SoundManager.Instance.PlaySoundEffect("ClickScaffolding_SFX");

			}
			else if (isCurrentlyUpgrading == false)
			{
				UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeBarrack);
				ReduceProductionTime();
				RefreshInterface();
			}
		}

    }

	public void ReduceProductionTime()
	{
		if (UnitManager.Instance.canProduceNewUnit == false)
		{
			UnitManager.Instance.currentProducedUnit.totalTimeToProduce -= timeReducedOnClick*(1+timePercentReducedOnClick/100);
			InstantiateParticles(UIManager.Instance.BigIntToString(timeReducedOnClick), imNormalUse);
			SoundManager.Instance.PlaySoundEffect("ClickBarrack_SFX");
		}
	}
	public override void AddWorkerToProducing()
	{
		base.AddWorkerToProducing();
		if (workerGotUpgraded)
		{
			for (int i = 0; i < unitsToUnlock.Length; i++)
			{
				unitsToUnlock[i].timeToReduceMultiplicator -= 0.1;
			}
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
		if(level == 1)
		{
			UIManager.Instance.troopsButton.gameObject.SetActive(true);
		}
    }

	public override void AddFirstSkillPoint()
	{
		base.AddFirstSkillPoint();
		if (skillFirstUpgraded)
		{
			for (int i = 0; i < unitsToUnlock.Length; i++)
			{
				unitsToUnlock[i].timeToReduceMultiplicator -= firstSkillPointLevel;
			}
			UpdateBarrack();
			RefreshInterface();
		}
	}
	public override void AddSecondSkillPoint()
	{
		base.AddSecondSkillPoint();
		if (skillSecondUpgraded)
		{
			for (int i = 0; i < unitsToUnlock.Length; i++)
			{
				unitsToUnlock[i].flatTimeReducing += skillSecondBonus;
			}
			UpdateBarrack();
			RefreshInterface();
		}
	}
	public override void AddThirdSkillPoint()
	{
		base.AddThirdSkillPoint();
		if (skillThirdUpgraded)
		{
			timePercentReducedOnClick+= skillThirdBonus;
			UpdateBarrack();
			RefreshInterface();
		}
	}
	public override void AddFourthSkillPoint()
	{
		base.AddFourthSkillPoint();
		if (skillFourthUpgraded)
		{
			timeReducedOnClick += skillFourthBonus;
			UpdateBarrack();
			RefreshInterface();
		}
	}


	public void UpdateBarrack()
    {
        for (int i = 0; i < unitsToUnlock.Length; i++)
        {
            if (levelToUnlockNextUnit[i]==level&&unitsToUnlock[i].isUnitsUnlocked == false)
            {
                unitsToUnlock[i].gameObject.SetActive(true);
                unitsToUnlock[i].myAttackPanel.SetActive(true);
				unitsToUnlock[i].isUnitsUnlocked = true;
				UnitManager.Instance.allUnits.Add(unitsToUnlock[i]);
            }
        }
    }
	public override void RefreshInterface()
	{
		base.RefreshInterface();
		UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, "", "", villagers, workerIconBuilding, buildingIcon, UIManager.Instance.BigIntToString(skillPoints) + " skill points",
		firstSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillFirstBonus) + firstSkillPointUpgradeNameEnd + " lvl." + firstSkillPointLevel, secondSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillSecondBonus) + secondSkillPointUpgradeNameEnd + " lvl." + secondSkillPointLevel, thirdSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillThirdBonus) + thirdSkillPointUpgradeNameEnd + " lvl." + thirdSkillPointLevel, fourthSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillFourthBonus) + fourthSkillPointUpgradeNameEnd + " lvl" + fourthSkillPointLevel);
	}
	public void ShowUnitInterface()
    {
        UIManager.Instance.TroopsProducingCanvas.SetActive(true);
		UIManager.Instance.DisableButton();
		SoundManager.Instance.PlaySoundEffect("GoToTroops_SFX");
    }
}
